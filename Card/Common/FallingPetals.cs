using BaseLib.Extensions;
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
    /// 花辞 1 获得8（10）格挡，给予1（2）【烈霜】
    /// </summary>
    public class FallingPetals:MiyabiCardModel
    {
        public FallingPetals() : base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(8, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move),
            new PowerVar<Frost>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Frost>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(2);
            DynamicVars.Power<Frost>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

            await PowerCmd.Apply<Frost>(choiceContext,cardPlay.Target,
                DynamicVars.Power<Frost>().BaseValue, Owner.Creature, this);
        }
    }
}
