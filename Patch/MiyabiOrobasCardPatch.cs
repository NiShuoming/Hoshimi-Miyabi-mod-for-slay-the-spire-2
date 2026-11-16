using HarmonyLib;
using HoshimiMiyabi.Card;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Patch
{
    [HarmonyPatch(typeof(ArchaicTooth), "get_TranscendenceUpgrades")]
    public static class MiyabiOrobasCardPatch
    {
        static void Postfix(ref Dictionary<ModelId, CardModel> __result)
        {
            if (!__result.ContainsKey(ModelDb.Card<Miyuki>().Id))
                __result.Add(ModelDb.Card<Miyuki>().Id,
                ModelDb.Card<Hisetsu>());
        }
    }
}
