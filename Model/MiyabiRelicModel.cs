using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Model
{
    [Pool(typeof(MiyabiRelicPool))]
    public abstract class MiyabiRelicModel : CustomRelicModel
    {
        protected override string IconBaseName =>
    base.Id.Entry.RemovePrefix().ToLowerInvariant();
    }
}
