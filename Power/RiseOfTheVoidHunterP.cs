using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Powers;

namespace HoshimiMiyabi.Power
{
    /// <summary>
    /// 打出伙伴牌、任务牌前会获得2【临时异常精通】。
    /// </summary>
    public class RiseOfTheVoidHunterP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task BeforeCardPlayed(CardPlay cardPlay)
        {
            if(Owner.IsPlayer && cardPlay.Player == Owner.Player
                && (cardPlay.Card.Keywords.Contains(ZZZKeyWord.Partner)
                || cardPlay.Card.Type==CardType.Quest))
            {
                await PowerCmd.Apply<AnomalyProficiencyOneTurn>(
                    new ThrowingPlayerChoiceContext(), Owner,
                    Amount, Owner, null);
            }
        }
    }
}
