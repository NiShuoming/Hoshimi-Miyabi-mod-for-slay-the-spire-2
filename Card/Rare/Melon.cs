using BaseLib.Extensions;
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
using ZZZLib.Cmds;
using ZZZLib.Powers;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 蜜瓜 0 【消耗】获得1能量、1（2）【落霜】、2【异常精通】、抽1（2）
    /// </summary>
    public class Melon:MiyabiCardModel
    {
        public Melon():base(0, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(1), new PowerVar<FallenFrost>(1),
            new PowerVar<AnomalyProficiency>(2), new CardsVar(1)];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<FallenFrost>().UpgradeValueBy(1);
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Energy),
            HoverTipFactory.FromPower<FallenFrost>(),
            HoverTipFactory.FromPower<AnomalyProficiency>()];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
            await ZZZCmd.AddSecondEnergy(Owner,
                DynamicVars.Power<FallenFrost>().IntValue,
                Owner.Creature, this);
            await PowerCmd.Apply<AnomalyProficiency>(choiceContext,
                    Owner.Creature, DynamicVars.Power<AnomalyProficiency>().BaseValue
                    ,Owner.Creature, this);
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue,
                Owner);
        }
    }
}
