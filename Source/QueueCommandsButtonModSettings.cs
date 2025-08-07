using System;
using Verse;

namespace Merthsoft.QueueCommandsButton;

public class QueueCommandsButtonModSettings : ModSettings
{
    public QueueCommandButtonLocation QueueCommandButtonLocation = QueueCommandButtonLocation.PawnGizmoGlobal;

    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Values.Look(ref QueueCommandButtonLocation, "QueueCommandButtonLocation", QueueCommandButtonLocation.PawnGizmoGlobal);
    }
}
