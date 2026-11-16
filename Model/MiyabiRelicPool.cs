using BaseLib.Abstracts;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Model
{
    public sealed class MiyabiRelicPool: CustomRelicPoolModel
    {
        public override string EnergyColorName => "miyabi";

        public override Color LabOutlineColor => new Color(0, 1, 1);

    }
}
