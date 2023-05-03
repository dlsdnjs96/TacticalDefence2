using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancementWindow : BaseWindow
{
    [SerializeField] GameObject heroListWindow;
    [SerializeField] GameObject itemListWindow;

    public void OpenHeroListWindow()
    {
        heroListWindow.SetActive(true);
        itemListWindow.SetActive(false);
    }
    public void OpenItemListWindow()
    {
        itemListWindow.SetActive(true);
        heroListWindow.SetActive(false);
    }
}
