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
    /// 观影修行 0 抽牌堆顶部中添加1张【《十万次心跳》】（回能）
    /// </summary>
    public class MovieWatchingTraining:MiyabiCardModel
    {
        public MovieWatchingTraining() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips {
            get { if (IsUpgraded) return [HoverTipFactory.FromCard<TheHeartbeat>(true)];
                return [HoverTipFactory.FromCard<TheHeartbeat>()];
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var card2 = CombatState.CreateCard<TheHeartbeat>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Draw, Owner, CardPilePosition.Top);
            if (IsUpgraded) CardCmd.Upgrade(card2);
        }
    }
}
