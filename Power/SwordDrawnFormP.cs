using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 获得或给予buff时，会对所有敌人造成3（4）伤害
    /// </summary>
    public class SwordDrawnFormP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
        {
            if (applier == Owner || power.Owner==Owner)
            {
                await CreatureCmd.Damage(choiceContext, CombatState.HittableEnemies,
                    Amount, MegaCrit.Sts2.Core.ValueProps.ValueProp.Unpowered, Owner);
            }
        }
    }
}
