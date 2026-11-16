using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 狐火 0 【消耗】=》【不消耗】升级所有手牌
    /// </summary>
    public class Kitsunebi:MiyabiCardModel
    {
        public Kitsunebi():base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override void OnUpgrade()
        {
            RemoveKeyword(CardKeyword.Exhaust);
        }


        protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var list = PileType.Hand.GetPile(Owner).Cards;
            foreach(var item in list)
            {
                if(item.IsUpgradable)CardCmd.Upgrade(item);
            }
            return Task.CompletedTask;
        }
    }
}
