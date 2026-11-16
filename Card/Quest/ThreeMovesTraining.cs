using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 过三招的修行 -1 【架势】【极限突破】【消耗】回合结束时，随机造成20（+3）点伤害
    /// </summary>
    public class ThreeMovesTraining:MiyabiCardModel
    {
        public ThreeMovesTraining() : base(-1, CardType.Quest, CardRarity.Token, TargetType.RandomEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(20,ValueProp.Unpowered)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(5);
        }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust, ZZZKeyWord.Stance, ZZZKeyWord.OverLimit];

        public override int MaxUpgradeLevel => 99;

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").TargetingRandomOpponents(CombatState)
                .Execute(choiceContext);

        }

        public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner.Creature) && Pile.Type==PileType.Hand)
            {
                await CardCmd.AutoPlay(choiceContext, this, null);
            }
        }
    }
}
