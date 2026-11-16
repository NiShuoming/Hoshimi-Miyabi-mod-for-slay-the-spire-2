using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 成为英雄的旅程 打出攻击牌时，可以随机消耗手牌中的1张状态牌或诅咒牌，获得1落霜，并抽1、
    /// </summary>
    public class ThePathToBecomingAHeroP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if(cardPlay.Player.Creature==Owner && cardPlay.Card.Type == CardType.Attack)
            {
                for(int i = 0; i < Amount; i++)
                {
                    var cards = PileType.Hand.GetPile(Owner.Player).Cards.Where(
                        a => (a.Type == CardType.Status || a.Type == CardType.Curse));

                    if(cards.Any())
                    {
                        var card = CombatState.RunState.Rng.CombatCardSelection.NextItem(cards);
                        await CardCmd.Exhaust(choiceContext,card);
                        await ZZZCmd.AddSecondEnergy(Owner.Player, 1, Owner, null);
                        await CardPileCmd.Draw(choiceContext, Owner.Player);
                    }
                }
            }
        }
    }
}
