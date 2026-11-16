using BaseLib.Extensions;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 无边飞雪 x 【】=【保留】给予所有敌人3（4）层【烈霜】，X次
    /// </summary>
    public class EndlessBlizzard:MiyabiCardModel
    {
        public EndlessBlizzard() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<Frost>(3)];

        protected override bool HasEnergyCostX => true;

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);
            DynamicVars.Power<Frost>().UpgradeValueBy(1);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Frost>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int num = ResolveEnergyXValue();
            await PowerCmd.Apply<Frost>(choiceContext, CombatState.HittableEnemies,
                DynamicVars.Power<Frost>().BaseValue * num, Owner.Creature, this);

        }
    }
}
