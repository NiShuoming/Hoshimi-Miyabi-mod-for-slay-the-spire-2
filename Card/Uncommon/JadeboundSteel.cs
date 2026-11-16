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

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 冷玉钢 1 【】=【固有】 每回合消耗1【落霜】获得1能量
    /// </summary>
    public class JadeboundSteel:MiyabiCardModel
    {
        public JadeboundSteel() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<FallenFrost>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<FallenFrost>(),
            HoverTipFactory.Static(StaticHoverTip.Energy)];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<JadeboundSteelP>(choiceContext, Owner.Creature,
                DynamicVars.Power<FallenFrost>().BaseValue, Owner.Creature, this);
        }
    }
}
