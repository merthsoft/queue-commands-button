using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace Merthsoft.QueueCommandsButton;

[StaticConstructorOnStartup]
public class QueueCommandsButtonMod : Mod
{
    private static QueueCommandsButtonModSettings Settings;
    public static Texture2D QueueIcon;

    static bool isEnabledGlobal = false;
    static readonly Dictionary<Pawn, bool> isEnabledDictionary = [];

    public static QueueCommandButtonLocation QueueCommandButtonLocation => Settings.QueueCommandButtonLocation;

    public static bool IsEnabled(Pawn p)
        => QueueCommandButtonLocation switch
        {
            QueueCommandButtonLocation.PawnGizmoPerPawn => isEnabledDictionary.GetOrSet(p, false),
            _ => isEnabledGlobal,
        };

    public static void SetEnabled(Pawn p, bool value)
    {
        switch (QueueCommandButtonLocation)
        {
            case QueueCommandButtonLocation.PawnGizmoPerPawn:
                isEnabledDictionary[p] = value;
                break;
            default:
                isEnabledGlobal = value;
                break;
        }
    }

    public QueueCommandsButtonMod(ModContentPack content) : base(content)
    {
        Settings = GetSettings<QueueCommandsButtonModSettings>();

        var harmony = new Harmony("Merthsoft.QueueCommandsButton");
        harmony.PatchAll();

        LongEventHandler.ExecuteWhenFinished(() =>
        {
            QueueCommandsButtonMod.QueueIcon = ContentFinder<Texture2D>.Get("queue");
        });
    }

    public override string SettingsCategory()
        => "Merthsoft.QueueCommandsButton.SettingsCategory".Translate();

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listing = new Listing_Standard();
        listing.Begin(inRect);

        listing.Label("Merthsoft.QueueCommandsButton.QueueCommandButtonLocation".Translate());

        foreach (QueueCommandButtonLocation location in Enum.GetValues(typeof(QueueCommandButtonLocation)))
        {
            var isSelected = (Settings.QueueCommandButtonLocation == location);
            if (listing.RadioButton(
                label: "\t" + $"Merthsoft.QueueCommandsButton.QueueCommandButtonLocation.{location}".Translate(), 
                active: isSelected))
            {
                Settings.QueueCommandButtonLocation = location;
            }
        }

        listing.End();
    }

    public static void Toggle(Pawn p)
        => SetEnabled(p, !IsEnabled(p));
}
