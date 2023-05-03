using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int itemID;
    public int enhancement;
    public Stat stat { private set; get; }

    public int GetEquipType() { return Mathf.Clamp((int)(itemID / 10000), 1, 3); }
    public int GetJobType() { return (int)((itemID % 10000) / 1000); }
}
