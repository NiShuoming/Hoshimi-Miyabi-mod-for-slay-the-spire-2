using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
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

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 如切如磋 1 造成9（13）伤害，将一张【切磋】加入弃牌堆
    /// </summary>
    public class LikeADuel:MiyabiCardModel
    {
        public LikeADuel() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromCard<Duel>()];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(9,ValueProp.Move)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(4);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            MiyabiVoiceManager.PlayAttackVoice();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", tmpSfx: "雅打击.mp3")
                .Execute(choiceContext);
            var card2 = CombatState.CreateCard<Duel>(Owner);
            await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Discard, Owner, CardPilePosition.Random);

        }
    }
}
