using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 星之见 1=0 打出伙伴牌时，会将抽牌堆中随机攻击牌加入手牌
    /// </summary>
    public class AstralVision:MiyabiCardModel
    {
        public AstralVision() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Partner)];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<AstralVisionP>(choiceContext,
                Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
