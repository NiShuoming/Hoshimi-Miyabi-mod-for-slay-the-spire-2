using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using HoshimiMiyabi.Power;
using HoshimiMiyabi.Vfx;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 名残雪 3 【终结技】对所有敌人造成8（10）【异常伤害】3次，获得3(4)【落霜】
    /// </summary>
    public class LingeringSnow:MiyabiCardModel
    {
        public LingeringSnow() : base(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Ultimate];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(8,ValueProp.Move|ZZZValueProp.Anomaly)
            , new RepeatVar(3), new PowerVar<FallenFrost>(3)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
            DynamicVars.Power<FallenFrost>().UpgradeValueBy(1);
        } 

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<FallenFrost>(),
            HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            MiyabiVoiceManager.PlayBigSkillVoice();

            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .FromCard(this, cardPlay).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithHitCount(DynamicVars.Repeat.IntValue)
                .WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly).Execute(choiceContext);

            await ZZZCmd.AddSecondEnergy(Owner,
                DynamicVars.Power<FallenFrost>().IntValue
                , Owner.Creature, this);
        }
    }
}
