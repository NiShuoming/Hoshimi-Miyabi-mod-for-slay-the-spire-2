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
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 飞雪 1 造成22（34）【异常伤害】，给予2（3）【烈霜】，获得2【落霜】
    /// </summary>
    public class Hisetsu:MiyabiCardModel
    {
        public Hisetsu() : base(1, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromPower<Frost>(), HoverTipFactory.FromPower<FallenFrost>()];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(22, ValueProp.Move|ZZZValueProp.Anomaly),
            new PowerVar<Frost>(2), new PowerVar<FallenFrost>(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(12);
            DynamicVars.Power<Frost>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithValueProp(ValueProp.Move | ZZZValueProp.Anomaly)
                .Execute(choiceContext);

            await PowerCmd.Apply<Frost>(choiceContext, cardPlay.Target,
                DynamicVars.Power<Frost>().BaseValue, Owner.Creature, this);

            await ZZZCmd.AddSecondEnergy(Owner, DynamicVars.Power<FallenFrost>().IntValue
                , Owner.Creature, this);
        }
    }
}
