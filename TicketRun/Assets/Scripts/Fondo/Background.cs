using UnityEngine;
public class Background : MonoBehaviour
{
    public float scrollSpeed;
    private float offset;
    private Material material;
    public bool direction = false;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    void Update()
    {
        offset+= Time.deltaTime * scrollSpeed;
        material.SetTextureOffset("_MainTex", new Vector2(direction == false ? 0: offset, direction == false ? offset: 0));

    }
}
