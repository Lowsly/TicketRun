using UnityEngine;

public class Rocas : MonoBehaviour
{
    public GameObject Egg;
    private GameObject spawnedEgg;
    public int points;
    void Start()
    {
        if(points > 1 && Random.Range(0,3) == 0)
        {
            spawnedEgg = Instantiate(Egg, transform.position, Quaternion.identity);
            spawnedEgg.transform.localScale*=2;
            spawnedEgg.GetComponent<Animator>().SetFloat("speed", 2);
            spawnedEgg.transform.SetParent(transform);
        }
    }
    void Update()
    {
        if(spawnedEgg !=null)
        spawnedEgg.transform.position = transform.position;
    }
}
