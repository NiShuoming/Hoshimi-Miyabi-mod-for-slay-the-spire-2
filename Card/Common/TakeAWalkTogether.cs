using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 一起散步吧 1 获得7（10）格挡，将1张随机伙伴牌加入手牌
    /// </summary>
    public class TakeAWalkTogether : MiyabiCardModel
    {
        public TakeAWalkTogether() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new BlockVar(7, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Partner)];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

            var cards = Owner.PlayerCombatState.AllCards.Where(a =>
            (a.Keywords.Contains(ZZZKeyWord.Partner) && (a.Pile.Type == PileType.Draw
            || a.Pile.Type==PileType.Discard))).ToList();

            if (cards.Count > 0)
            {
                var card = Owner.RunState.Rng.CombatCardSelection.NextItem(cards);
                await CardPileCmd.Add(card, PileType.Hand.GetPile(Owner));
            }
        }
    }
}
