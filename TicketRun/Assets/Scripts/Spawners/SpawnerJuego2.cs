using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerJuego2 : MonoBehaviour
{
    public Transform background;
    public float timeToSpawn;
    private float timeSinceLastSpawn = 4;
    public GameObject prefab;
    private float _bh,_bw;
    void Start()
    {
        _bh = background.transform.localScale.y;
        _bw = background.transform.localScale.x;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeToSpawn)
        {
            Instantiate(prefab, new Vector2(_bw/1.5f, Random.Range(-_bh/3f,_bh/3.2f)), Quaternion.identity);
            timeSinceLastSpawn = 0;
        }
    }
}
