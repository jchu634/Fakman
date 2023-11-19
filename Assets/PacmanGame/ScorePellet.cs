using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScorePellet : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Globals.PelletsCollected += 1;
            Globals.score += 10;
            //Debug.Log(Globals.score);
            //Debug.Log(Globals.PelletsCollected);
            Destroy(gameObject);
            
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
