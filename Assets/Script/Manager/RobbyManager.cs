using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class RobbyManager : MonoBehaviour
{
    private void Start()
    {

    }
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("NormalStage");
    }
}
