using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyAI : GhostAIParent
{
    public Transform Pacman;
    public Transform PacMovePoint;
    public Transform Blinky;
    private int ExitCount = 0;
    public override void exitCage()
    {
        if (((Globals.Level == 1) && (Globals.PelletsCollected >= 30))| Globals.Level>=2)

        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                switch (ExitCount)
                {
                    case 0:
                        x = 1;
                        y = 0;
                        break;
                    case 1:
                    case 2:
                        x = 0;
                        y = 1;
                        break;
                    case 3:
                        hasExitedCage = true;
                        return;
                }
                ExitCount += 1;
                if (Mathf.Abs(x) == 1f)                                                                                            //Move x
                {

                    movePoint.position += new Vector3(x, y, 0f);

                    animator.SetFloat("Horizontal", x);                                                                        //Set Animation
                    animator.SetFloat("Vertical", y);

                }
                else if (Mathf.Abs(y) == 1f)                                                                                         //Move y
                {
                    movePoint.position += new Vector3(x, y, 0f);

                    animator.SetFloat("Horizontal", x);                                                                        //Set Animation
                    animator.SetFloat("Vertical", y);

                }
            }
        }
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
        else { Target.position = Blinky.position + (((Pacman.position + ((PacMovePoint.position - Pacman.position) * 2)) - Blinky.position) * 2); }
        
    }

}
