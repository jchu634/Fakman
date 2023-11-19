using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class PinkyAI : GhostAIParent
{
    public Transform Pacman;
    public Transform PacMovePoint;
    private int ExitCount = 0;
    public override void exitCage()

    {       transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
            switch (ExitCount)
            {
                case 0:
                case 1:
                    x = 0;
                    y = 1;
                    break;
                case 2:
                    hasExitedCage = true;
                    return;
                    
            }
            ExitCount += 1;
            if (Mathf.Abs(x) == 1f)                                                                                            //Move x
                {
                    
                    movePoint.position += new Vector3(x, y, 0f) ;

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
            Target.position = Pacman.position + (PacMovePoint.position - Pacman.position) * 4;
        }
    }
}