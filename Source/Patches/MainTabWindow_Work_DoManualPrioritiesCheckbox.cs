using HarmonyLib;
using RimWorld;
using System;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Merthsoft.QueueCommandsButton.Patches;

[HarmonyPatch(typeof(MainTabWindow_Work), "DoManualPrioritiesCheckbox")]
public static class MainTabWindow_Work_DoManualPrioritiesCheckbox
{
    static readonly Rect toggleRect = new(155f, 5f, 200f, 30f);

    public static void Postfix()
    {
        if (QueueCommandsButtonMod.QueueCommandButtonLocation != QueueCommandButtonLocation.WorkTab)
            return;

        var enabled = QueueCommandsButtonMod.IsEnabled(null);
        Widgets.CheckboxLabeled(
            rect: toggleRect,
            label: "Merthsoft.QueueCommandsButton.QueueCommandsLabel".Translate(),
            ref enabled
        );
        QueueCommandsButtonMod.SetEnabled(null, enabled);

        TooltipHandler.TipRegion(toggleRect, "Merthsoft.QueueCommandsButton.QueueCommandsDescription".Translate());
    }
}