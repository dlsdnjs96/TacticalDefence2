using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class HeroSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public BaseWindow connectedWindow;


    private Image slotImage;
    [SerializeField] private Image heroImage;
    [SerializeField] private Image grayScaleImage;
    private bool selected;
    public HeroInfo heroInfo;

    protected bool isDragged;

    public GameObject scrollView;
    float prevY;


    void Awake()
    {
        selected = false;
        slotImage = GetComponent<Image>();
        scrollView = transform.parent.gameObject;
    }



    public void OnDrag(PointerEventData eventData)
    {
        isDragged = true;
        scrollView.transform.position += Vector3.down * (prevY - eventData.position.y) * scrollView.transform.lossyScale.y;
        prevY = eventData.position.y;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (selected || heroInfo == null || isDragged) return;

        if (!connectedWindow.gameObject.activeSelf)
            connectedWindow.OpenWindow();
        connectedWindow.ReceiveHeroInfo(heroInfo, out selected);
        SetSelected(selected);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragged = false;
        prevY = eventData.position.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = false;
        prevY = eventData.position.y;
    }


    public void SetSelected(bool _selected)
    {
        selected = _selected;
        grayScaleImage.gameObject.SetActive(selected);
    }

    public void UpdateSlotImage(int _heroID)
    {
        if (_heroID == 0)
            heroImage.sprite = Resources.Load<Sprite>("Texture/UI/HeroSlot");
        else
            heroImage.sprite = Resources.Load<Sprite>("Texture/Icon/" + _heroID);
        
    }

}


