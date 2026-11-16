using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 生日帽叠叠高 0 【保留】【消耗】位于手牌中时，保留手牌。使用时，丢弃所有手牌，抽取等量手牌。
    /// /// </summary>
    public class BirthdayHatStacker:MiyabiCardModel
    {
        public BirthdayHatStacker() : base(0, CardType.Quest, CardRarity.Token, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Retain, CardKeyword.Exhaust];
        public override int MaxUpgradeLevel => 0;
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var cards = PileType.Hand.GetPile(Owner).Cards.ToList();
            await CardCmd.DiscardAndDraw(choiceContext, cards, cards.Count);
        }

        public override async Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (Pile.Type != PileType.Hand) return;
            var cards = PileType.Hand.GetPile(Owner).Cards.ToList();
            foreach(var card in cards)
            {
                card.GiveSingleTurnRetain();
            }
            await Task.CompletedTask;
        }
    }
}
