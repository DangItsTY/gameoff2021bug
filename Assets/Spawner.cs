using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject original;
    public Vector2 position;
    public float start;
    public float frequency;
    public float vx = 2f;
    public float rate = -5.0f / 180.0f;
    public float rateStart = 6.0f;
    private float interval;
    private Vector2[] spawnPoints = new Vector2[16] {
        new Vector2(-7.0f, 3.0f),
        new Vector2(-5.0f, 3.0f),
        new Vector2(-7.0f, 1.0f),
        new Vector2(-5.0f, 1.0f),
        new Vector2(-7.0f, -1.0f),
        new Vector2(-5.0f, -1.0f),
        new Vector2(-7.0f, -3.0f),
        new Vector2(-5.0f, -3.0f),
        new Vector2(5.0f, 3.0f),
        new Vector2(7.0f, 3.0f),
        new Vector2(5.0f, 1.0f),
        new Vector2(7.0f, 1.0f),
        new Vector2(5.0f, -1.0f),
        new Vector2(7.0f, -1.0f),
        new Vector2(5.0f, -3.0f),
        new Vector2(7.0f, -3.0f),
    };
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", start, frequency);
    }
    void Spawn()
    {
        CancelInvoke("Spawn");
        interval = (rate * Time.timeSinceLevelLoad) + rateStart;
        interval = interval < 1.0f ? 1.0f : interval;
        Debug.Log(interval);
        position = spawnPoints[Random.Range(0, 16)];
        GameObject newObject = Instantiate(original, position, Quaternion.identity);
        newObject.name = original.name;
        newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(vx, 0f);
        Invoke("Spawn", interval);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
