using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject original;
    public Vector2 position;
    public float start;
    public float frequency;
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
    private GameObject[] spawners = new GameObject[16];
    private Animator[] spawnerAnimators = new Animator[16];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = Instantiate(original, spawnPoints[i], Quaternion.identity);
            spawners[i].name = original.name;
            spawnerAnimators[i] = spawners[i].GetComponent<Animator>();
        }
        InvokeRepeating("Spawn", start, frequency);
    }
    void Spawn()
    {
        CancelInvoke("Spawn");
        interval = (rate * Time.timeSinceLevelLoad) + rateStart;
        interval = interval < 1.0f ? 1.0f : interval;
        //Debug.Log(interval);
        spawnerAnimators[Random.Range(0, 16)].SetTrigger("SpawnTrigger");
        Invoke("Spawn", interval);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
