using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroEnhancementInfoWindow : BaseWindow
{
    private HeroInfo heroInfo;
    [SerializeField] private HeroEnhancementSlot mainSlot;
    [SerializeField] private HeroEnhancementSlot rawMaterialSlot;

    [SerializeField] Button enhanceButton;
    [SerializeField] Button enhance10Button;

    public override void ReceiveHeroInfo(HeroInfo _heroInfo, out bool _selected)
    {
        _selected = true;
        heroInfo = _heroInfo;

        if (mainSlot.heroInfo == null)
            mainSlot.SetHeroInfo(heroInfo);
        else
        {
            if (rawMaterialSlot.heroInfo == null)
                rawMaterialSlot.SetHeroInfo(heroInfo);
            else
            {
                rawMaterialSlot.heroInventory.ReturnHero(rawMaterialSlot.heroInfo.heroUID);
                rawMaterialSlot.SetHeroInfo(heroInfo);
            }
        }
    }

    public override void CloseWindow()
    {
        mainSlot.ReturnHeroInfo();
        rawMaterialSlot.ReturnHeroInfo();
        base.CloseWindow();
    }
    
    public void ReturnRawMaterialSlot()
    {
        rawMaterialSlot.ReturnHeroInfo();
    }
}
