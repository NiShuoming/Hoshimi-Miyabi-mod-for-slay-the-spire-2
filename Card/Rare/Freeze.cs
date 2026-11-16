using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
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
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 冻结 1 【消耗】击晕该敌人（如果目标有【烈霜】，触发【紊乱】）
    /// </summary>
    public class Freeze:MiyabiCardModel
    {
        public Freeze() : base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get { if (IsUpgraded) return [HoverTipFactory.FromPower<Frost>(),
                HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder)];
                return [];
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.Stun(cardPlay.Target);
            MiyabiVoiceManager.PlayAttackVoice();
            if (IsUpgraded && cardPlay.Target.HasPower<Frost>())
                await ZZZCmd.Disorder(choiceContext, cardPlay.Target, Owner.Creature);
        }
    }
}
