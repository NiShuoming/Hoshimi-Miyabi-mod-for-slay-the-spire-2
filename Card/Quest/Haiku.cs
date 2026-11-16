using HoshimiMiyabi.Model;
using HoshimiMiyabi.Other;
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
    /// 俳句 0 【消耗】随便念1道台词
    /// </summary>
    public class Haiku:MiyabiCardModel
    {
        public Haiku():base(0,CardType.Quest, CardRarity.Token, TargetType.Self) { }

        public override int MaxUpgradeLevel => 0;

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];
        
        protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            MiyabiVoiceManager.PlayXianVoice();
            return base.OnPlay(choiceContext, cardPlay);
        }
    }
}
