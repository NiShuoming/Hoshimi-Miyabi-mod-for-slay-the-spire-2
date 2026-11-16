using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 冷玉钢 每回合消耗2（1）【落霜】获得1能量
    /// </summary>
    public class JadeboundSteelP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner) && Owner.IsPlayer)
            {
                int n = ZZZCmd.GetSecondEnergy(Owner.Player).Energy2;
                int fall = Math.Min(n, Amount);

                await ZZZCmd.AddSecondEnergy(Owner.Player, -fall, Owner, null);
                if(fall > 0) 
                    await PlayerCmd.GainEnergy(1, Owner.Player);
            }
        }
    }
}
