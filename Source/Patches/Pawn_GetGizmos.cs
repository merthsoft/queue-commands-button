using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Merthsoft.QueueCommandsButton.Patches;

[HarmonyPatch(typeof(Pawn), nameof(Pawn.GetGizmos))]
public static class Pawn_GetGizmos
{
    public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> __result, Pawn __instance)
    {
        foreach (var g in __result)
            yield return g;

        if (QueueCommandsButtonMod.QueueCommandButtonLocation != QueueCommandButtonLocation.PawnGizmoGlobal
            && QueueCommandsButtonMod.QueueCommandButtonLocation != QueueCommandButtonLocation.PawnGizmoPerPawn)
        {
            yield break;
        }

        if (__instance.IsColonistPlayerControlled)
        {
            yield return new Command_Toggle
            {
                defaultLabel = "Merthsoft.QueueCommandsButton.QueueCommandsLabel".Translate(),
                defaultDesc = "Merthsoft.QueueCommandsButton.QueueCommandsDescription".Translate(),
                isActive = () => QueueCommandsButtonMod.IsEnabled(__instance),
                toggleAction = () => QueueCommandsButtonMod.Toggle(__instance),
                icon = QueueCommandsButtonMod.QueueIcon,
            };
        }
    }
}
