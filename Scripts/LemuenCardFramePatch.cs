using System.Reflection;
using BaseLib.Abstracts;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace LXY.Scripts;

[HarmonyPatch(typeof(NCard), "Reload")]
public static class LemuenCardFramePatch
{
    private static readonly FieldInfo? FrameField =
        typeof(NCard).GetField("_frame", BindingFlags.NonPublic | BindingFlags.Instance);

    [HarmonyPostfix]
    private static void Reload_Postfix(NCard __instance)
    {
        if (__instance.Model is not CustomCardModel customCard ||
            customCard.Pool is not lemuenCardPool pool)
        {
            return;
        }

        if (FrameField?.GetValue(__instance) is not TextureRect frameRect)
        {
            return;
        }

        Texture2D? customFrame = pool.CustomFrame(customCard);
        if (customFrame != null)
        {
            frameRect.Texture = customFrame;
        }

        // BaseLib/default card rendering may keep tint/material on the frame.
        // Custom colored frame art should render as-authored for lemuen cards.
        frameRect.Material = null;
        frameRect.Modulate = Colors.White;
        frameRect.SelfModulate = Colors.White;
    }
}
