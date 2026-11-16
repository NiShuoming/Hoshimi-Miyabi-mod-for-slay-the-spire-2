using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 银庭花信 1 【消耗】选择弃牌堆中的1张牌加入手牌，下2（3）个回合开始时，它会回到手牌。
    /// </summary>
    public class DignifiedBlossom:MiyabiCardModel
    {
        public DignifiedBlossom() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("Turn",2)];

        protected override void OnUpgrade()
        {
            DynamicVars["Turn"].UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardModel cardModel = (await CardSelectCmd.FromCombatPile(
                prefs: new CardSelectorPrefs(SelectionScreenPrompt, 1),
                context: choiceContext, pile: PileType.Discard.GetPile(base.Owner),
                player: base.Owner)).FirstOrDefault();

            if (cardModel != null)
            {
                await CardPileCmd.Add(cardModel, PileType.Draw, CardPilePosition.Top);
            }

            PowerModel powerModel = ModelDb.Power<DignifiedBlossomP>();        
            var power = powerModel.ToMutable();
            ((DignifiedBlossomP)power).LoadCard(cardModel);
            await PowerCmd.Apply(choiceContext, power, Owner.Creature,
                DynamicVars["Turn"].BaseValue, Owner.Creature, this);  

        }
    }
}
