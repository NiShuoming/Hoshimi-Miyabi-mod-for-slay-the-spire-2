using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 回合内目标触发【紊乱】时，会对所有敌人造成此次【紊乱】的伤害。
    /// </summary>
    public class IceFireP : PowerModel
    {
        public override PowerType Type => PowerType.Debuff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature dealer, CardModel cardSource)
        {
            if(target==Owner && props.HasFlag(ZZZValueProp.Disorder))
            {
                await Cmd.Wait(0.1f);
                await CreatureCmd.Damage(choiceContext, CombatState.HittableEnemies,
                    result.TotalDamage, ValueProp.Unpowered, dealer);
            }
        }

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner)) await PowerCmd.Decrement(this);
        }
    }
}
