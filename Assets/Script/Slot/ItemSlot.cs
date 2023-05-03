using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    Item item;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemQuantity;

    void SlotUpdate()
    {
        UpdateImage();
        UpdateQuantity();
    }

    void UpdateImage()
    {
        if (item == null)
        {
            itemImage.gameObject.SetActive(false);
        }
        else
        {
            itemImage = Resources.Load<Image>("Texture/Icon/Item/"+item.itemID.ToString());

            if (itemImage != null) itemImage.gameObject.SetActive(true);
            else itemImage.gameObject.SetActive(false);
        }
    }

    void UpdateQuantity()
    {
        if (item == null)
        {
            itemQuantity.gameObject.SetActive(false);
        }
        else
        {
            itemQuantity.gameObject.SetActive(true);

            if (item.ea <= 1) itemQuantity.text = "";
            else itemQuantity.text = item.ea.ToString();
        }
    }
}
