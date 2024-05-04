using UnityEngine;
public class Background : MonoBehaviour
{
    public float scrollSpeed;
    private float offset;
    private Material material;
    private float distance = 0.0f;
    public bool direction = false;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset+= Time.deltaTime * scrollSpeed;
        distance += scrollSpeed * Time.deltaTime;
         int distanceInt = Mathf.RoundToInt(distance);
        material.SetTextureOffset("_MainTex", new Vector2(direction == false ? 0: offset, direction == false ? offset: 0));
        //Debug.Log(distanceInt);
        //text.text = string.Format("distancia = {0}", distanceInt);

    }
}
