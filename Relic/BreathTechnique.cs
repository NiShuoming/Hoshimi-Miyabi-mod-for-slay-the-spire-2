using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Relic
{
    /// <summary>
    /// 吐纳法 初始额外获得3【落霜】
    /// </summary>
    public class BreathTechnique : MiyabiRelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Uncommon;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [HoverTipFactory.FromPower<FallenFrost>()];


        public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (combatState.RoundNumber == 1 && participants.Contains(Owner.Creature))
            {
                await ZZZCmd.AddSecondEnergy(Owner, 3, Owner.Creature, null);
            }
        }

    }
}
