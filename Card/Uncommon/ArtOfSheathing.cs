using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 纳刀术 1 回合开始时，手牌中每有1张升级过的牌，获得2（3）格挡
    /// </summary>
    public class ArtOfSheathing:MiyabiCardModel
    {
        public ArtOfSheathing() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("Blo",2)];

        protected override void OnUpgrade()
        {
            DynamicVars["Blo"].UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Block)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<ArtOfSheathingP>(choiceContext, Owner.Creature,
                DynamicVars["Blo"].BaseValue, Owner.Creature, this);
        }
    }
}
