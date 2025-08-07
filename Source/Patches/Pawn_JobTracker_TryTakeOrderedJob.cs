using HarmonyLib;
using System.Reflection;
using Verse;
using Verse.AI;

namespace Merthsoft.QueueCommandsButton.Patches;

[HarmonyPatch(typeof(Pawn_JobTracker), nameof(Pawn_JobTracker.TryTakeOrderedJob))]
public static class Pawn_JobTracker_TryTakeOrderedJob
{
    static readonly FieldInfo Pawn = AccessTools.Field(typeof(Pawn_JobTracker), "pawn");

    static Pawn GetPawn(Pawn_JobTracker jobTracker)
        => Pawn.GetValue(jobTracker) as Pawn;

    static void Prefix(Pawn_JobTracker __instance, ref bool requestQueueing) =>
        requestQueueing |= QueueCommandsButtonMod.IsEnabled(GetPawn(__instance));
}