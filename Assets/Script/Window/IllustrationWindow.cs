using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustrationWindow : BaseWindow
{
    private HeroSlot[] heroSlots;

    [SerializeField] public HeroSlot heroSlotPrefab;
    [SerializeField] private GameObject content;




    void Awake()
    {
        int index = 0;
        Hero[] heroList = Resources.LoadAll<Hero>("Hero/");
        heroSlots = new HeroSlot[heroList.Length];
        foreach (Hero hero in heroList)
        {
            heroSlots[index] = Instantiate(heroSlotPrefab, content.transform);
            heroSlots[index].heroInfo = new HeroInfo(int.Parse(hero.gameObject.name));
            heroSlots[index].UpdateSlotImage(int.Parse(hero.gameObject.name));
            heroSlots[index].connectedWindow = this;
            index++;
        }
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
    }
    public override void CloseWindow()
    {
        connectedWindow.CloseWindow();
        base.CloseWindow();
    }

    public override void ReceiveHeroInfo(HeroInfo _heroInfo, out bool _selected)
    {
        _selected = false;
        (connectedWindow as IllustrationInfoWindow).selectedHeroID = _heroInfo.heroID;
        connectedWindow.OpenWindow();
    }
}
