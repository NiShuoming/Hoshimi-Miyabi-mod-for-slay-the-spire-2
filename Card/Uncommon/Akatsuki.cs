using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 晓 1 抽2(3)，升级并本回合保留抽到的牌。
    /// </summary>
    public class Akatsuki:MiyabiCardModel
    {
        public Akatsuki() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var cards = await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue,
                Owner);

            foreach(var card in cards)
            {
                if(card.IsUpgradable) CardCmd.Upgrade(card);
                card.GiveSingleTurnRetain();
            }
        }
    }
}
