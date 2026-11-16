using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Saves.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 寒雀 1 获得8（11）格挡，若本回合生成过牌，再获得8（11）格挡
    /// </summary>
    public class KanSuzume : MiyabiCardModel
    {
        public KanSuzume() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(8, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)];

        protected override bool ShouldGlowGoldInternal => WasCardGeneratedThisTurn;

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
        }

        private bool WasCardGeneratedThisTurn => CombatManager.Instance.History.Entries.
            OfType<CardGeneratedEntry>().Any((CardGeneratedEntry e) => e.HappenedThisTurn(
                base.CombatState) && e.Actor == base.Owner.Creature);

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            if (WasCardGeneratedThisTurn)
            {
                await Cmd.Wait(0.1f);
                await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            }
        }
    }
}
