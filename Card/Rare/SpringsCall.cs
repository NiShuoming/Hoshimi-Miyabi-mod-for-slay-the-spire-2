using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 春临 0 其他手牌全部被强化时群伤20（30）
    /// </summary>
    public class SpringsCall:MiyabiCardModel
    {
        public SpringsCall():base(0, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(15, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(10);
        }

        protected override bool ShouldGlowGoldInternal =>
            PileType.Hand.GetPile(Owner).Cards.Any(a => (a != this)) &&
            ! PileType.Hand.GetPile(Owner).Cards.Any(a=>(a!=this && !a.IsUpgraded));

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (ShouldGlowGoldInternal)
            {
                MiyabiVoiceManager.PlayAttackVoice();
                await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                    .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").TargetingAllOpponents(CombatState)
                    .Execute(choiceContext);

            }
        }
    }
}
