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
using ZZZLib.Powers;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 冬蜂 2 获得12（15）格挡，给予所有敌人2（3）【烈霜】
    /// </summary>
    public class Fuyubachi:MiyabiCardModel
    {
        public Fuyubachi() : base(2, CardType.Skill, CardRarity.Common, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(12, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move),
            new PowerVar<Frost>(2)];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
            DynamicVars.Power<Frost>().UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Frost>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            await PowerCmd.Apply<Frost>(choiceContext, CombatState.HittableEnemies,
                DynamicVars.Power<Frost>().BaseValue, Owner.Creature, this);
        }
    }
}
