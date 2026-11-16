using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 纳刀术 回合开始时，手牌中每有1张升级过的牌，获得2（3）格挡
    /// </summary>
    public class ArtOfSheathingP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterSideTurnStartLate(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if(participants.Contains(Owner) && Owner.IsPlayer)
            {
                int cards = PileType.Hand.GetPile(Owner.Player).Cards
                    .Count(a=>a.IsUpgraded);

                await CreatureCmd.GainBlock(Owner, Amount * cards, 
                    MegaCrit.Sts2.Core.ValueProps.ValueProp.Unpowered,
                    null);
            }
        }
    }
}
