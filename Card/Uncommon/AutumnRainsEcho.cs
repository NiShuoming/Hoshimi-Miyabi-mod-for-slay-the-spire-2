using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 秋水洄鸣 2 消耗最多2（3）张牌。对所有敌人造成12（15）伤害，每消耗1张牌，额外造成5（7）伤害。
    /// </summary>
    public class AutumnRainsEcho:MiyabiCardModel
    {
        public AutumnRainsEcho() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2), new DamageVar(12,ValueProp.Move),
            new ExtraDamageVar(5)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Exhaust)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
            DynamicVars.ExtraDamage.UpgradeValueBy(2);
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var cards = (await CardSelectCmd.FromHand( prefs: new CardSelectorPrefs(
                    CardSelectorPrefs.ExhaustSelectionPrompt, 0, DynamicVars.Cards.IntValue),
                context: choiceContext, player: base.Owner, filter: null, source: this)).ToList();
            if (cards.Count > 0)
            {
                foreach(var card in cards)
                {
                    await CardCmd.Exhaust(choiceContext, card);
                }
            }
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue +
                cards.Count * DynamicVars.ExtraDamage.BaseValue)
                .FromCard(this, cardPlay).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                .Execute(choiceContext);
        }

    }
}
