using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 有【烈霜】的敌人身上的【烈霜】层数不会小于2
    /// </summary>
    public class UnbreakableIceP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override decimal ModifyPowerAmountGivenAdditive(PowerModel power, Creature giver, decimal amount, Creature target, CardModel cardSource)
        {
            if(power is Frost && power.Amount + amount < Amount)
            {
                return Amount - power.Amount - amount;
            }
            return 0;
        }
    }
}
