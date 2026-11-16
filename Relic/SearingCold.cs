using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Relic
{
    /// <summary>
    /// 寒炎 初始获得3【落霜】，触发紊乱时，获得2【落霜】
    /// </summary>
    public class SearingCold : MiyabiRelicModel, IZZZTriggleAnomalyHook
    {
        public override RelicRarity Rarity => RelicRarity.Starter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [HoverTipFactory.FromPower<FallenFrost>(),
            HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder)];

        public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if(combatState.RoundNumber==1 && participants.Contains(Owner.Creature))
            {
                await ZZZCmd.AddSecondEnergy(Owner, 3, Owner.Creature, null);
            }
        }

        public async Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results)
        {
            if (keyWord == ZZZKeyWord.Disorder)
            {
                await ZZZCmd.AddSecondEnergy(Owner, 2, Owner.Creature, null);
            }
        }

        
    }
}
