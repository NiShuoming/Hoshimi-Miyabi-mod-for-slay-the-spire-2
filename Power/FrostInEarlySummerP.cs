using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
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
    /// 消耗【落霜】时，下回合抽3，获得1能量，每回合1次
    /// </summary>
    public class FrostInEarlySummerP : PowerModel,IZZZSecondEnergyHook
    {
        public FrostInEarlySummerP() { did = false; }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(3)];

        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        private bool did = false;

        public int ModifySecondEnergyGain(Player player, int amount, Creature giver, CardModel cardModel)
        {
            return amount;
        }

        public async Task AfterSecondEnergyChanged(Player player, int before, int after, CardModel cardModel)
        {
            if (after < before && Owner.IsPlayer && !did)
            {
                did = true;
                await PowerCmd.Apply<DrawCardsNextTurnPower>(new ThrowingPlayerChoiceContext(),
                    Owner, DynamicVars.Cards.BaseValue, Owner, null);
                await PowerCmd.Apply<EnergyNextTurnPower>(new ThrowingPlayerChoiceContext(),
                    Owner, 1, Owner, null);
            }
        }

        public int ModifyMaxSecondEnergy(Player player, int amount)
        {
            return amount;

        }

        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner))
            {
                did = false;
            }return Task.CompletedTask;
        }
    }
}
