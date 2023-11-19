using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePellet_wAnim : ScorePellet
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        switch (Globals.Level)
        {
            case 0:
                animator.SetInteger("ItemID", 0);
                break;
            case 1:
                animator.SetInteger("ItemID", 1);
                break;
            case 2:
                animator.SetInteger("ItemID", 2);
                break;
            case 3:
            case 4:
                animator.SetInteger("ItemID", 3);
                break;
            case 5:
            case 6:
                animator.SetInteger("ItemID", 4);
                break;
            case 7:
            case 8:
                animator.SetInteger("ItemID", 5);
                break;
            case 9:
            case 10:
                animator.SetInteger("ItemID", 6);
                break;
            case 11:
            case 12:
                animator.SetInteger("ItemID", 7);
                break;
            default:
                animator.SetInteger("ItemID", 8);
                break;
        }
        //animator.SetInteger("")
    }

    // Update is called once per frame
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Globals.PelletsCollected += 1;
            
        switch (Globals.Level)
        {
            case 0:
                    Globals.score += 10;
                    break;
            case 1:
                    Globals.score += 100;
                    break;
            case 2:
                    Globals.score += 300;
                    break;
            case 3:
            case 4:
                    Globals.score += 500;
                    break;
            case 5:
            case 6:
                    Globals.score += 700;
                    break;
            case 7:
            case 8:
                    Globals.score += 1000;
                    break;
            case 9:
            case 10:
                    Globals.score += 2000;
                    break;
            case 11:
            case 12:
                    Globals.score += 3000;
                    break;
            default:
                    Globals.score += 5000;
                    break;
        }
            
            Destroy(gameObject);

        }
    }
}
