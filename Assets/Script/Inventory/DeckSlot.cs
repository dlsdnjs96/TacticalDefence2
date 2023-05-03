using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DeckSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static DeckInventory deckInventory;
    private Image slotImage;
    //public string heroUID;
    public HeroInfo heroInfo;
    public int slotNumber;


    void Awake()
    {
        slotImage = transform.Find("HeroImage").GetComponent<Image>();
        slotImage.enabled = false;
        heroInfo = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        deckInventory.pickedImage.transform.position = eventData.position;
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (heroInfo == null) return;

        deckInventory.selectedSlot = this;


        deckInventory.pickedImage.transform.position = eventData.position;
        deckInventory.pickedImage.enabled = true;
        deckInventory.pickedImage.sprite = Resources.Load<Sprite>("Texture/Icon/" + heroInfo.heroID);
        slotImage.enabled = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        deckInventory.OnPointerUp(eventData.position);

        deckInventory.selectedSlot = null;
        deckInventory.pickedImage.enabled = false;
    }

    public void UpdateSlot(HeroInfo _heroInfo)
    {
        heroInfo = _heroInfo;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (heroInfo == null) 
            slotImage.enabled = false;
        else
        {
            slotImage.enabled = true;
            slotImage.sprite = Resources.Load<Sprite>("Texture/Icon/" + heroInfo.heroID);
        }
    }

}
