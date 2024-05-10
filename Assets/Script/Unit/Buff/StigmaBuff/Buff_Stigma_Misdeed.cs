using UnityEngine;

public class Buff_Stigma_Misdeed : Buff
{
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.Misdeed;

        _name = "�Ǿ�";

        _description = "���� �� �� �ž��� 1 ����߸��� �Ǿ� ������ 2�� ȹ���մϴ�";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.STIGMA;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }

    public override bool Active(BattleUnit caster)
    {
        if (!_owner.Buff.CheckBuff(BuffEnum.Malevolence))
            for (int i = 0; i < 2; i++)
                _owner.SetBuff(new Buff_Malevolence());

        return false;
    }
}