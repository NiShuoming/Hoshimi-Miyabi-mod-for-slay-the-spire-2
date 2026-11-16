using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 十字切 1 【紊乱】伤害提高50%/75%
    /// </summary>
    public class CrossSlash:MiyabiCardModel
    {
        public CrossSlash():base(1, CardType.Power,CardRarity.Uncommon,TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("DamageUp", 50)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder)];

        protected override void OnUpgrade()
        {
            DynamicVars["DamageUp"].UpgradeValueBy(25);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<CrossSlashP>(choiceContext, Owner.Creature,
                DynamicVars["DamageUp"].BaseValue, Owner.Creature, this);
        }
    }
}
