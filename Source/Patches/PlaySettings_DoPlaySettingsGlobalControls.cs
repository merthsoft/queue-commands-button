using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Merthsoft.QueueCommandsButton.Patches;


[HarmonyPatch(typeof(PlaySettings), nameof(PlaySettings.DoPlaySettingsGlobalControls))]
public static class PlaySettings_DoPlaySettingsGlobalControls
{
    public static void Postfix(WidgetRow row)
    {
        if (QueueCommandsButtonMod.QueueCommandButtonLocation != QueueCommandButtonLocation.GameSettings)
            return;

        var enabled = QueueCommandsButtonMod.IsEnabled(null);
        row.ToggleableIcon(
            toggleable: ref enabled, 
            tex: QueueCommandsButtonMod.QueueIcon, 
            tooltip: "Merthsoft.QueueCommandsButton.QueueCommandsDescription".Translate());
        
        QueueCommandsButtonMod.SetEnabled(null, enabled);
    }
}
