using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 烛火摇曳 0 【保留】获得3格挡，可回到手中2（3）次，每回合重置。
    /// </summary>
    public class FlickeringCandlelight:MiyabiCardModel
    {
        public FlickeringCandlelight() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(3,ValueProp.Move),
            new DynamicVar("Turn",2), new DynamicVar("MaxTurn",2)];

        protected override void OnUpgrade()
        {
            DynamicVars["Turn"].UpgradeValueBy(1);
            DynamicVars["MaxTurn"].UpgradeValueBy(1);
        }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Retain];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature,
                DynamicVars.Block, cardPlay);
        }

        protected override CardLocation GetResultLocationForCardPlay()
        {
            CardLocation resultLocationForCardPlay = base.GetResultLocationForCardPlay();
            if (resultLocationForCardPlay.pileType == PileType.Discard 
                && DynamicVars["Turn"].BaseValue>0)
            {
                resultLocationForCardPlay.pileType = PileType.Hand;
                DynamicVars["Turn"].BaseValue -= 1;
            }
            return resultLocationForCardPlay;
        }

        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if(participants.Contains(Owner.Creature))
            {
                DynamicVars["Turn"].BaseValue = DynamicVars["MaxTurn"].BaseValue;
            }return Task.CompletedTask;
        }
    }
}
