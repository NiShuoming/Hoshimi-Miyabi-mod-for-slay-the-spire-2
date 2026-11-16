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
    /// 霜月 0 【霜月架势】2：造成6（+1）伤害。:4：攻击次数+1，改为群伤。6：对所有敌人造成8（+2）伤害
    /// </summary>
    public class Shimotsuki:MiyabiCardModel
    {
        public Shimotsuki() : base(0, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy) { }

        /*
        public override TargetType TargetType
        {
            get {
                if(Owner == null) return TargetType.AnyEnemy;
                if (ZZZCmd.GetSecondEnergy(Owner).Energy2 >= 4)
                    return TargetType.AllEnemies;
                return TargetType.AnyEnemy;
            }
        }*/
        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Stance),
            HoverTipFactory.FromKeyword(ZZZKeyWord.OverLimit),
            HoverTipFactory.FromPower<FallenFrost>()];
        public override int MaxUpgradeLevel => 99;

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [MiyabiKeyWord.ShimotsukiStance];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(6, ValueProp.Move), 
            new DamageVar("Damage2", 8, ValueProp.Move)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(1);
            DynamicVars["Damage2"].UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int n = ZZZCmd.GetSecondEnergy(Owner).Energy2;
            await MiyabiVoiceManager.PlayChargeVoice(n / 2,Owner,this);
            MiyabiVoiceManager.PlayShuangyueVoice();
            if (n >= 2)
            {
                if (n >= 4)
                {
                    await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                        .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").TargetingAllOpponents(CombatState)
                        .WithHitCount(2).Execute(choiceContext);

                }
                else
                {
                    ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
                    await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                        .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                        .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                        .Execute(choiceContext);
                }
            }
            if(n >= 6)
            {
                await DamageCmd.Attack(base.DynamicVars["Damage2"].BaseValue).FromCard(this, cardPlay)
                    .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").TargetingAllOpponents(CombatState)
                    .Execute(choiceContext);
            }
        }
    }
}
