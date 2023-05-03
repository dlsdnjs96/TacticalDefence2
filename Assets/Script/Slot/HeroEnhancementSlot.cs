using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HeroEnhancementSlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public HeroInventory heroInventory;


    private Image slotImage;
    public HeroInfo heroInfo;




    void Awake()
    {
        slotImage = GetComponent<Image>();
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        (heroInventory.connectedWindow as HeroEnhancementInfoWindow).ReturnRawMaterialSlot();
        ReturnHeroInfo();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        print("OnPointerDown");
    }

    public void SetHeroInfo(HeroInfo _heroInfo)
    {
        heroInfo = _heroInfo;
        UpdateSlotImage();
    }

    public void ReturnHeroInfo()
    {
        if (heroInfo != null) heroInventory.ReturnHero(heroInfo.heroUID);
        heroInfo = null;
        UpdateSlotImage();
    }
    public void UpdateSlotImage()
    {
        if (heroInfo == null || heroInfo.heroID == 0)
            slotImage.sprite = Resources.Load<Sprite>("Texture/UI/HeroSlot");
        else
            slotImage.sprite = Resources.Load<Sprite>("Texture/Icon/" + heroInfo.heroID);

    }


}
