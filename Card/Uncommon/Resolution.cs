using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
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
    /// 绝念 1=2 【架势】造成8伤害。下回合获得2能量（造成15异常伤害，给予1【易伤】1【脆弱】）
    /// </summary>
    public class Resolution:MiyabiCardModel
    {
        public Resolution() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Stance];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(8, ValueProp.Move), new EnergyVar(2),
            new DamageVar("Damage2",15,ValueProp.Move|ZZZValueProp.Anomaly),
            new PowerVar<VulnerablePower>(1), new PowerVar<FrailPower>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips{
            get { if (IsUpgraded) 
                    return [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)
                ,HoverTipFactory.FromPower<VulnerablePower>(),
                HoverTipFactory.FromPower<FrailPower>()];

                return [HoverTipFactory.Static(StaticHoverTip.Energy)];
            }
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            if (IsUpgraded)
            {
                await DamageCmd.Attack(base.DynamicVars["Damage2"].BaseValue)
                    .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                    .Execute(choiceContext);
                await PowerCmd.Apply<VulnerablePower>(choiceContext, cardPlay.Target,
                    DynamicVars.Power<VulnerablePower>().BaseValue, Owner.Creature, this);
                await PowerCmd.Apply<FrailPower>(choiceContext, cardPlay.Target,
                     DynamicVars.Power<FrailPower>().BaseValue, Owner.Creature, this);
            }
            else
            {
                await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                    .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                    .Execute(choiceContext);
                await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext,
                    Owner.Creature, DynamicVars.Energy.BaseValue, Owner.Creature, this);
            }
        }
    }
}
