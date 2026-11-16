using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace HoshimiMiyabi.Model
{
    [Pool(typeof(MiyabiCardPool))]
    public abstract class MiyabiCardModel
    (int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true, bool autoAdd = true) :
    CustomCardModel(baseCost, type, rarity, target, showInCardLibrary, autoAdd)
    {
        protected virtual string PortraitName => Id.Entry.RemovePrefix().ToSnakeCase();

        public override string CustomPortraitPath =>
            PathHelper.GetCardImageBigPath(PortraitName);

        public override string PortraitPath =>
            PathHelper.GetCardImageBigPath(PortraitName);
    }

}
