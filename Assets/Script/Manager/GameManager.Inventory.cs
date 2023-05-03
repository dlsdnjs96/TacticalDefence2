using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public HeroInventory heroInventory;
    public DeckInventory deckInventory;



    // HeroInventory
    public bool HasHeroInInven(string _heroUID)
    {
        return heroInventory.HasHero(_heroUID);
    }
    public void ReturnHeroToInven(string _heroUID)
    {
        heroInventory.ReturnHero(_heroUID);
    }
    public void UpdateHeroInventoryImages()
    {
        heroInventory.UpdateSlotImages();
    }
    public void UpdateSelectedHeroSlots()
    {
        heroInventory.UpdateSelectedSlots();
    }

    // DeckInventory
    public bool AddHeroToDeck(HeroInfo _heroInfo)
    {
        return deckInventory.AddHero(_heroInfo);
    }
    public int FindHeroInDeck(string _heroUID)
    {
        return deckInventory.FindHero(_heroUID);
    }
    public bool CanInsertHeroInDeck(HeroInfo _heroInfo)
    {
        return deckInventory.CanInsertHero(_heroInfo);
    }
    public bool IsHeoInCurrentDeck(string _heroUID)
    {
        return deckInventory.currentDeck.IsExist(_heroUID);
    }
    public HeroDeck GetCurrentDeck()
    {
        return deckInventory.currentDeck;
    }
    public HeroDeckList GetDeckList()
    {
        return deckInventory.heroDeckList;
    }
    public void SetDeckList(HeroDeckList _heroDeckList)
    {
        deckInventory.heroDeckList = _heroDeckList;
    }
    public void UpdateDeckList()
    {
        deckInventory.UpdateDeckList();
    }
}
