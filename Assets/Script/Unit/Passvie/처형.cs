using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class 처형 : Passive
{
    public override void Use(BattleUnit caster, BattleUnit receiver)
    {
        if (receiver.HP.GetCurrentHP() <= 10)
        {
            receiver.ChangeHP(-receiver.HP.GetCurrentHP());
        }
    }
}