using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Globals
{
    public static float lives = 3;
    public static int Level = 1;
    public static bool IsModeScatter;
    public static int PelletsCollected = 0;
    public static bool PlayerDead;
    public static int score = 0;
    public static bool GhostKillable;
    public static Resolution currentResolution;
    public static float basePacmanSpeed = 2.5f;
    public static float baseGhostSpeed = 2.67f;
    //static void resetGlobals()
    //{
    //    lives = 3;
    //    Level = 1;
    //    score = 0;

    //}
}
public class PersistantGlobalsScript : MonoBehaviour
{
    
        static PersistantGlobalsScript instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this; // In first scene, make us the singleton.
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
        }

    public void resetGlobals()
    {
        Globals.lives = 3;
        Globals.Level = 1;
        Globals.score = 0;
    }
}

