using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : BaseWindow
{
    private ItemSlot[] itemSlots;
    public static HeroInfo[] heroInven;

    [SerializeField] public ItemSlot itemSlotPrefab;
    [SerializeField] private GameObject content;

    public GameManager gameManager;
    public int slotSize;



    void Awake()
    {
        slotSize = 30;
        itemSlots = new ItemSlot[slotSize];
        for (int i = 0; i < slotSize; i++)
        {
            itemSlots[i] = Instantiate(itemSlotPrefab, content.transform);
        }

        heroInven = Database.heroInven;
    }
    void Start()
    {
        heroInven = Database.heroInven;

        for (int i = 0; i < slotSize; i++)
        {
            //if (heroInven[i] != null) itemSlots[i].heroInfo = heroInven[i];
        }
        UpdateSlotImages();
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        UpdateSlotImages();
    }
    public override void CloseWindow()
    {
        connectedWindow.CloseWindow();
        base.CloseWindow();
    }


    public void UpdateSlotImages()
    {
        heroInven = Database.heroInven;
        for (int i = 0; i < slotSize; i++)
        {
            //if (heroInven[i] != null) heroSlots[i].heroInfo = heroInven[i];
        }
        for (int i = 0; i < slotSize; i++)
        {
            //if (heroInven[i] != null) heroSlots[i].UpdateSlotImage(heroInven[i].heroID);
        }
    }

    public void UpdateSelectedSlots()
    {
        foreach (ItemSlot slot in itemSlots)
        {
            //if (slot.heroInfo != null && gameManager.IsHeoInCurrentDeck(slot.heroInfo.heroUID))
            //    slot.SetSelected(true);
            //else
            //    slot.SetSelected(false);
        }
    }


    public bool AddHero(HeroInfo _heroInfo)
    {
        if (!gameManager.CanInsertHeroInDeck(_heroInfo))
        {
            return false;
        }
        return gameManager.AddHeroToDeck(_heroInfo);
    }

    public void ReturnHero(string _heroUID)
    {
        for (int i = 0; i < slotSize; i++)
        {
            if (heroInven[i] != null && heroInven[i].heroUID == _heroUID)
            {
                return;
            }
        }
        return;
    }

    public bool HasHero(string _heroUID)
    {
        for (int i = 0; i < slotSize; i++)
        {
            if (heroInven[i] != null && heroInven[i].heroUID == _heroUID)
                return true;
        }
        return false;
    }
}
