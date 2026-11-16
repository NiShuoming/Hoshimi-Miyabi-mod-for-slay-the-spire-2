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
using ZZZLib;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 星之见 打出伙伴牌时，会将抽牌堆中随机攻击牌加入手牌
    /// </summary>
    public class AstralVisionP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if(cardPlay.Player.Creature==Owner && cardPlay.Card.Keywords.Contains(ZZZKeyWord
                .Partner))
            {
                for(int i =0;i<Amount;i++)
                {
                    var cards = PileType.Draw.GetPile(Owner.Player).Cards.
                        Where(a => a.Type == CardType.Attack);
                    if (!cards.Any()) return;
                    var card = CombatState.RunState.Rng.CombatCardSelection.
                        NextItem<CardModel>(cards);
                    await CardPileCmd.Add(card, PileType.Hand.GetPile(Owner.Player));
                }
            }
        }
    }
}
