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

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 霜刃染 0 【消耗】造成6（8）【异常】伤害，目标下2(3)次受到的伤害提升100%
    /// </summary>
    public class FrostveiledEdge:MiyabiCardModel
    {
        public FrostveiledEdge() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(6, ValueProp.Move|ZZZValueProp.Anomaly),
            new DynamicVar("DamageUp", 2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
            DynamicVars["DamageUp"].UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                .Execute(choiceContext);
            await PowerCmd.Apply<FrostveiledEdgeP>(choiceContext, cardPlay.Target,
                DynamicVars["DamageUp"].BaseValue, Owner.Creature, this);
        }
    }
}
