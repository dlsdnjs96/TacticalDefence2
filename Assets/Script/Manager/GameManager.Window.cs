using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public BaseWindow heroDeckWindow;
    public BaseWindow victoryWindow;
    public BaseWindow defeatWindow;
    public BaseWindow editDeckListWindow;
    public BaseWindow loadingWindow;
    public BaseWindow playWindow;


    // Common
    public void OpenHeroDeckWindow()
    {
        heroDeckWindow.OpenWindow();
    }
    public void CloseHeroDeckWindow()
    {
        heroDeckWindow.CloseWindow();
    }
    public void OpenVictoryWindow()
    {
        victoryWindow.OpenWindow();
    }
    public void CloseVictoryWindow()
    {
        victoryWindow.CloseWindow();
    }
    public void OpenDefeatWindow()
    {
        defeatWindow.OpenWindow();
    }
    public void CloseDefeatWindow()
    {
        defeatWindow.CloseWindow();
    }
    public void OpenEditDeckListWindow()
    {
        editDeckListWindow.OpenWindow();
    }
    public void CloseEditDeckListWindow()
    {
        editDeckListWindow.CloseWindow();
    }
    public void OpenLoadingWindow() { loadingWindow.OpenWindow(); }
    public void CloseLoadingWindow() { loadingWindow.CloseWindow(); }

    public void OpenPlayWindow() { playWindow.OpenWindow(); }
    public void ClosePlayWindow() { playWindow.CloseWindow(); }
}
