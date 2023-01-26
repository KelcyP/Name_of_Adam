using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Attack", menuName = "Scriptable Object/Effect_Attack", order = 3)]
public class Effect_Attack : EffectSO
{

    // 공격 실행
    public override void Effect(BattleUnit caster, List<BattleUnit> battleUnits)
    {
        FieldManager _FieldMNG = GameManager.Instance.FieldMNG;
        List<Vector2> RangeList = GetRange();
        List<BattleUnit> targetUnits = new List<BattleUnit>();

        if (attackType == AttackType.rangeAttack)
        {
            // 공격범위 안에 있는 모든 대상을 리스트에 넣는다.
            for (int i = 0; i < RangeList.Count; i++)
            {
                int x = caster.UnitMove.LocX - (int)RangeList[i].x;
                int y = caster.UnitMove.LocY - (int)RangeList[i].y;

                // 공격 범위가 필드를 벗어나지 않았다면 범위 위의 적 유닛을 가져온다
                BattleUnit unit = _FieldMNG.GetTargetUnit(x, y);
                if(unit != null && caster.BattleUnitSO.MyTeam != unit.BattleUnitSO.MyTeam)
                    targetUnits.Add(unit); 
            }
        }
        else if (attackType == AttackType.targeting)
        {

            int x = (int)caster.SelectTile.x;
            int y = (int)caster.SelectTile.y;

            if (x == -1 && y == -1)
            {
                x = caster.UnitMove.LocX;
                y = caster.UnitMove.LocY;
            }

            BattleUnit unit = _FieldMNG.GetTargetUnit(x, y);
            if (unit != null && caster.BattleUnitSO.MyTeam != unit.BattleUnitSO.MyTeam)
                targetUnits.Add(unit);
        }
        caster.UnitAction.OnAttack(targetUnits);
    }
}

// 23.01.25 김종석
// Effect_Attack에서 처리하던 타겟 서칭을 SkillSO로 이동