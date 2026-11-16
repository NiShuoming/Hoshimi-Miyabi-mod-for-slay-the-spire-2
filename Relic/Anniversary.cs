using HoshimiMiyabi.Model;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace HoshimiMiyabi.Relic
{
    /// <summary>
    /// 纪念日 伙伴牌给予的debuff+1
    /// </summary>
    public class Anniversary : MiyabiRelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Uncommon;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Partner)];

        public override decimal ModifyPowerAmountGivenAdditive(PowerModel power, Creature giver, decimal amount, Creature target, CardModel cardSource)
        {
            if(cardSource!=null && cardSource.Keywords.Contains(ZZZKeyWord.Partner)&&
                giver==Owner.Creature && power.Type == PowerType.Debuff)
            {
                return 1;
            }
            return 0;
        }
    }
}
