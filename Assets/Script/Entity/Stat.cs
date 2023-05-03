using System;

[System.Serializable]
public class Stat
{
    public float wAtk;
    public float mAtk;
    public float atkSpeed;
    public float atkRange;
    public float criProb;
    public float criDamage;
    public float maxHp;
    public float defensive;
    public float coolTimeReduction;
    public float physicalLifeSteal;
    public float magicalLifeSteal;
    public float physicalDamage;
    public float magicalDamage;

    public Stat()
    {
        wAtk                = 0f;
        mAtk                = 0f;
        atkSpeed            = 0f;
        atkRange            = 0f;
        criProb             = 0f;
        criDamage           = 0f;
        maxHp               = 0f;
        defensive           = 0f;
        coolTimeReduction   = 0f;
        physicalLifeSteal   = 0f;
        magicalLifeSteal    = 0f;
        physicalDamage      = 0f;
        magicalDamage       = 0f;
    }
    public Stat(float _wAtk, float _mAtk, float _atkSpeed, float _atkRange, float _criProb, float _criDamage, float _maxHp, float _defensive,
        float _coolTimeReduction, float _physicalLifeSteal, float _magicalLifeSteal,
        float _physicalDamage, float _magicalDamage)
    {
        wAtk                = _wAtk;
        mAtk                = _mAtk;
        atkSpeed            = _atkSpeed;
        atkRange            = _atkRange;
        criProb             = _criProb;
        criDamage           = _criDamage;
        maxHp               = _maxHp;
        defensive           = _defensive;
        coolTimeReduction   = _coolTimeReduction;
        physicalLifeSteal   = _physicalLifeSteal;
        magicalLifeSteal    = _magicalLifeSteal;
        physicalDamage      = _physicalDamage;
        magicalDamage       = _magicalDamage;
    }

    public static Stat operator +(Stat a) => a;
    public static Stat operator -(Stat a) => new Stat(-a.wAtk, -a.mAtk, -a.atkSpeed, -a.atkRange, -a.criProb, -a.criDamage, -a.maxHp, -a.defensive,
        a.coolTimeReduction, -a.physicalLifeSteal, -a.magicalLifeSteal, -
        a.physicalDamage, -a.magicalDamage);

    public static Stat operator +(Stat a, Stat b)
        => new Stat(a.wAtk + b.wAtk, a.mAtk + b.mAtk, a.atkSpeed + b.atkSpeed, a.atkRange + b.atkRange, a.criProb + b.criProb, 
            a.criDamage + b.criDamage, a.maxHp + b.maxHp, a.defensive + b.defensive,
        a.coolTimeReduction + b.coolTimeReduction, a.physicalLifeSteal + b.physicalLifeSteal, a.magicalLifeSteal + b.magicalLifeSteal, 
        a.physicalDamage + b.physicalDamage, a.magicalDamage + b.magicalDamage);

    public static Stat operator -(Stat a, Stat b)
        => a + (-b);


    public static Stat operator *(Stat a, float b)
        => new Stat(a.wAtk * b, a.mAtk * b, a.atkSpeed * b, a.atkRange * b, a.criProb * b, a.criDamage * b, a.maxHp * b, a.defensive * b,
        a.coolTimeReduction * b, a.physicalLifeSteal * b, a.magicalLifeSteal * b, 
        a.physicalDamage * b, a.magicalDamage * b);

    public static Stat operator /(Stat a, float b)
    {
        if (b == 0f)
        {
            throw new DivideByZeroException();
        }
        return new Stat(a.wAtk / b, a.mAtk / b, a.atkSpeed / b, a.atkRange / b, a.criProb / b, a.criDamage / b, a.maxHp / b, a.defensive / b,
        a.coolTimeReduction / b, a.physicalLifeSteal / b, a.magicalLifeSteal / b, 
        a.physicalDamage / b, a.magicalDamage / b);
    }
    public override string ToString()
    {
        return (String.Format("(wAtk : {0}, mAtk : {1}, atkSpeed : {2}, atkRange : {3}, criProb : {4}, criDamage : {5}, maxHp : {6}, defensive : {7}" +
            ", coolTimeReduction : {8}, physicalLifeSteal : {9}, magicalLifeSteal : {10}, " +
            "physicalDamage : {12}, magicalDamage : {13})",
            wAtk, mAtk, atkSpeed, atkRange, criProb, criDamage, maxHp, defensive,
        coolTimeReduction, physicalLifeSteal, magicalLifeSteal,
        physicalDamage, magicalDamage));
    }

