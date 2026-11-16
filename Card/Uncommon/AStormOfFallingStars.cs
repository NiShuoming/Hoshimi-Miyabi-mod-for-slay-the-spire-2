using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
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

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 星流霆击 2 对随机敌人造成5（6）异常伤害4次。触发【属性异常】后，本回合能耗-1
    /// </summary>
    public class AStormOfFallingStars:MiyabiCardModel,IZZZTriggleAnomalyHook
    {
        public AStormOfFallingStars() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(5,ValueProp.Move|ZZZValueProp.Anomaly),
            new RepeatVar(4)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .FromCard(this, cardPlay).TargetingRandomOpponents(CombatState)
                .WithHitCount(DynamicVars.Repeat.IntValue)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                .Execute(choiceContext);
        }

        public Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results)
        {
            if(applier!=null && applier == Owner.Creature)
            {
                this.EnergyCost.AddThisTurnOrUntilPlayed(-1);
            }
            return Task.CompletedTask;
        }
    }
}
