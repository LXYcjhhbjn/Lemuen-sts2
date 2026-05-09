using System.Reflection;
using BaseLib.Abstracts;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Saves.Managers;
using STS2RitsuLib.Audio;

namespace LXY.Scripts;

[ModInitializer("Init")]
public class Entry
{
    public static void Init()
    {
        var harmony = new Harmony("sts2.LXYmods");
        harmony.PatchAll();
        ScriptManagerBridge.LookupScriptsInAssembly(typeof(Entry).Assembly);
        FmodStudioDeferredBankRegistration.RegisterBank("res://lemuen/sfx/Lemuen.bank");
        FmodStudioDeferredBankRegistration.RegisterStudioGuidMappings("res://lemuen/sfx/GUIDs.txt");
        Log.Debug("Mod initialized!");
    }
    [HarmonyPatch(typeof(ProgressSaveManager),"ObtainCharUnlockEpoch")]
    private class SkipObtainCharUnlockEpoch
    {
        [HarmonyPrefix]
        private static bool Prefix(Player localPlayer)
        {
            return localPlayer.Character is not ICustomModel;
        }
    }
    [HarmonyPatch]
    public static class TouchOfOrobasPatch
    {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            return AccessTools.Method(
                typeof(TouchOfOrobas),
                nameof(TouchOfOrobas.GetUpgradedStarterRelic),
                new Type[]
                {
                    typeof(RelicModel)
                }
            );
        }
        [HarmonyPrefix]
        public static bool Prefix(
            ref RelicModel __result,
            RelicModel starterRelic
            )
        {
            if(starterRelic.Id==ModelDb.Relic<GunFlowers>().Id)
            {
                __result=ModelDb.Relic<AmmoFlower>();
                return false;
            }
            return true;
        }
    }
}