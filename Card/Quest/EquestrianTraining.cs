using HoshimiMiyabi.Main;
using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
    /// 马术修行 -1 【母亲的修行】加入手牌时，抽2
    /// </summary>
    public class EquestrianTraining:MiyabiCardModel
    {
        public EquestrianTraining() : base(-1, CardType.Quest, CardRarity.Token, TargetType.None) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [MiyabiKeyWord.MotherTraining];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel clonedBy)
        {
            if(card==this && oldPileType!=PileType.Hand && Pile.Type == PileType.Hand)
            {
                await CardPileCmd.Draw(new ThrowingPlayerChoiceContext(), DynamicVars.Cards.IntValue, Owner);
            }
        }
    }
}
