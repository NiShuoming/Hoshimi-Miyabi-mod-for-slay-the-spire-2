using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 花雪憩蜂 1 获得7（10）格挡，本回合敌人每有1层【烈霜】，对自己造成的伤害减少15%
    /// </summary>
    public class AwakeningOfTheBees:MiyabiCardModel
    {
        public AwakeningOfTheBees() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(7, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move),
            new DynamicVar("DamageDown", 15)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Frost>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

            await PowerCmd.Apply<AwakeningOfTheBeesP>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);
        }
    }
}
