using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 失落温馨 4-1 【架势】【极限突破】获得7(+1)格挡。
    /// </summary>
    public class LostWarmth:MiyabiCardModel
    {
        public LostWarmth() : base(4, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(9,ValueProp.Move)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Stance, ZZZKeyWord.OverLimit];

        public override int MaxUpgradeLevel => 99;

        protected override void OnUpgrade()
        {
            if (CurrentUpgradeLevel <= 4) EnergyCost.UpgradeBy(-1);
            DynamicVars.Block.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature,
                DynamicVars.Block, cardPlay);
        }
    }
}
