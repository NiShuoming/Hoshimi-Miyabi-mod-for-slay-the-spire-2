using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 虚狩·心念无我 2 触发【紊乱】时，获得1能量，每回合最多4次。
    /// </summary>
    public class IsshinMugaP : PowerModel,IZZZTriggleAnomalyHook
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

        public override int DisplayAmount => base.DynamicVars["Times"].IntValue;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(1), new DynamicVar("Times", 4m)];

        public async Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results)
        {
            if(applier==Owner && keyWord == ZZZKeyWord.Disorder 
                && DynamicVars["Times"].BaseValue>0 && Owner.IsPlayer)
            {
                DynamicVars["Times"].BaseValue--;
                InvokeDisplayAmountChanged();
                await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner.Player);
            }
        }

        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (!participants.Contains(base.Owner))
            {
                return Task.CompletedTask;
            }
            base.DynamicVars["Times"].BaseValue = 4m;
            InvokeDisplayAmountChanged();
            return Task.CompletedTask;
        }

    }
}
