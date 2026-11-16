using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HoshimiMiyabi.Main
{
    [ModInitializer(nameof(Initialize))]
    public static class MiyabiModInitialzer
    {
        public const string ModId = "Remielle";
        public static void Initialize()
        {
            GD.Print("Miyabi Mod - 加载中!");
            Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(Assembly.GetExecutingAssembly());
            var harmony = new Harmony("Miyabi");
            harmony.PatchAll();
            GD.Print("Miyabi Mod - 加载成功!");

        }
    }
}
