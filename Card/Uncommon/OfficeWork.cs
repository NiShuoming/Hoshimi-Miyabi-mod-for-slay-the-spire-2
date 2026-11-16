using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 办公 1=0 手牌中加入1张【保养佩刀】
    /// </summary>
    public class OfficeWork:MiyabiCardModel
    {
        public OfficeWork() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromCard<PolishSword>()];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var card2 = CombatState.CreateCard<PolishSword>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Hand, Owner);
        }
    }
}
