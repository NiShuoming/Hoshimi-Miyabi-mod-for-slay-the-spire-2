using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Main
{
    public class MiyabiKeyWord
    {
        /// <summary>
        /// 霜月架势 具有架势和极限突破特性,根据【落霜】提升卡牌的效果。
        /// </summary>
        [CustomEnum("ShimotsukiStance")]
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword ShimotsukiStance;
        
        /// <summary>
        /// 母亲的修行 集齐4种具有该词条的卡牌时，消耗这些卡牌，将【最后】的修行加入手牌
        /// </summary>
        [CustomEnum("MotherTraining")]
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword MotherTraining;
        
    }
}
