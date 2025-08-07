using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Merthsoft.QueueCommandsButton.Patches;

[HarmonyPatch(typeof(FloatMenuMakerMap), nameof(FloatMenuMakerMap.GetOptions))]
public static class FloatMenuMakerMap_GetOptions
{
    public static void Postfix(List<FloatMenuOption> __result)
    {
        if (QueueCommandsButtonMod.QueueCommandButtonLocation != QueueCommandButtonLocation.FloatMenu)
            return;

        var item = FloatMenuOption.CheckboxLabeled(
            label: "Merthsoft.QueueCommandsButton.QueueCommandsLabel".Translate(),
            checkboxStateChanged: () => QueueCommandsButtonMod.Toggle(null),
            currentState: QueueCommandsButtonMod.IsEnabled(null)
        );

        item.orderInPriority = int.MinValue;
        item.Priority = (MenuOptionPriority)byte.MaxValue;

        __result.Add(item);
    }
}
