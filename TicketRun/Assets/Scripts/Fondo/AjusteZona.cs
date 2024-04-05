using UnityEngine;

public class AjusteZona : MonoBehaviour
{
     void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;
       
        float spriteWidth = spriteRenderer.sprite.bounds.size.x * transform.localScale.x;
        float spriteHeight = spriteRenderer.sprite.bounds.size.y * transform.localScale.y;

        float scaleFactorX = cameraWidth / spriteWidth;
        float scaleFactorY = cameraHeight / spriteHeight;

        transform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1);
    }
}