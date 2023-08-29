using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(-10f,10f)]
    public float scrollSpeed;
    private float offset;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset+= Time.deltaTime * scrollSpeed/10f;
        material.SetTextureOffset("_MainTex", new Vector2(offset,0));
    }
}
