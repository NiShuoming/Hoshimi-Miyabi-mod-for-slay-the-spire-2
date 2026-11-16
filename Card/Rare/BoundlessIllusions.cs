using HoshimiMiyabi.Main;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
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
    /// 斩念千幻 X 【霜月架势】对目标造成12（+2）伤害X倍  2：倍数+2，改为群伤
    /// </summary>
    public class BoundlessIllusions:MiyabiCardModel
    {
        public BoundlessIllusions() : base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(12,ValueProp.Move),new RepeatVar(2)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [MiyabiKeyWord.ShimotsukiStance];

        protected override bool HasEnergyCostX => true;

        public override int MaxUpgradeLevel => 99;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Stance),
            HoverTipFactory.FromKeyword(ZZZKeyWord.OverLimit)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int num = ResolveEnergyXValue();
            int n = ZZZCmd.GetSecondEnergy(Owner).Energy2;
            await MiyabiVoiceManager.PlayChargeVoice(n>=2?1:0,Owner,this);
            MiyabiVoiceManager.PlayShuangyueVoice();

            if (n >= 2)
            {
                num += DynamicVars.Repeat.IntValue;
                await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue * num )
                    .FromCard(this, cardPlay).TargetingAllOpponents(CombatState)
                    .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                    .Execute(choiceContext);
            }
            else  await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue * num)
                    .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                    .Execute(choiceContext);
        }
    }
}
