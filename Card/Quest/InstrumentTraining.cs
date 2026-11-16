using HoshimiMiyabi.Main;
using HoshimiMiyabi.Model;
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
    /// 乐器修行 0 【母亲的修行】【虚无】【消耗】获得1能量
    /// </summary>
    public class InstrumentTraining:MiyabiCardModel
    {
        public InstrumentTraining() : base(0, CardType.Quest, CardRarity.Token, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [MiyabiKeyWord.MotherTraining, CardKeyword.Ethereal, 
            CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Energy)];

        public override int MaxUpgradeLevel => 0;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(1)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
            
        }
    }
}
