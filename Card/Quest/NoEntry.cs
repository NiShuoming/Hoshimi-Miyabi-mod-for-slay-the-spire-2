using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 不可踏入 -1 加入手牌时，单数牌本回合能量消耗-1。
    /// </summary>
    public class NoEntry:MiyabiCardModel
    {
        public NoEntry():base(-1, CardType.Quest,CardRarity.Token,TargetType.None) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Energy)];

        public override int MaxUpgradeLevel => 0;

        public override Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel clonedBy)
        {
            if (card == this && card.Pile.Type==PileType.Hand && oldPileType != PileType.Hand)
            {
                var cards = PileType.Hand.GetPile(Owner).Cards.ToList();
                for(int i = 0; i < cards.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        cards[i].EnergyCost.AddThisTurnOrUntilPlayed(-1);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
