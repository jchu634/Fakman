using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BlinkyAI : GhostAIParent
{
    public Transform Pacman;
    public override void exitCage()
    {
        hasExitedCage = true;
    }
    public override void UpdateTarget()
    {
        if (IsDead)
        {
            Target.position = Respawn_Location.position;
        }
        else if (Globals.IsModeScatter)
        {
            Target.position = Scatter.position;
        }
        else
        {
            Target.position = Pacman.position;
        }
    }
}