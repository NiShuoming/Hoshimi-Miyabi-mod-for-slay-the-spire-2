using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Powers;

namespace HoshimiMiyabi.Relic
{
    /// <summary>
    /// 皲裂 在第二回合开始时，获得3【异常精通】
    /// </summary>
    public class Rupture : MiyabiRelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Rare;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<AnomalyProficiency>(3)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<AnomalyProficiency>()];

        public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if(combatState.RoundNumber==2 && participants.Contains(Owner.Creature))
            {
                await PowerCmd.Apply<AnomalyProficiency>(choiceContext,
                    Owner.Creature, DynamicVars.Power<AnomalyProficiency>().BaseValue,
                    Owner.Creature, null);
            }
        }
    }
}
