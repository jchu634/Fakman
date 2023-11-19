 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPellet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Globals.GhostKillable = true;
            Globals.PelletsCollected += 1;
            Globals.score += 50;
            Destroy(gameObject);
        }
    }
    
}
