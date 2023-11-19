using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI score;

    // Update is called once per frame
    void fixedUpdate()
    {
        score.text =  Globals.score.ToString();
    }
}
