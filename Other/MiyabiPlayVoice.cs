using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Other
{
    public partial class MiyabiPlayVoice : Node2D
    {
        public override void _EnterTree()
        {
            MiyabiVoiceManager.PlaySelectVoice();
        }
    }
}
