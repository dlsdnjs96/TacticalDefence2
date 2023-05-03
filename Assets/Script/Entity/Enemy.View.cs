using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Enemy : Entity
{
    private ParticleSystem  hurt;




    public void OnDead()
    {
        if (gameManager == null) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.KilledEnemy(entityID);
    }
}

