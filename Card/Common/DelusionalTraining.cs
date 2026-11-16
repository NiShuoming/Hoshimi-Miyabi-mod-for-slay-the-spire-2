using HoshimiMiyabi.Model;
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
    /// 脑补修行 1=0 分别在手牌、抽排堆顶加入1张【与猫搏斗】
    /// </summary>
    public class DelusionalTraining:MiyabiCardModel
    {
        public DelusionalTraining() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromCard<FightingCats>()];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var card = CombatState.CreateCard<FightingCats>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Draw, Owner, CardPilePosition.Top);

            var card2 = CombatState.CreateCard<FightingCats>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Hand, Owner);
        }
    }
}
