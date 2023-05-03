using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInventory : BaseWindow
{
    private HeroSlot[] heroSlots;
    public static HeroInfo[] heroInven;

    [SerializeField] public HeroSlot heroSlotPrefab;
    [SerializeField] private GameObject content;

    public GameManager gameManager;
    public int slotSize;



    void Awake()
    {
        slotSize = 30;
        heroSlots = new HeroSlot[slotSize];
        for (int i = 0; i < slotSize; i++)
        {
            heroSlots[i] = Instantiate(heroSlotPrefab, content.transform);
            heroSlots[i].connectedWindow = connectedWindow;
        }

        heroInven = Database.heroInven;
    }
    void Start() {
        heroInven = Database.heroInven;

        for (int i = 0; i < slotSize; i++)
        {
            if (heroInven[i] != null) heroSlots[i].heroInfo = heroInven[i];
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
            if (heroInven[i] != null) heroSlots[i].heroInfo = heroInven[i];
        }
        for (int i = 0; i < slotSize; i++)
        {
            if (heroInven[i] != null) heroSlots[i].UpdateSlotImage(heroInven[i].heroID);
        }
    }

    public void UpdateSelectedSlots()
    {
        foreach (HeroSlot slot in heroSlots)
        {
            if (slot.heroInfo != null && gameManager.IsHeoInCurrentDeck(slot.heroInfo.heroUID)) 
                slot.SetSelected(true);
            else 
                slot.SetSelected(false);
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
            if (heroInven[i] != null && heroInven[i].heroUID == _heroUID) {
                heroSlots[i].SetSelected(false);
                return;
            }
        }
        return;
    }

    public bool HasHero(string _heroUID)
    {
        for (int i = 0;i < slotSize;i++)
        {
            if (heroInven[i] != null && heroInven[i].heroUID == _heroUID)
                return true;
        }
        return false;
    }


}
