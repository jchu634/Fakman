using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class GhostController : MonoBehaviour
{
    
    [SerializeField] private BoxCollider2D BottomCollider;
    [SerializeField] private CircleCollider2D TopCollider;
    public Animator Animator;
    
    // Start is called before the first frame update
    void Start()
    {
        Animator.SetFloat("Horizontal", 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Globals.GhostKillable == true)
            {
                Animator.SetBool("IsDead", true);
                BottomCollider.enabled = false;
                TopCollider.enabled = false;
                Globals.score += 200;
            //    Destroy(gameObject);
            }
            else
            {
                Globals.PlayerDead = true;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Globals.GhostKillable == true)
        {
            Animator.SetBool("IsRunningAway", true);
            StartCoroutine(GhostKillableToggleAfterDelay(8));
            AIDestinationSetter.run = true;
            
        }

    }
    IEnumerator GhostKillableToggleAfterDelay(float time)
    {

        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        Globals.GhostKillable = false;
        Animator.SetBool("IsRunningAway", false);
        AIDestinationSetter.run = false;
    }
}