    public string ToShortDesc()
    {
        return (String.Format("공격력 : \t{0:#,0}\n" +
            "주문력 : \t{1:#,0}\n" +
            "공격속도 : \t{2:#,0}\n" +
            "사거리 : \t{3:#,0}\n" +
            "치명타율 : \t{4:#,0f}\n" +
            "체력 : \t{5:#,0}\n" +
            "방어력 : \t{6:#,0}\n" +
            "쿨타임 감소 : \t{7:#,0}\n", 
            wAtk, mAtk, atkSpeed, atkRange, criProb, maxHp, defensive, coolTimeReduction));
    }

    public string ToDetailKey()
    {
        return (String.Format("공격력\n" +
            "주문력\n" +
            "공격속도\n" +
            "사거리\n" +
            "치명타율\n" +
            "치명타 데미지\n" +
            "체력\n" +
            "방어력\n" +
            "쿨타임 감소\n" +
            "물리 흡혈\n" +
            "마법 흡혈\n" +
            "물리 데미지\n" +
            "마법 데미지"));
    }
    public string ToDetailValue()
    {
        return (String.Format(": {0:#,0}\n" +
            ": {1:#,0}\n" +
            ": {2:#,0}\n" +
            ": {3:#,0}\n" +
            ": {4:#,0}%\n" +
            ": {5:#,0}%\n" +
            ": {6:#,0}\n" +
            ": {7:#,0}\n" +
            ": {8:#,0}%\n" +
            ": {9:#,0}%\n" +
            ": {10:#,0}%\n" +
            ": {11:#,0}%\n" +
            ": {12:#,0}%\n",
            wAtk, mAtk, atkSpeed, atkRange, criProb, criDamage, maxHp, defensive,
        coolTimeReduction, physicalLifeSteal, magicalLifeSteal,
        physicalDamage, magicalDamage));
    }
    public string ToJson()
    {
        string jsonData = "{";

                                                                      
        if (wAtk                != 0f) jsonData += "\"wAtk\" : " + wAtk              + ", ";
        if (mAtk                != 0f) jsonData += "\"mAtk\" : " + mAtk              + ", ";
        if (atkSpeed            != 0f) jsonData += "\"atkSpeed\" : " + atkSpeed          + ", ";
        if (atkRange            != 0f) jsonData += "\"atkRange\" : " + atkRange          + ", ";
        if (criProb             != 0f) jsonData += "\"criProb\" : " + criProb           + ", ";
        if (criDamage           != 0f) jsonData += "\"criDamage\" : " + criDamage         + ", ";
        if (maxHp               != 0f) jsonData += "\"maxHp\" : " + maxHp             + ", ";
        if (defensive           != 0f) jsonData += "\"defensive\" : " + defensive         + ", ";
        if (coolTimeReduction   != 0f) jsonData += "\"coolTimeReduction\" : " + coolTimeReduction + ", ";
        if (physicalLifeSteal   != 0f) jsonData += "\"physicalLifeSteal\" : " + physicalLifeSteal + ", ";
        if (magicalLifeSteal    != 0f) jsonData += "\"magicalLifeSteal\" : " + magicalLifeSteal  + ", ";
        if (physicalDamage      != 0f) jsonData += "\"physicalDamage\" : " + physicalDamage    + ", ";
        if (magicalDamage       != 0f) jsonData += "\"magicalDamage\" : " + magicalDamage     + ", ";

        if (jsonData.Length > 1) jsonData = jsonData.Remove(jsonData.Length - 2);
        jsonData += "}";
        return jsonData;
    }
}

[System.Serializable]
public class EnemyStat
{
    public float moveSpeed;

}