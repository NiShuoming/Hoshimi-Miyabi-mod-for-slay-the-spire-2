using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 星见雅后援会 1 【架势】【消耗】给予3【易伤】=》给予3【虚弱】
    /// </summary>
    public class MiyabiFanClub:MiyabiCardModel
    {
        public MiyabiFanClub() : base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<VulnerablePower>(3), new PowerVar<WeakPower>(3)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Stance, CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get { if (IsUpgraded) return [HoverTipFactory.FromPower<WeakPower>()];
                return [HoverTipFactory.FromPower<VulnerablePower>()];
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            if (!IsUpgraded) await PowerCmd.Apply<VulnerablePower>(choiceContext,
                cardPlay.Target, DynamicVars.Power<VulnerablePower>().BaseValue,
                Owner.Creature, this);
            else await PowerCmd.Apply<WeakPower>(choiceContext,
                cardPlay.Target, DynamicVars.Power<WeakPower>().BaseValue,
                Owner.Creature, this);
        }
    }
}
