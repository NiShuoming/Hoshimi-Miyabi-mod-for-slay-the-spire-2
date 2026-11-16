using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Relic
{
    /// <summary>
    ///  抽到【任务】牌时，对所有敌人造成4伤害
    /// </summary>
    public class MartialDiscipline : MiyabiRelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Rare;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(4, ValueProp.Unpowered)];

        public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel clonedBy)
        {
            if(card.Type==CardType.Quest && oldPileType!=PileType.Hand && card.Pile.Type==PileType.Hand)
            {
                var list = card.CombatState.HittableEnemies.ToList();
                await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(),
                    list, DynamicVars.Damage, Owner.Creature);
            }
        }
    }
}
