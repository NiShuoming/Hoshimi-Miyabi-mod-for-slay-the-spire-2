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
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 霜炎烈灼 1 敌人回合开始时，会受到等同于4（6）倍于【烈霜】层数的【异常伤害】。
    /// </summary>
    public class FrostInferno:MiyabiCardModel
    {
        public FrostInferno() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("Dam",4)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromPower<Frost>()];

        protected override void OnUpgrade()
        {
            DynamicVars["Dam"].UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<FrostInfernoP>(choiceContext, Owner.Creature,
                DynamicVars["Dam"].BaseValue,Owner.Creature ,this);
        }
    }
}
