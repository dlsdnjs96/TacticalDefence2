using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public partial class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI timeScaleText;
    [SerializeField] private TextMeshProUGUI noticeText;
    [SerializeField] private TextMeshProUGUI stageNumber;

    private float playTimeScale;
    private int killCount;




    public void PlayNextGame()
    {
        currentStage++;
        PlayGame();
    }
    public void PlayGame()
    {
        if (!CanStartGame()) return;

        gameManager.CloseVictoryWindow();
        gameManager.CloseDefeatWindow();
        gameManager.ClosePlayWindow();


        Enemy.spawner = GameObject.Find("Spawner");
        ResetStage();

        killCount = 0;
        foreach (SpawnData _data in spawnData[currentStage].data)
            StartCoroutine(CoSpawnEnemy(_data));
    }

    public bool CanStartGame()
    {
        if (!gameManager.HasAnyHeroInField())
        {
            Notice.Instance.ShowNoticeText("You need atleast one hero to start game");
            ShowNotice("You need atleast one hero to start game", 3f);
            return false;
        }
        return true;
    }

    public void NextStage()
    {
        if (!IsStageAvailable(currentStage + 1)) return;
        currentStage++;
        stageNumber.text = "Stage " + currentStage.ToString();
    }
    public void PrevStage()
    {
        if (!IsStageAvailable(currentStage - 1)) return;
        currentStage--;
        stageNumber.text = "Stage " + currentStage.ToString();
    }
    public bool IsStageAvailable(int _stage)
    {
        return spawnData.ContainsKey(_stage);
    }

    public void ShowNotice(string _notice, float _duration)
    {
        noticeText.gameObject.SetActive(true);
        noticeText.text = _notice;
        StartCoroutine(CoShowNotice(_duration));
    }

    IEnumerator CoShowNotice(float _duration)
    {
        yield return new WaitForSecondsRealtime(_duration);
        noticeText.gameObject.SetActive(false);
    }

    public void AddTimeScale()
    {
        playTimeScale += 1f;
        UpdateTimeScale();
    }

    private void UpdateTimeScale()
    {
        if (playTimeScale >= 4f)
            playTimeScale = 1f;

        Time.timeScale = playTimeScale;
        timeScaleText.text = "¡¿"+ playTimeScale.ToString();
    }
}
