using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUnit
{
    public string name { get; }
    public int DarkEssence { get; set; }
    public Sprite image { get; }

    public RewardUnit(string name, int DarkEssence,Sprite image)
    {
        this.name = name;
        this.DarkEssence = DarkEssence;
        this.image = image;
    }
} 
public class RewardController
{
    #region ����
    Dictionary<int, RewardUnit> dic_units;
    int prev_DarkEssence;
    #endregion
    #region �Լ�
    public void Init(List<DeckUnit> units, int DarkEssence)
    {
        this.dic_units = new Dictionary<int, RewardUnit>();
        this.prev_DarkEssence = DarkEssence;

        foreach (DeckUnit unit in units)
        {
            dic_units.Add(unit.UnitID, new RewardUnit(unit.Data.Name, unit.DeckUnitStat.FallCurrentCount, 
                GameManager.Resource.Load<Sprite>($"Arts/Units/Unit_Portrait/" + unit.Data.Name + "_Ÿ��_Portrait")));
        }
    }
    public void RewardSetting(List<DeckUnit> units, UI_RewardScene rewardScene)//����������� �� �������ֱ�
    {
        rewardScene.Init(units.Count,GameManager.Data.DarkEssense - prev_DarkEssence);
        RewardUnit unit,fallunit;
        for (int i=0;i<units.Count;++i)
        {
            if(dic_units.TryGetValue(units[i].UnitID,out unit))//������ �ִ� ģ����
            {
                unit.DarkEssence -= units[i].DeckUnitStat.FallCurrentCount;
                rewardScene.setContent(i, unit);
                dic_units.Remove(units[i].UnitID);
            }
            else//���� �÷��̾� ���� ���� ģ����
            {
                
                fallunit = new RewardUnit(units[i].Data.Name, units[i].Data.RawStat.FallMaxCount-units[i].Data.RawStat.FallCurrentCount-units[i].DeckUnitStat.FallCurrentCount, 
                    GameManager.Resource.Load<Sprite>($"Arts/Units/Unit_Portrait/" + units[i].Data.Name + "_Ÿ��_Portrait"));
                rewardScene.setContent(i, fallunit,true);
            }
        }
        foreach(var u in dic_units)//���� ģ����
        {
            unit = u.Value;
            Debug.Log("���� ģ���� : " + unit.name);
        }
        dic_units.Clear();
    }
   
    #endregion
}