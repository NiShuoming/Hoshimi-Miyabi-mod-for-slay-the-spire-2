using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 切磋 -1 【虚无】位于弃牌堆时，对攻击意图的敌人造成伤害时，随机升级1张手牌。
    /// </summary>
    public class Duel:MiyabiCardModel
    {
        public Duel():base(-1, CardType.Quest,CardRarity.Token, TargetType.None) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Ethereal];
        public override int MaxUpgradeLevel => 0;

        public override Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature dealer, DamageResult result, ValueProp props, Creature target, CardModel cardSource)
        {
            if (Pile.Type == PileType.Discard && dealer == Owner.Creature && target.IsEnemy && target.Monster.IntendsToAttack)
            {
                var cards = PileType.Hand.GetPile(Owner).Cards.Where(a => a.IsUpgradable).ToList();
                if(cards.Count > 0) 
                CardCmd.Upgrade(CombatState.RunState.Rng.CombatCardSelection.NextItem(cards));

            }
            return Task.CompletedTask;
        }
    }
}
