using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using ZZZLib.Cards.Equips.DriveDiscs;
using ZZZLib.Cards.Equips.Other;
using ZZZLib.Cards.Equips.WEngine;
using ZZZLib.Cards.Partner;

namespace HoshimiMiyabi.Model
{
    public class MiyabiCardPool: CustomCardPoolModel
    {
        public override string Title => "Miyabi";

        public override string EnergyColorName => "miyabi";

        public override string CardFrameMaterialPath => "card_frame_blue";

        public override Color DeckEntryCardColor => Colors.Blue;

        public override Color EnergyOutlineColor => new Color("802020");

        public override bool IsColorless => false;

        /// <summary>
        /// 记录角色所有卡牌
        /// </summary>
        /// <returns></returns>
        protected override CardModel[] GenerateAllCards()
        {
            return new CardModel[]
            {
                ModelDb.Card<Soukaku>(),
                ModelDb.Card<CunningHares>(),
                ModelDb.Card<BelobogHeavyIndustries>(),
                ModelDb.Card<SonsOfCalydon>(),
                ModelDb.Card<Hsos6>(),
                ModelDb.Card<TsukishiroYanagi>(),
                ModelDb.Card<VictoriaHousekeepingCo>(),
                 ModelDb.Card<FrostStainedStar>(),
                 ModelDb.Card<Cisrt>(),
                 ModelDb.Card<AsabaHarumasa>(),
                 ModelDb.Card<HailstormShrine>(),
                 ModelDb.Card<BranchBladeSong>(),
                ModelDb.Card<Yixuan>(),

            };
        }

    }
}
