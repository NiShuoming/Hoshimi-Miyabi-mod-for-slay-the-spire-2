using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 花信风 2 获得12（15）格挡，获得2（3）【落霜】
    /// </summary>
    public class DancingPetals:MiyabiCardModel
    {
        public DancingPetals() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(12,ValueProp.Move), new PowerVar<FallenFrost>(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
            DynamicVars.Power<FallenFrost>().UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<FallenFrost>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            await ZZZCmd.AddSecondEnergy(Owner,DynamicVars.Power<FallenFrost>().IntValue,
                Owner.Creature, this);
        }
    }
}
