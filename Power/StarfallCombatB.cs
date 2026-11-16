using HoshimiMiyabi.Main;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 抽到【任务】/架势牌时，抽1
    /// </summary>
    public class StarfallCombatB : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel clonedBy)
        {
            if(card.Owner.Creature==Owner && oldPileType!=PileType.Hand 
                && card.Pile.Type == PileType.Hand &&( card.Type==CardType.Quest
                || card.Keywords.Contains(ZZZKeyWord.Stance) ||
                card.Keywords.Contains(MiyabiKeyWord.ShimotsukiStance)))
            {
                await CardPileCmd.Draw(new ThrowingPlayerChoiceContext(),
                    Amount, Owner.Player);
            }
        }
    }
}
