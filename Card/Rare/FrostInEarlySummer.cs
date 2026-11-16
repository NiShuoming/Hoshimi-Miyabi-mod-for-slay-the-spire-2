using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 于初夏成霜 1 【】=【固有】消耗【落霜】时，抽3，每回合1次
    /// </summary>
    public class FrostInEarlySummer:MiyabiCardModel
    {
        public FrostInEarlySummer() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
        }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(3)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<FallenFrost>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<FrostInEarlySummerP>(choiceContext, Owner.Creature
                ,DynamicVars.Cards.BaseValue, Owner.Creature, this);
        }
    }
}
