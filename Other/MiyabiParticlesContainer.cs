using Godot;
using Godot.Collections;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Other
{
    public partial class MiyabiParticlesContainer : NParticlesContainer
    {
        [Export(PropertyHint.None, "")]
        public Array<GpuParticles2D>? _p;
        public override void _EnterTree()
        {
            var prop = typeof(NParticlesContainer).GetField(
                "_particles", BindingFlags.NonPublic | BindingFlags.Instance);
            if (_p != null && prop != null)
            {
                prop.SetValue(this, _p);
                return;
            }
        }
    }
}
