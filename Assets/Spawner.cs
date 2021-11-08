using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject original;
    public Vector2 position;
    public float start;
    public float frequency;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", start, frequency);
    }
    void Spawn()
    {
        Instantiate(original, position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
