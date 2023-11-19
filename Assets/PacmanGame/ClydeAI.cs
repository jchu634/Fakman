using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeAI : GhostAIParent
{
    public Transform Pacman;
    private int ExitCount = 0;
    public override void exitCage()
    {
        if (((Globals.PelletsCollected >= 90)&&(Globals.Level == 1)) | ((Globals.PelletsCollected >= 50)&&(Globals.Level == 2))| Globals.Level >=3)
            {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                switch (ExitCount)
                {
                    case 0:
                        x = -1;
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
        //Debug.Log((Pacman.position - transform.position).magnitude);
        if (IsDead)
        {
            Target.position = Respawn_Location.position;
        }
        else if (Globals.IsModeScatter | !((Pacman.position - transform.position).magnitude > 8))
        {
            Target.position = Scatter.position;
        }
        else
        {
            Target.position = Pacman.position;
            }
        
    }
}
