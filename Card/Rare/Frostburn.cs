using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 霜灼 1 【消耗】=【】清除目标身上的【烈霜】获得等量能量，抽2。
    /// </summary>
    public class Frostburn:MiyabiCardModel
    {
        public Frostburn() : base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Frost>(),
            HoverTipFactory.Static(StaticHoverTip.Energy)];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2)];

        protected override void OnUpgrade()
        {
            RemoveKeyword(CardKeyword.Exhaust);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            int n = cardPlay.Target.GetPowerAmount<Frost>();

            await PlayerCmd.GainEnergy(n,Owner);
            await PowerCmd.Apply<Frost>(choiceContext, cardPlay.Target,
                -n, Owner.Creature, this);
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }
    }
}
