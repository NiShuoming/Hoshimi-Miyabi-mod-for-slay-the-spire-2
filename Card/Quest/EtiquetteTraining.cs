using HoshimiMiyabi.Main;
using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 礼仪修行 -1 【母亲的修行】【保留】战斗开始时说一句台词
    /// </summary>
    public class EtiquetteTraining:MiyabiCardModel
    {
        public EtiquetteTraining():base(-1, CardType.Quest,CardRarity.Token, TargetType.None) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [MiyabiKeyWord.MotherTraining,CardKeyword.Retain];

        public override int MaxUpgradeLevel => 0;

        public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner.Creature))
            {
                MiyabiVoiceManager.PlayXianVoice();
            }
            return Task.CompletedTask;
        }
    }
}
