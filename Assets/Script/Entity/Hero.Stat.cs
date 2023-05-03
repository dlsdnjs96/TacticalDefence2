using UnityEngine;


public partial class Hero : Entity
{
    public HeroInfo heroInfo;
    public Equipment[] equipment;

    private void CalculateLevelStat()
    {
        if (stat == null) stat = new Stat();
        stat = StatInformation.info[heroInfo.heroID].baseStat +
            (StatInformation.info[entityID].bonusStat * heroInfo.level);
    }
    public void Equip(Equipment _equipment)
    {
        equipment[_equipment.GetEquipType()] = _equipment;
    }
    public void UnEquip(int _slotNumber)
    {
        _slotNumber = Mathf.Clamp(0, 2, _slotNumber);
    }
    private void CalculateEquip()
    {
        for (int i = 0;i < equipment.Length; i++)
        {
            if (equipment[i] != null) stat += equipment[i].stat;
        }
    }    
    private void CalculateStat()
    {
        CalculateLevelStat();
        CalculateEquip();
        animator.SetFloat("AtkSpeed", stat.atkSpeed);
    }
    public void ApplyHeroInfo(HeroInfo _heroInfo)
    {
        heroInfo = _heroInfo;
        entityID = heroInfo.heroID;
        CalculateStat();
    }
}
