using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RobbyManager : MonoBehaviour
{
    [SerializeField] GameObject heroInventoryWindow;
    [SerializeField] GameObject heroInfoWindow;

    public void OpenHeroInventory() { heroInventoryWindow.SetActive(true); }
    public void CloseHeroInventory() { heroInventoryWindow.SetActive(false); }
    public void OpenHeroInfo() { heroInfoWindow.SetActive(true); }
    public void CloseHeroInfo() { heroInfoWindow.SetActive(false); }
}
