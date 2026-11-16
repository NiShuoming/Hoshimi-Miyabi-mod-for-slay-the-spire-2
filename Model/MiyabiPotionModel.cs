using BaseLib.Abstracts;
using BaseLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Model
{
    [Pool(typeof(MiyabiPotionPool))]
    public abstract class MiyabiPotionModel: CustomPotionModel
    {

    }
}
