using BaseLib.Extensions;
using HoshimiMiyabi.Main;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
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
    /// 烈露濯霜 1 造成10（12）点异常伤害，将【霜月架势】牌召唤到手牌.（获得1落霜）
    /// </summary>
    public class FrostgleamDew:MiyabiCardModel
    {
        public FrostgleamDew() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get {
                if(IsUpgraded) return [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromKeyword(MiyabiKeyWord.ShimotsukiStance),
                HoverTipFactory.FromPower<FallenFrost>()];
                else return [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromKeyword(MiyabiKeyWord.ShimotsukiStance)];
            }
        }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(10,ValueProp.Move|ZZZValueProp.Anomaly),
            new PowerVar<FallenFrost>(1)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3").WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                .Execute(choiceContext);
            var list = Owner.PlayerCombatState.AllCards.Where((CardModel a) =>
            a.Keywords.Contains(MiyabiKeyWord.ShimotsukiStance)).ToList();
            if(list.Count>0)
                await CardPileCmd.Add(list, PileType.Hand);
            if (IsUpgraded) await ZZZCmd.AddSecondEnergy(Owner, DynamicVars.Power<FallenFrost>().IntValue,
                Owner.Creature, this);
        }
    }
}
