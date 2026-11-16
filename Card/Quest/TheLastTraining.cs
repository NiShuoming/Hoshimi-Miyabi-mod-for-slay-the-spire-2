using HoshimiMiyabi.Model;
using HoshimiMiyabi.Vfx;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 最后的修行 2 【消耗】【保留】杀死所有敌人
    /// </summary>
    public class TheLastTraining:MiyabiCardModel
    {
        public TheLastTraining() : base(2, CardType.Skill, CardRarity.Token, TargetType.AllEnemies) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Retain, CardKeyword.Exhaust];
        
        public override int MaxUpgradeLevel => 0;

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var list = CombatState.HittableEnemies.ToList();
            NLastTraingVfx nGrandFinaleVfx = NLastTraingVfx.Create();
            if (nGrandFinaleVfx != null)
            {
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(nGrandFinaleVfx);
                await Cmd.Wait(NLastTraingVfx.totalAnticipationDuration);
            }
            await CreatureCmd.Kill(list);
        }
    }
}
