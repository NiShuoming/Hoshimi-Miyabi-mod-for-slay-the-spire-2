using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 银庭花信 1 下2（3）个回合开始时，它会回到手牌。
    /// </summary>
    public class DignifiedBlossomP() : PowerModel
    {
        private CardModel cardModel =null;

        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get { if (cardModel != null) return [HoverTipFactory.FromCard(cardModel)];
                return [];
            }
        }

        public override async Task AfterSideTurnStartLate(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner)&& cardModel!=null)
            {
                await CardPileCmd.Add(cardModel, PileType.Hand.GetPile(Owner.Player));
                await PowerCmd.Decrement(this);
            }
        }

        public void LoadCard(CardModel cardModel)
        { this.cardModel = cardModel; }
    }
}
