using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class GhostAIParent : MonoBehaviour
{
    public Transform Respawn_Location;
    public Transform Scatter;
    public LayerMask GhostCollider;

    [SerializeField] private Transform Up;
    [SerializeField] private Transform Down;
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right;

    public Transform movePoint;
    public Transform Target;
    public Animator animator;
    public bool hasExitedCage = false;
    public bool IsDead = false;
    
    private float temp = 1000;
    private int Direction;
    private bool GhostTimer = false;
    private bool Teleported = false;
    
    [SerializeField] private Transform TeleportBlockLeft;
    [SerializeField] private Transform TeleportBlockRight;
    

public float moveSpeed = 2;

    private float[] ChooseDirection;
    private bool[] ChooseDirectionEnabled;
    public int x;
    public int y;

    private Vector3 movementLock = new Vector3(0f, 0f, 0f);
    void Start()
    {
        movePoint.parent = null;
        Target.parent = null;
        ChooseDirectionEnabled = new bool[4];
        ChooseDirection = new float[4];
        Scatter.parent = null;
        Target.position = Scatter.position;
        setSpeed();
    }
    private void setSpeed()
    {
        switch (Globals.Level)
        {
            case 1:
                moveSpeed = Globals.baseGhostSpeed *.75f;
                break;
            case 2:
            case 3:
            case 4:
                moveSpeed = Globals.baseGhostSpeed * .85f;
                break;
            default:
                moveSpeed = Globals.baseGhostSpeed * .95f;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player") { Debug.Log("Player Collision"); }
        //else if (collision.gameObject.tag == "Intersection") { Debug.Log("BlinkyIntersectionHit"); }
        //else { Debug.Log("IDK what it collided with"); }

        switch (collision.gameObject.tag)
        {
            case "Player":
                if (Globals.GhostKillable == true)
                {
                    IsDead = true;
                    animator.SetBool("IsDead", IsDead);
                    Globals.score += 200;
                    UpdateTarget();
                }
                else if (!IsDead)
                {
                    Globals.PlayerDead = true;
                }
                break;
            case "Intersection":
                UpdateTarget();
                break;
            case "TeleportBlockRight":
                if (!Teleported)
                {
                    transform.position = TeleportBlockLeft.position;
                    movePoint.position = TeleportBlockLeft.position + new Vector3(1, 0);
                    StartCoroutine(EnableTeleportBlock(.5f));
                }
                break;
            case "TeleportBlockLeft":
                if (!Teleported)
                {
                    transform.position = TeleportBlockRight.position;
                    movePoint.position = TeleportBlockRight.position + new Vector3(-1, 0);
                    StartCoroutine(EnableTeleportBlock(.5f));
                }
                break;
            case "Respawn Location":
                //Debug.Log("AttemptedRespawn");
                if(IsDead)
                {
                    Debug.Log("Respawned");
                    IsDead = false;
                    animator.SetBool("IsDead", IsDead);
                }
                break;
        }
    }
    public virtual void UpdateTarget()
    {
    }
    void resetDirection()
    {
        for (int i = 0; i < 4; i++)
        {
            ChooseDirectionEnabled[i] = true;
        }
    }
    void setMovementlock()
    {

        movementLock.x = x;
        movementLock.y = y;
    }
    public virtual void exitCage() { }
    void FixedUpdate()
    {
        if (!hasExitedCage)
        {
            exitCage();
        }
        else
        {

            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                if (Physics2D.OverlapCircle(Left.position, .2f, GhostCollider)) { ChooseDirectionEnabled[0] = false; }
                if (Physics2D.OverlapCircle(Right.position, .2f, GhostCollider)) { ChooseDirectionEnabled[1] = false; }
                if (Physics2D.OverlapCircle(Up.position, .2f, GhostCollider)) { ChooseDirectionEnabled[2] = false; }
                if (Physics2D.OverlapCircle(Down.position, .2f, GhostCollider)) { ChooseDirectionEnabled[3] = false; }
                //Debug.Log($"{ChooseDirectionEnabled[0]}, {ChooseDirectionEnabled[2]}, {ChooseDirectionEnabled[2]}, {ChooseDirectionEnabled[3]}");

                if (!Globals.GhostKillable | IsDead)
                {

                    if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
                    {
                        ChooseDirection[0] = (Target.position - Left.position).sqrMagnitude;
                        ChooseDirection[1] = (Target.position - Right.position).sqrMagnitude;
                        ChooseDirection[2] = (Target.position - Up.position).sqrMagnitude;
                        ChooseDirection[3] = (Target.position - Down.position).sqrMagnitude;
                        //Debug.Log($"{ChooseDirection[0]}, {ChooseDirection[2]}, {ChooseDirection[2]}, {ChooseDirection[3]}");

                        for (int i = 0; i < 4; i++)
                        {
                            //Debug.Log(ChooseDirection[i]);
                            if (ChooseDirectionEnabled[i])
                            {
                                if (temp >= ChooseDirection[i])
                                {
                                    temp = ChooseDirection[i];
                                    Direction = i;
                                }
                            }
                        }
                        temp = 1000;
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
                    {
                        temp = Random.Range(0, 3);
                        if (!ChooseDirectionEnabled[(int)temp])
                        {
                            temp = 2;
                            for (int p= 0; p < 4; p++)
                            {
                                if (!ChooseDirectionEnabled[(int)temp])
                                {
                                    switch (temp)
                                    {
                                        case 0:
                                            temp = 3;
                                            break;
                                        case 1:
                                            Debug.Log("Something went wrong");
                                            break;
                                        case 2:
                                            temp = 0;
                                            break;
                                        case 3:
                                            temp = 1;
                                            break;
                                    }
                                }
                                else
                                {
                                    Direction = (int)temp;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Direction = (int)temp;
                        }
                        if (!GhostTimer)
                        {
                            switch (Globals.Level)
                            {
                                case 1:
                                    moveSpeed = Globals.baseGhostSpeed / 2;
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                    moveSpeed = Globals.baseGhostSpeed * .55f;
                                    break;
                                default:
                                    moveSpeed = Globals.baseGhostSpeed * .65f;
                                    break;
                            }

                            animator.SetBool("IsRunningAway", true);
                            switch (Globals.Level)
                            {
                                case 1:
                                    temp = 6;
                                    break;
                                case 2:
                                case 6:
                                case 10:
                                    temp = 5;
                                    break;
                                case 3:
                                    temp = 4;
                                    break;
                                case 4:
                                case 14:
                                    temp = 3;
                                    break;
                                case 5:
                                case 7:
                                case 8:
                                case 11:
                                    temp = 2;
                                    break;
                                case 9:
                                case 12:
                                case 13:
                                case 15:
                                case 18:
                                    temp = 1;
                                    break;
                                default:
                                    break;
                            }
                            if ((Globals.Level > 16) && Globals.Level != 18)
                            {
                                UpdateTarget();
                                Globals.GhostKillable = false;
                            }
                            else
                            {
                                StartCoroutine(GhostKillWarning(temp / 2));
                                StartCoroutine(GhostKillableToggleAfterDelay(temp));
                                GhostTimer = true;

                            }
                            temp = 1000;



                        }
                    }
            }

            //if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            //{
            //    for (int i = 0; i < 4; i++)
            //    {
            //        if (ChooseDirectionEnabled[i])
            //        {
            //            temp += 1;
            //        }
            //    }
            //    for (int i = 0; i < 4; i++)
            //    {
            //        if (temp == -1)
            //        {
            //            break;
            //        }
            //        else if (ChooseDirectionEnabled[i])
            //        {
            //            Direction = i;
            //            temp -= 1;
            //        }

            //    }
            //    temp = 1000;

                

                if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
                {
                    switch (Direction)
                    {
                        case 0:
                            //Debug.Log("left");
                            x = -1;
                            y = 0;
                            resetDirection();
                            ChooseDirectionEnabled[1] = false;
                            break;
                        case 1:
                            //Debug.Log("Right");
                            x = 1;
                            y = 0;
                            resetDirection();
                            ChooseDirectionEnabled[0] = false;
                            break;
                        case 2:
                            //Debug.Log("Up");
                            x = 0;
                            y = 1;
                            resetDirection();
                            ChooseDirectionEnabled[3] = false;
                            break;
                        case 3:
                            //Debug.Log("Down");
                            x = 0;
                            y = -1;
                            resetDirection();
                            ChooseDirectionEnabled[2] = false;
                            break;
                    }
                }
                if (Mathf.Abs(x) == 1f)                                                                                            //Move x
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x, 0f, 0f), .2f, GhostCollider))                  //Collision Check
                    {
                        //Debug.Log("Thisdoeshappen");
                        setMovementlock();
                        movePoint.position += movementLock;

                        animator.SetFloat("Horizontal", movementLock.x);                                                                        //Set Animation
                        animator.SetFloat("Vertical", movementLock.y);
                    }
                    else if (!Physics2D.OverlapCircle(movePoint.position + movementLock, .2f, GhostCollider))
                    {
                        movePoint.position += movementLock;
                    }
                }
                else if (Mathf.Abs(y) == 1f)                                                                                         //Move y
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, y, 0f), .2f, GhostCollider))                    //Collision Check
                    {
                        setMovementlock();
                        movePoint.position += movementLock;

                        animator.SetFloat("Horizontal", movementLock.x);                                                                        //Set Animation
                        animator.SetFloat("Vertical", movementLock.y);
                    }
                    else if (!Physics2D.OverlapCircle(movePoint.position + movementLock, .2f, GhostCollider))
                    {
                        movePoint.position += movementLock;
                    }
                }
            }
        }
    }
    



IEnumerator GhostKillWarning(float time)
    {
        yield return new WaitForSeconds(time);
        //animator.SetBool("Warning", true);
    }
    IEnumerator GhostKillableToggleAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        Globals.GhostKillable = false;
        GhostTimer = false;
        animator.SetBool("Warning", false);
        animator.SetBool("IsRunningAway", false);
        setSpeed();
    }

    IEnumerator EnableTeleportBlock(float time)
    {
        Teleported = true;
        yield return new WaitForSeconds(time);
        Teleported = false;
    }
}







