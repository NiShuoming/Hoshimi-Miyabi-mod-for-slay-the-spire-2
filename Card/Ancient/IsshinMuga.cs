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
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 虚狩·心念无我 2 []=[固有] 触发【紊乱】时，获得1能量，每回合最多4次。
    /// </summary>
    public class IsshinMuga:MiyabiCardModel
    {
        public IsshinMuga() : base(2, CardType.Power, CardRarity.Ancient, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(1), new DynamicVar("Times",4)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Disorder),
            HoverTipFactory.Static(StaticHoverTip.Energy)];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<IsshinMugaP>(choiceContext, Owner.Creature
                , 1, Owner.Creature, this);
        }
    }
}
