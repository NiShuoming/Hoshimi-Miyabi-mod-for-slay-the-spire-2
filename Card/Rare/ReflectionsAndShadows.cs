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
    /// 镜花匿影 1 【】=【保留】获得1层【极限视域】
    /// </summary>
    public class ReflectionsAndShadows:MiyabiCardModel
    {
        public ReflectionsAndShadows() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<VitalView>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<VitalView>()];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<VitalView>(choiceContext, Owner.Creature,
                DynamicVars.Power<VitalView>().BaseValue, Owner.Creature, this);
        }
    }
}
