using BaseLib.Extensions;
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
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 居合道 1 对所有敌人造成7（10）点异常伤害，将弃牌堆中的1张牌放入抽排堆顶部。
    /// </summary>
    public class Iaidou:MiyabiCardModel
    {
        public Iaidou() : base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(6, ValueProp.Move|ZZZValueProp.Anomaly)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                .TargetingAllOpponents(CombatState).WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                .Execute(choiceContext);

            CardModel cardModel = (await CardSelectCmd.FromCombatPile(
                prefs: new CardSelectorPrefs(SelectionScreenPrompt, 1), 
                context: choiceContext, pile: PileType.Discard.GetPile(base.Owner), 
                player: base.Owner)).FirstOrDefault();

            if (cardModel != null)
            {
                await CardPileCmd.Add(cardModel, PileType.Draw, CardPilePosition.Top);
            }
        }
    }
}
