using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexScrolling : MonoBehaviour
{
    [SerializeField]
    private float parallaxEffectMultiplier = 0.5f;
    //private float parallaxEffectMultiplierY = 0.05f;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;
    [SerializeField]
    private bool infiniteHorizontal;
    [SerializeField]
    private bool infiniteVertical;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }
    private void LateUpdate()
    {
        Vector3 deltaMovement = new Vector3(cameraTransform.position.x - lastCameraPosition.x,0,0);
       

        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = cameraTransform.position;

        if(infiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }
        if(infiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
            }
        }
       
    }
}
