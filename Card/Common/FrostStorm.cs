using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 霜碎雪 2 对所有敌人造成14（17）伤害，给予1（2）【虚弱】。
    /// </summary>
    public class FrostStorm:MiyabiCardModel
    {
        public FrostStorm() : base(2, CardType.Attack, CardRarity.Common, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(14, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)
            , new PowerVar<WeakPower>(1)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
            DynamicVars.Power<WeakPower>().UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<WeakPower>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").TargetingAllOpponents(CombatState)
                .Execute(choiceContext);

            await PowerCmd.Apply<WeakPower>(choiceContext, CombatState.HittableEnemies,
                DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
        }
    }
}
