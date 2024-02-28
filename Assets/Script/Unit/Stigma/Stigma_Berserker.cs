using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stigma_Berserker : Stigma
{
    public override void Use(BattleUnit caster)
    {
        base.Use(caster);

        if (!caster.Buff.CheckBuff(BuffEnum.Berserker))
        {
            // ���� �ִ� ���� ���� �ڵ� �ʿ�
            caster.SetBuff(gameObject.AddComponent<Buff_Berserker>());
        }
    }
}
