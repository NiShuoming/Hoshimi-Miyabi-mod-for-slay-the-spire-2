using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 拔刀形态 3=2 获得或给予buff时，会对所有敌人造成3（4）伤害
    /// </summary>
    public class SwordDrawnForm:MiyabiCardModel
    {
        public SwordDrawnForm() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(3,ValueProp.Unpowered)];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
            DynamicVars.Damage.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<SwordDrawnFormP>(choiceContext, Owner.Creature,
                DynamicVars.Damage.BaseValue, Owner.Creature, this);
        }
    }
}
