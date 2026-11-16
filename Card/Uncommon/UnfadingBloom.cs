using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 华不败 1=0 获得当前格挡值1/2的格挡
    /// </summary>
    public class UnfadingBloom:MiyabiCardModel
    {
        public UnfadingBloom() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Block)];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, Owner.Creature.Block / 2m,
                ValueProp.Unpowered, cardPlay);
        }
    }
}
