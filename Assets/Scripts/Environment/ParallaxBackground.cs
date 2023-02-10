using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] float parallaxEffectMultiplier = 1f;

    private Vector3 StartPos;
    private float textureUnitSizeX;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(Vector2.left * Time.deltaTime * parallaxEffectMultiplier);

        if (StartPos.x - textureUnitSizeX >= transform.position.x)
        {
            Debug.Log("the texture ran out");
            float offSetPosX = (StartPos.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(StartPos.x + offSetPosX, transform.position.y);
        }
    }
}
