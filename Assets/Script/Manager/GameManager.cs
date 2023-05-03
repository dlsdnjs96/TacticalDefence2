using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    public StageManager stageManager;
    public HeroSpawner heroSpawner;


    void OnEnable()
    {
        deckInventory.LoadData();
        heroSpawner.InsertDeck();

        OpenPlayWindow();

        Hero.gameManager = this;
    }

    // StageManager
    public void ShowNotice(string _notice, float _duration)
    {
        stageManager.ShowNotice(_notice, _duration);
    }
    public void DeadHero()
    {
        stageManager.DeadHero();
    }
    public void KilledEnemy(int _enemyID)
    {
        stageManager.KilledEnemy(_enemyID);
    }

    // HeroSpanwer
    public bool HasAnyHeroInField()
    {
        return heroSpawner.HasAnyHero();
    }
    public bool HasAnyAliveHeroInField()
    {
        return heroSpawner.HasAnyAliveHero();
    }
    public void ResetHerosInField()
    {
        heroSpawner.ResetHeros();
    }
    public void InsertDeckIntoField()
    {
        heroSpawner.InsertDeck();
    }

    // EnemyPool
    public void ClearAllEnemy()
    {
        EntityPool.Instance.ReturnWithTag("Enemy");
    }

    public void ExitToRobby()
    {
        EntityPool.Instance.ReturnAll();
        SceneManager.LoadScene("Robby");
    }

    public void StartLoading()
    {
        Loading.Instance.StartLoading();
    }
}
