using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 冰霜打击 1 造成9（12）点伤害，如果目标有【烈霜】额外造成4（6）伤害
    /// </summary>
    public class FrostStrike:MiyabiCardModel
    {
        public FrostStrike() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override HashSet<CardTag> CanonicalTags => 
            [CardTag.Strike];
        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CalculationBaseVar(9), new ExtraDamageVar(4),
            new CalculatedDamageVar(MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)
            .WithMultiplier((card,enermy)=>{
            if(enermy!=null) return enermy.HasPower<Frost>()?1:0;
            return 0; })];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Frost>()];

        protected override void OnUpgrade()
        {
            DynamicVars.CalculationBase.UpgradeValueBy(3);
            DynamicVars.ExtraDamage.UpgradeValueBy(2);
        }

        protected override bool ShouldGlowGoldInternal =>
            CombatState.HittableEnemies.Any(a=>a.HasPower<Frost>());

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(DynamicVars.CalculatedDamage)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                .Execute(choiceContext);
            
        }
    }
}
