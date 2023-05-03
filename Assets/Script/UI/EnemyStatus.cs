using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public Entity target;
    private RectTransform rectTransform;

    private Image backImg;
    private Image hpImg;
    private Image shieldImg;





    void Awake()
    {
        rectTransform   = GetComponent<RectTransform>();
        backImg         = GetComponent<Image>();
        hpImg           = transform.Find("Hp").GetComponent<Image>();
        shieldImg       = transform.Find("Shield").GetComponent<Image>();
    }



    void Update()
    {
        rectTransform.position = Camera.main.WorldToScreenPoint(target.statusObj.transform.position);
    }

    public void InitializeHP(Entity _target)
    {
        target                  = _target;
        hpImg.fillAmount        = 0f;
        shieldImg.fillAmount    = 0f;
        backImg.enabled         = false;
    }


    public void UpdateHp()
    {
        backImg.enabled = true;
        if (target.shield > 0f)
        {
            if (target.shield + target.hp > target.stat.maxHp)
            {
                hpImg.fillAmount = target.shield + target.hp;
                shieldImg.fillAmount = 1f;
            }
            else
            {
                hpImg.fillAmount = target.hp / target.stat.maxHp;
                shieldImg.fillAmount = (target.shield + target.hp) / target.stat.maxHp;
            }
        }
        else
        {
            hpImg.fillAmount = target.hp / target.stat.maxHp;
            shieldImg.fillAmount = 0f;
        }
    }
}
