using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 身法修行 1 【】=【固有】【消耗】获得1张【不可踏入】
    /// </summary>
    public class FootworkTraining:MiyabiCardModel
    {
        public FootworkTraining() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromCard<NoEntry>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var card2 = CombatState.CreateCard<NoEntry>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Hand, Owner);
        }
    }
}
