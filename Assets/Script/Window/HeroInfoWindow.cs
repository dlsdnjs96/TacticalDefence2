using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class HeroInfoWindow : BaseWindow
{
    [SerializeField] TextMeshProUGUI heroName;
    [SerializeField] TextMeshProUGUI heroStatKey;
    [SerializeField] TextMeshProUGUI heroStatValue;
    [SerializeField] GameObject heroImage;
    [SerializeField] HeroInventory heroInventory;
    private HeroInfo heroInfo = null;




    private void OnEnable()
    {
        ResetInfo();
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        heroInventory.UpdateSlotImages();
    }


    public override void ReceiveHeroInfo(HeroInfo _heroInfo, out bool _selected)
    {
        _selected = false;
        heroInfo = _heroInfo;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        UpdateNameInfo();
        UpdateStatInfo();
    }

    private void ResetInfo()
    {
        if (heroInfo != null) (connectedWindow as HeroInventory).ReturnHero(heroInfo.heroUID);
        heroInfo = null;
        ClearHeroImage();
        heroStatValue.text = "";
    }

    private void ClearHeroImage()
    {
        for (int i = 1; i < heroImage.GetComponentsInChildren<Transform>().Length; i++)
            GameObject.Destroy(heroImage.GetComponentsInChildren<Transform>()[i].gameObject);
    }

    public void UpdateStatInfo()
    {
        ClearHeroImage();

        Hero hero = EntityPool.Instance.Get(heroInfo.heroID) as Hero;
        hero.gameObject.transform.SetParent(heroImage.transform);
        hero.ApplyHeroInfo(heroInfo);
        hero.SetReady();
        //heroStatKey.text = hero.stat.ToDetailKey();
        heroStatValue.text = hero.stat.ToDetailValue();
    }
    public void UpdateNameInfo()
    {

    }
    public void UpdateSkillInfo()
    {

    }
}
