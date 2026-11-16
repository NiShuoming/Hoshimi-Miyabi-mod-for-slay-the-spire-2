using BaseLib.Extensions;
using Godot;
using HoshimiMiyabi.Main;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 书法修行 0 【母亲的修行】【消耗】获得2【落霜】
    /// </summary>
    public class CalligraphyTraining : MiyabiCardModel
    {
        public CalligraphyTraining() : base(0, CardType.Quest, CardRarity.Token, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [MiyabiKeyWord.MotherTraining, CardKeyword.Exhaust];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<FallenFrost>(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<FallenFrost>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await ZZZCmd.AddSecondEnergy(Owner, DynamicVars.Power<FallenFrost>().IntValue, Owner.Creature, this);
        }
    }
}
