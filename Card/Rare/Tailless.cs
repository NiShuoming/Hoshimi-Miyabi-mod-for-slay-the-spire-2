using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
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
    /// 讨骸刀·无尾 2 【架势】造成20(22)异常伤害，每有2【落霜】获得1能量（额外造成1次伤害）。
    /// </summary>
    public class Tailless:MiyabiCardModel
    {
        public Tailless() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<FallenFrost>(1), new DamageVar(20, ValueProp.Move|ZZZValueProp.Anomaly)
            , new CalculationBaseVar(0), new CalculationExtraVar(1),
            new CalculatedVar("N").WithMultiplier((card,_)=>
            ZZZCmd.GetSecondEnergy(card.Owner).Energy2/2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
        }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Stance];

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                if (IsUpgraded) return [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
                HoverTipFactory.FromPower<FallenFrost>()];
                return [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
                HoverTipFactory.FromPower<FallenFrost>(),
                HoverTipFactory.Static(StaticHoverTip.Energy)];
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            int n = ((int)((CalculatedVar)DynamicVars["N"]).Calculate(Owner.Creature));
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                .WithHitCount(IsUpgraded?n+1:1).Execute(choiceContext);
            if (!IsUpgraded) await PlayerCmd.GainEnergy(n, Owner);
        }
    }
}
