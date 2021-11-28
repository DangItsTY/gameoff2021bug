using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Text text = GetComponent<Text>();
        int score = PlayerPrefs.GetInt("score");
        text.text = "Score: " + score;
    }
}
