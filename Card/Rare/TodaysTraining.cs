using HoshimiMiyabi.Main;
using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 今日修行 1 【】=【固有】随机获得1张不重复的【母亲的修行】牌。
    /// </summary>
    public class TodaysTraining:MiyabiCardModel
    {
        public TodaysTraining() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(MiyabiKeyWord.MotherTraining),
            HoverTipFactory.FromCard<TheLastTraining>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var cardstemp = Owner.PlayerCombatState.AllCards.Where(
                a => a.Keywords.Contains(MiyabiKeyWord.MotherTraining));

            CardModel cardModel = CardFactory.GetDistinctForCombat(Owner,
                from c in Owner.Character.CardPool.GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint)
                where (c.Keywords.Contains(MiyabiKeyWord.MotherTraining) && !cardstemp.Any(a=>a.GetType()==(c.GetType())))
                select c, 1, Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault();
            if (cardModel != null)
            {
                await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, Owner);
            }
        }
    }
}
