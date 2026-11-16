using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Relic
{
    /// <summary>
    /// 心间舞雀 战斗开始时获得1层【极限视域】
    /// </summary>
    public class DanceOfTheSparrows : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Shop;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<VitalView>()];

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if(combatState.RoundNumber==1 && participants.Contains(Owner.Creature))
            {
                await PowerCmd.Apply<VitalView>(new ThrowingPlayerChoiceContext(),
                    Owner.Creature, 1, Owner.Creature, null);
            }
        }
    }
}
