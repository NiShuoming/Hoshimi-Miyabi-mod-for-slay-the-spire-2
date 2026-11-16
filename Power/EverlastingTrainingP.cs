using HoshimiMiyabi.Card;
using HoshimiMiyabi.Main;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 每个回合开始时，将1张【任务】牌加入手中
    /// </summary>
    public class EverlastingTrainingP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner))
            {
                CardModel cardModel = CardFactory.GetDistinctForCombat(Owner.Player,
                    from c in Owner.Player.Character.CardPool.GetUnlockedCards(Owner.Player.UnlockState,
                    Owner.Player.RunState.CardMultiplayerConstraint)
                    where (c.Type==CardType.Quest)
                    select c, 1, Owner.Player.RunState.Rng.CombatCardGeneration).FirstOrDefault();
                if (cardModel != null)
                {
                    await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, Owner.Player);
                }

            }
        }
    }
}
