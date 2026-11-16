using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 花筏 1=0 本回合，伙伴造成伤害会化为格挡
    /// </summary>
    public class DriftingPetals:MiyabiCardModel
    {
        public DriftingPetals() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder),
            HoverTipFactory.Static(StaticHoverTip.Block)];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<DriftingPetalsP>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);
        }
    }
}
