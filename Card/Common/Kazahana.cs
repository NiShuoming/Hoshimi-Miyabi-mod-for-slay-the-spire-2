using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 风花 0 【架势】【极限突破】造成4点伤害，给予1层【烈霜】
    /// </summary>
    public class Kazahana:MiyabiCardModel
    {
        public Kazahana() : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Stance,ZZZKeyWord.OverLimit];

        public override int MaxUpgradeLevel => 99;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(4, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move),
            new PowerVar<Frost>(1)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                .Execute(choiceContext);
                await PowerCmd.Apply<Frost>(choiceContext, cardPlay.Target,
                    DynamicVars.Power<Frost>().BaseValue, Owner.Creature, this);
        }
    }
}
