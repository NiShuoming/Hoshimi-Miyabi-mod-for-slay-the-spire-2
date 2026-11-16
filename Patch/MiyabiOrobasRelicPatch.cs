using Godot;
using HarmonyLib;
using HoshimiMiyabi.Relic;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Patch
{
    [HarmonyPatch(typeof(TouchOfOrobas), "get_RefinementUpgrades")]
    public static class MiyabiOrobasRelicPatch
    {
        static void Postfix(ref Dictionary<ModelId, RelicModel> __result)
        {
            if (!__result.ContainsKey(ModelDb.Relic<SearingCold>().Id))
            {
                __result.Add(ModelDb.Relic<SearingCold>().Id,
                ModelDb.Relic<BaskInFrost>());
            }
        }
    }
}
