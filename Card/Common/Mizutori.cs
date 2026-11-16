using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
    /// 水鸟 1 【架势】奇：获得6格挡，抽2。偶：获得11格挡
    /// </summary>
    public class Mizutori:MiyabiCardModel
    {
        public Mizutori() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(6, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move),
            new CardsVar(2)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Stance];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(5);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            if (!IsUpgraded) 
                await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }
    }
}
