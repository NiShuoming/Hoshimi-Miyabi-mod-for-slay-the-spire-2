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
using ZZZLib;
using ZZZLib.Powers;

namespace HoshimiMiyabi.Card
{
    /// <summary>
    /// 虚狩诞生之日 1 【】=【固有】打出伙伴牌、任务牌前会获得2【临时异常精通】。
    /// </summary>
    public class RiseOfTheVoidHunter : MiyabiCardModel
    {
        public RiseOfTheVoidHunter() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<AnomalyProficiency>(2)];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Partner),
            HoverTipFactory.FromPower<AnomalyProficiency>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<RiseOfTheVoidHunterP>(choiceContext,
                Owner.Creature, DynamicVars.Power<AnomalyProficiency>().BaseValue,
                Owner.Creature, this);
        }
    }
}
