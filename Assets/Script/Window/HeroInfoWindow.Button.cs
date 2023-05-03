using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HeroInfoWindow : BaseWindow
{
    [SerializeField] GameObject statWindow;
    [SerializeField] GameObject equipWindow;
    [SerializeField] GameObject skillWindow;

    public void OpenStat()
    {
        statWindow.SetActive(true);
        equipWindow.SetActive(false);
        skillWindow.SetActive(false);
    }
    public void OpenEquip()
    {
        equipWindow.SetActive(true);
        statWindow.SetActive(false);
        skillWindow.SetActive(false);
    }
    public void OpenSkill()
    {
        skillWindow.SetActive(true);
        statWindow.SetActive(false);
        equipWindow.SetActive(false);
    }
}
