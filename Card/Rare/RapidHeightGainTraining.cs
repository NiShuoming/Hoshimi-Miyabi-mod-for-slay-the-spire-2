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
    /// 迅速提升身高的修行 1=0 【消耗】在手牌中加入1张【生日帽叠叠高】
    /// </summary>
    public class RapidHeightGainTraining:MiyabiCardModel
    {
        public RapidHeightGainTraining() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromCard<BirthdayHatStacker>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var card2 = CombatState.CreateCard<BirthdayHatStacker>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Hand, Owner);

        }
    }
}
