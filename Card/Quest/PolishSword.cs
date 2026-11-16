using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
    /// 保养佩刀 -1 【保留】位于手牌中，如果本回合没有使用攻击牌，下回合攻击伤害+100%，然后消耗这张牌。
    /// </summary>
    public class PolishSword:MiyabiCardModel
    {
        public PolishSword() : base(-1, CardType.Quest, CardRarity.Token, TargetType.None) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Retain];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("DamageUp",100)];

        protected override void OnUpgrade()
        {
            DynamicVars["DamageUp"].UpgradeValueBy(50);
        }

        protected override bool ShouldGlowGoldInternal =>
            !CombatManager.Instance.History.CardPlaysFinished.Any(
                (CardPlayFinishedEntry e) => e.CardPlay.Card.Type == CardType.Attack &&
                e.CardPlay.Player == base.Owner && e.HappenedThisTurn(base.CombatState));

        public override async Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner.Creature) && Pile.Type == PileType.Hand && ShouldGlowGoldInternal)
            {
                await PowerCmd.Apply<PolishSwordP>(choiceContext, Owner.Creature,
                    DynamicVars["DamageUp"].BaseValue, Owner.Creature, this);
                await CardCmd.Exhaust(choiceContext, this);
            }
        }
    }
}
