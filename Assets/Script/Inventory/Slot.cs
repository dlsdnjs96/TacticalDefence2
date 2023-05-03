using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    protected bool isDragged;

    public GameObject scrollView;
    float prevY;
    void Awake()
    {
        //scrollView = transform.parent.gameObject;
    }


    public virtual void OnDrag(PointerEventData eventData)
    {
        isDragged = true;

        scrollView.transform.position += Vector3.down * (prevY - eventData.position.y);
        prevY = eventData.position.y;
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = false;
        prevY = eventData.position.y;
    }

}

