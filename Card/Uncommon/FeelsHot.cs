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
    /// 手感滚烫 1 给予2【烈霜】，抽2（3）
    /// </summary>
    public class FeelsHot:MiyabiCardModel
    {
        public FeelsHot():base(1,CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<Frost>(2),new CardsVar(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Frost>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await PowerCmd.Apply<Frost>(choiceContext, cardPlay.Target,
                DynamicVars.Power<Frost>().BaseValue, Owner.Creature, this);

            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue,
                Owner);
        }
    }
}
