using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ShopWindow : BaseWindow
{
    [SerializeField] GameObject rubyWindow;
    [SerializeField] GameObject cashWindow;
    [SerializeField] GameObject adWindow;

    public void OpenRubyWindow()
    {
        rubyWindow.SetActive(true);
        cashWindow.SetActive(false);
        adWindow.SetActive(false);
    }
    public void OpenCashWindow()
    {
        rubyWindow.SetActive(false);
        cashWindow.SetActive(true);
        adWindow.SetActive(false);
    }
    public void OpenAdWindow()
    {
        rubyWindow.SetActive(false);
        cashWindow.SetActive(false);
        adWindow.SetActive(true);
    }
}
