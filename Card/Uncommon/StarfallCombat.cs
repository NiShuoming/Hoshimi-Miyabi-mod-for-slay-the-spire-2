using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 星流演武 1=0 抽到【架势】、任务牌时，抽1
    /// </summary>
    public class StarfallCombat:MiyabiCardModel
    {
        public StarfallCombat() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Stance)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<StarfallCombatB>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);
        }
    }
}
