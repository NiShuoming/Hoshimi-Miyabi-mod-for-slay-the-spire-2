using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 雅的卷轴 1 从手牌中选择1张攻击牌加入手牌（并升级）
    /// </summary>
    public class MiyabisScroll:MiyabiCardModel
    {
        public MiyabisScroll():base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var cards = PileType.Hand.GetPile(Owner).Cards.Where(a => a.Type == CardType.Attack).ToList();

            CardModel cardModel = (await CardSelectCmd.FromSimpleGrid(choiceContext,
                cards, Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 1))).FirstOrDefault();

            if (cardModel != null)
            {
                await CardPileCmd.Add(cardModel, PileType.Hand);
                if(IsUpgraded && cardModel.IsUpgradable)CardCmd.Upgrade(cardModel);
            }

        }
    }
}
