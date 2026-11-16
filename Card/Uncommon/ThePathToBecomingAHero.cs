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
    /// 成为英雄的旅程 2=1 打出攻击牌时，可以随机消耗手牌中的1张状态牌或诅咒牌，获得1落霜,并抽1
    /// </summary>
    public class ThePathToBecomingAHero:MiyabiCardModel
    {
        public ThePathToBecomingAHero() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
            HoverTipFactory.FromPower<FallenFrost>()];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<FallenFrost>(1)];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<ThePathToBecomingAHeroP>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);
        }
    }
}
