using HoshimiMiyabi.Model;
using HoshimiMiyabi.Power;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 无尽修行 3=2 每个回合开始时，将1张【任务】牌加入手中
    /// </summary>
    public class EverlastingTraining:MiyabiCardModel
    {
        public EverlastingTraining() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<EverlastingTrainingP>(choiceContext,
                Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
