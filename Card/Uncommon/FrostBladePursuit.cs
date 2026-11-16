using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 霜刃追袭 1 【架势】【极限突破】造成8（+1）伤害，给予1（+1）脆弱
    /// </summary>
    public class FrostBladePursuit : MiyabiCardModel
    {
        public FrostBladePursuit() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(8, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)
            , new PowerVar<FrailPower>(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(1);
            DynamicVars.Power<FrailPower>().UpgradeValueBy(1);
        }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Stance, ZZZKeyWord.OverLimit];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<FrailPower>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").Targeting(cardPlay.Target)
                .Execute(choiceContext);

            await PowerCmd.Apply<FrailPower>(choiceContext, cardPlay.Target,
                DynamicVars.Power<FrailPower>().BaseValue, Owner.Creature, this);
        }
    }
}
