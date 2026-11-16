using BaseLib.Abstracts;
using HoshimiMiyabi.Card;
using HoshimiMiyabi.Main;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Listener
{
    /// <summary>
    /// 集齐3种具有该词条的卡牌时，消耗这些卡牌，将【最后】的修行加入手牌
    /// </summary>
    public class MotherTrainingListener : CustomSingletonModel
    {
        public MotherTrainingListener() : base(HookType.Combat)
        {
            did = false;
        }

        public override bool ShouldReceiveCombatHooks => true;

        bool did = false;

        public override async Task AfterCardGeneratedForCombat(CardModel card, Player creator)
        {
            if(did || creator==null) return;
            var cards = creator.PlayerCombatState.AllCards.Where(
                a => a.Keywords.Contains(MiyabiKeyWord.MotherTraining)).ToList();
            if (cards.Count >= 3)
            {
                did = true;
                foreach (var c in cards)
                {
                    await CardCmd.Exhaust(new ThrowingPlayerChoiceContext(), c);
                }
                var card2 = creator.Creature.CombatState.CreateCard<TheLastTraining>(creator);
                await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Draw, creator, CardPilePosition.Top);

               
            }
        }
    }

}
