using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 休息时间 0 无法抽牌，获得2（3）能量
    /// </summary>
    public class BreakTime:MiyabiCardModel
    {
        public BreakTime() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Energy.UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Energy)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<NoDrawPower>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);

            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        }
    }
}
