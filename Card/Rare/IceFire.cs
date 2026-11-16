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
    /// 冰焰 1 3（4）回合内目标触发【紊乱】时，会对所有敌人造成此次【紊乱】的伤害。
    /// </summary>
    public class IceFire:MiyabiCardModel
    {
        public IceFire() : base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("Turn",3)];

        protected override void OnUpgrade()
        {
            DynamicVars["Turn"].UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await PowerCmd.Apply<IceFireP>(choiceContext, cardPlay.Target,
                DynamicVars["Turn"].BaseValue, Owner.Creature, this);
        }
    }
}
