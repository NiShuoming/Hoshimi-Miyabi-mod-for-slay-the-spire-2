using Godot;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
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
    /// 同沐霜雪 初始获得3【落霜】，触发紊乱时，获得2【落霜】，消耗的【落霜】减半
    /// </summary>
    public class BaskInFrost : MiyabiRelicModel, IZZZTriggleAnomalyHook, IZZZSecondEnergyHook
    {
        public override RelicRarity Rarity => RelicRarity.Starter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<FallenFrost>(),
            HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder)];

        public Task AfterSecondEnergyChanged(Player player, int before, int after, CardModel cardModel)
        {
            return Task.CompletedTask;
        }

        public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (combatState.RoundNumber == 1 && participants.Contains(Owner.Creature))
            {
                await ZZZCmd.AddSecondEnergy(Owner, 3, Owner.Creature, null);
            }
        }

        public int ModifyMaxSecondEnergy(Player player, int amount)
        {
            return amount;
        }

        public int ModifySecondEnergyGain(Player player, int amount, Creature giver, CardModel cardModel)
        {
            if(amount < 0) return amount /2;
            return amount;
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
