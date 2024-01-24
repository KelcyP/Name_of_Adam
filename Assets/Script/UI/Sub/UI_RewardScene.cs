using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_RewardScene : MonoBehaviour
{
    #region ����
    [SerializeField] private List<GameObject> contents;//�ϴ� 10�������� �����ϵ��� �ϴ� ������.
    [SerializeField] private TMP_Text darkness;

    private int count=10;
    public bool FadeEnd = false;
    #endregion

    #region �Լ�
    public void Init(int count,int changeDarkness)
    {
        this.GetComponent<FadeController>().StartFadeIn();
        if (count > 10) this.count = 10;
        else this.count = count;
        for(int i = 0; i < this.count; ++i)
        {
            contents[i].SetActive(true);
        }
        darkness.text = changeDarkness.ToString();
    }
    public void setContent(int idx, RewardUnit rewardUnit,bool isNew = false)
    {
        UI_UnitReward content = contents[idx].GetComponent<UI_UnitReward>();
        content.Init(rewardUnit.image, rewardUnit.name, rewardUnit.DarkEssence, isNew);
        FadeEnd = content.FadeIn((float)idx,count);
    }
    public void EndFadeIn()
    {
        FadeEnd = true;
        for (int i = 0; i < this.count; i++)
            contents[i].GetComponent<UI_UnitReward>().EndFadeIn();
        //StopAllCoroutines();
    }
    #endregion
}