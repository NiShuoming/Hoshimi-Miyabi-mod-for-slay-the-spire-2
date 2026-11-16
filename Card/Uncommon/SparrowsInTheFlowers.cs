using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cmds;
using ZZZLib.Powers;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 花间雀 1 造成8（10）点伤害，获得3（4）临时【异常精通】，给予3（4）【烈霜】
    /// </summary>
    public class SparrowsInTheFlowers:MiyabiCardModel
    {
        public SparrowsInTheFlowers() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<AnomalyProficiency>(3),
            new DamageVar(8,MegaCrit.Sts2.Core.ValueProps.ValueProp.Move),
            new PowerVar<Frost>(3)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(1);
            DynamicVars.Power<AnomalyProficiency>().UpgradeValueBy(1);
            DynamicVars.Power<Frost>().UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<AnomalyProficiency>(),
            HoverTipFactory.FromPower<Frost>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                .Execute(choiceContext);

            await PowerCmd.Apply<AnomalyProficiencyOneTurn>(choiceContext,
                Owner.Creature, DynamicVars.Power<AnomalyProficiency>().BaseValue,
                Owner.Creature, this);

            await PowerCmd.Apply<Frost>(choiceContext, cardPlay.Target,
                DynamicVars.Power<Frost>().BaseValue, Owner.Creature, this);
        }
    }
}
