using BaseLib.Extensions;
using HoshimiMiyabi.Main;
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
    /// 霜月残雪 2 【霜月架势】造成10（+2）【异常伤害】1次。2：攻击次数+1, 4：获得2【能量】，获得3【落霜】。6：攻击次数+1，改为群伤。
    /// </summary>
    public class SnowDebris:MiyabiCardModel
    {
        public SnowDebris() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(10, ValueProp.Move|ZZZValueProp.Anomaly),
            new EnergyVar(2), new PowerVar<FallenFrost>(3)];

        public override int MaxUpgradeLevel => 99;

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [MiyabiKeyWord.ShimotsukiStance];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Stance),
            HoverTipFactory.FromKeyword(ZZZKeyWord.OverLimit),
            HoverTipFactory.Static(StaticHoverTip.Energy),
            HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromPower<FallenFrost>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            int n = ZZZCmd.GetSecondEnergy(Owner).Energy2;
            int repeat = 1;

            if (n >= 2)
            {
                repeat += 1;
            }
            await MiyabiVoiceManager.PlayChargeVoice(n / 2, Owner, this);
            MiyabiVoiceManager.PlayShuangyueVoice();
            if (n >= 4)
            {
                await ZZZCmd.AddSecondEnergy(Owner, DynamicVars.Power<FallenFrost>().IntValue
                    , Owner.Creature, this);
                await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
            }
            if (n >= 6)
            {
                await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                    .TargetingAllOpponents(CombatState).WithHitCount(repeat)
                    .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithValueProp(ValueProp.Move | ZZZValueProp.Anomaly)
                    .Execute(choiceContext);
            }
            else await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                    .WithHitCount(repeat).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithValueProp(ValueProp.Move | ZZZValueProp.Anomaly)
                    .Execute(choiceContext);
        }
    }
}
