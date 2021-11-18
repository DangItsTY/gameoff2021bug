using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCombo : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Text text = GetComponent<Text>();
        int combo = PlayerPrefs.GetInt("combo");
        text.text = "" + combo;
    }
}
