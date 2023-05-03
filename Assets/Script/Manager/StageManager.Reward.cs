using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageManager : MonoBehaviour
{
    public int droppedGold { private set; get; }
    public int droppedRuby { private set; get; }
    public int gainedEXP { private set; get; }

    private void ResetReward()
    {
        droppedGold = 0;
        droppedRuby = 0;
    }

    public void GainGold(int _gold)
    {
        droppedGold += _gold;
    }
    public void GainRuby(int _ruby)
    {
        droppedRuby += _ruby;
    }
    public void GainExp(int _exp)
    {
        gainedEXP += _exp;
    }
}
