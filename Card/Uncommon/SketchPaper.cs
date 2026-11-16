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
    /// 草稿纸 1 将1张【下井字棋】加入手牌（抽牌）
    /// </summary>
    public class SketchPaper : MiyabiCardModel
    {
        public SketchPaper() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get { if (IsUpgraded) return [HoverTipFactory.FromCard<PlayTicTacToe>(true)];
                return [HoverTipFactory.FromCard<PlayTicTacToe>()];
            }
        }
         

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var card2 = CombatState.CreateCard<PlayTicTacToe>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Hand, Owner);
            if (IsUpgraded) CardCmd.Upgrade(card2);
        }
    }
}
