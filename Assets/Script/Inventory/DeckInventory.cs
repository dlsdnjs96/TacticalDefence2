using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class HeroDeck
{
    public string name;
    public string[] slots;
    private int count;


    public HeroDeck()
    {
        name = "New Deck";
        slots = new string[12];
        Array.Fill<string>(slots, "");
        count = 0;
    }
    public HeroDeck(string _name)
    {
        name = _name;
        slots = new string[12];
        Array.Fill<string>(slots, "");
        count = 0;
    }

    public int GetCount()
    {
        count = 0;
        for (int i = 0;i < 12;i++)
        {
            if (slots[i] != "") count++;
        }
        return count;
    }

    public bool IsExist(string _heroUID)
    {
        for (int i = 0; i < 12; i++)
        {
            if (slots[i] == _heroUID)
                return true;
        }
        return false;
    }
}

public partial class DeckInventory : BaseWindow
{
    public DeckSlot selectedSlot { set; private get; }
    private DeckSlot overedSlot;
    public Image pickedImage;
    private DeckSlot[] deckSlots;

    public HeroDeck currentDeck { private set; get; }

    public GameManager gameManager;

    private void Awake()
    {
        DeckSlot.deckInventory = this;

        deckSlots = new DeckSlot[12];

        for (int i = 0; i < 12; i++)
        {
            deckSlots[i] = transform.Find("Slots").GetChild(i).GetComponent<DeckSlot>();
            deckSlots[i].slotNumber = i;
        }


        LoadData();

        UpdateDeckList();

        gameManager.InsertDeckIntoField();
        gameManager.UpdateHeroInventoryImages();
        gameManager.UpdateSelectedHeroSlots();
    }

    void Start()
    {
        UpdateSlots();
    }

    public void UpdateDeckList()
    {
        optionList = new List<string>();

        AcceptDeckListToOption();
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < 12; i++)
        {
            if (!gameManager.HasHeroInInven(currentDeck.slots[i]))
                currentDeck.slots[i] = "";
        }
        for (int i = 0; i < 12; i++)
            deckSlots[i].UpdateSlot(Database.Instance.GetHeroInfoByUID(currentDeck.slots[i]));
    }

    private DeckSlot FindSlot(Vector2 _pos)
    {
        foreach (DeckSlot slot in deckSlots)
        {
            if (slot.gameObject.GetComponent<BoxCollider2D>().OverlapPoint(_pos)) return slot;
        }
        return null;
    }

    public bool IsHeroInCurrentDeck(string _heroUID)
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentDeck.slots[i] == _heroUID)
                return true;
        }
        return false;
    }

    public bool HasSameHero(HeroInfo _heroInfo)
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentDeck.slots[i] == "")
                continue;
            if (Database.Instance.GetHeroInfoByUID(currentDeck.slots[i]).heroID == _heroInfo.heroID)
                return true;
        }
        return false;
    }

    public bool CanInsertHero(HeroInfo _heroInfo)
    {
        if (currentDeck.GetCount() >= 5) { gameManager.ShowNotice("You cannot add further hero", 3f); return false; }

        if (HasSameHero(_heroInfo)) { gameManager.ShowNotice("You cannot add same hero", 3f); return false; }

        return true;
    }

    public int FindHero(string _heroUID)
    {
        for (int i = 0;i < deckSlots.Length;i++)
        {
            if (deckSlots[i].heroInfo.heroUID == _heroUID) return i;
        }
        return -1;
    }
    public override void ReceiveHeroInfo(HeroInfo _heroInfo, out bool _selected)
    {
        _selected = false;
        if (!CanInsertHero(_heroInfo) || HasSameHero(_heroInfo))
            return;

        _selected = true;
        AddHero(_heroInfo);
        UpdateSlots();
    }


    public void OnPointerUp(Vector2 _pos)
    {
        overedSlot = FindSlot(_pos);

        if (overedSlot == null) // 덱 밖으로 드래그 할 경우
        {
            // 히어로 인벤토리에서 선택 가능하도록 변경
            gameManager.ReturnHeroToInven(selectedSlot.heroInfo.heroUID);
            // 덱에서 제거
            currentDeck.slots[selectedSlot.slotNumber] = "";
            selectedSlot.heroInfo = null;
            selectedSlot.UpdateSlot();
            gameManager.InsertDeckIntoField();
            return;
        } else if (selectedSlot == null) return;

        Util.Swap<HeroInfo>(ref overedSlot.heroInfo, ref selectedSlot.heroInfo);
        Util.Swap<string>(ref currentDeck.slots[overedSlot.slotNumber], ref currentDeck.slots[selectedSlot.slotNumber]);

        overedSlot.UpdateSlot();
        selectedSlot.UpdateSlot();

        gameManager.InsertDeckIntoField();
    }

    public bool RemoveHero(string _heroUID)
    {
        foreach (DeckSlot slot in deckSlots)
        {
            if (slot.heroInfo.heroUID == _heroUID)
            {
                slot.heroInfo= null;
                slot.UpdateSlot();
                return true;
            }
        }
        return false;
    }
    public bool AddHero(HeroInfo _heroInfo)
    {
        foreach (DeckSlot slot in deckSlots)
        {
            if (slot.heroInfo == null)
            {
                slot.heroInfo = _heroInfo;
                currentDeck.slots[slot.slotNumber] = _heroInfo.heroUID;
                slot.UpdateSlot();
                gameManager.InsertDeckIntoField();
                return true;
            }
        }
        return false;
    }

    public void Exit()
    {
        SaveData();
        gameManager.CloseHeroDeckWindow();
    }
}
