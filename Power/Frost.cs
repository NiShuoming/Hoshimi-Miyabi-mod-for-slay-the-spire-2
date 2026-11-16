using BaseLib.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 烈霜 被给予其他debuf时，烈霜层数+x，当层数达到5时，触发紊乱，并减少5层
    /// </summary>
    public class Frost : PowerModel, IHealthBarForecastSource
    {
        public override PowerType Type => PowerType.Debuff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public IEnumerable<HealthBarForecastSegment> GetHealthBarForecastSegments(HealthBarForecastContext context)
        {
            HealthBarForecastSegment healthBar = new HealthBarForecastSegment
                (((int)GetAmount()), new Godot.Color(0.01f, 1, 1), HealthBarForecastDirection.FromLeft);
            return [healthBar];
        }

        public decimal GetAmount()
        {
            int frostInferno = CombatState.Players.Sum(a => a.Creature.GetPowerAmount<FrostInfernoP>());
            if(frostInferno > 0) 
            return Hook.ModifyDamage(Owner.CombatState.RunState, Owner.CombatState,
                Owner, Applier, Amount*frostInferno, ZZZValueProp.Anomaly | ValueProp.Unpowered,
                 null, null, ModifyDamageHookType.All, CardPreviewMode.None, out IEnumerable<AbstractModel> _);
            else return 0;
        }

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(base.Owner))
            {
                int frostInferno = CombatState.Players.Sum(a => a.Creature.GetPowerAmount<FrostInfernoP>());

                await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, 
                    frostInferno * Amount,
                    ValueProp.Unblockable | ValueProp.Unpowered | ZZZValueProp.Anomaly,
                    Applier, null, null);

                if (!Owner.IsAlive)
                {
                    await Cmd.CustomScaledWait(0.1f, 0.25f);
                }
            }
        }

        public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
        {
            if (power == this && applier != null)
            {
                Applier = applier;
            }
            if (power == this && Amount>=5)
            {
                await ZZZCmd.Disorder(choiceContext, Owner, applier);
                await PowerCmd.Apply<Frost>(choiceContext, Owner, -5, applier, null);
            }
            else if(power!=this && power.Owner == Owner && amount > 0 && power.Type == PowerType.Debuff)
            {
                await PowerCmd.Apply<Frost>(choiceContext, Owner, Math.Min(amount, 10), applier, null);
            }
        }
    }
}
