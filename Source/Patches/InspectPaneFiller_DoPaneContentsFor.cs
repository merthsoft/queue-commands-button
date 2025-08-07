using HarmonyLib;
using Merthsoft.QueueCommandsButton;
using RimWorld;
using System;
using UnityEngine;
using Verse;
using Verse.Sound;

[HarmonyPatch(typeof(InspectPaneFiller), nameof(InspectPaneFiller.DoPaneContentsFor))]
public static class InspectPaneFiller_DoPaneContentsFor
{
    public static void Postfix(ISelectable sel, Rect rect)
    {
        if (sel is not Pawn pawn || !pawn.IsColonistPlayerControlled)
            return;
        
        if (QueueCommandsButtonMod.QueueCommandButtonLocation != QueueCommandButtonLocation.PawnCardPerPawn
            && QueueCommandsButtonMod.QueueCommandButtonLocation != QueueCommandButtonLocation.PawnCardGlobal)
            return;

        var row = new WidgetRow(rect.x + rect.width - 26, rect.y + rect.height - 26);
        var enabled = QueueCommandsButtonMod.IsEnabled(null);
        row.ToggleableIcon(
            toggleable: ref enabled,
            tex: QueueCommandsButtonMod.QueueIcon,
            tooltip: "Merthsoft.QueueCommandsButton.QueueCommandsDescription".Translate());

        QueueCommandsButtonMod.SetEnabled(null, enabled);
    }
}
