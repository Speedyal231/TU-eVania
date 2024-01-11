using UnityEngine;

public class SpriteArray : MonoBehaviour
{
    // Public array of sprites
    public Sprite[] spriteArray;

    void Start()
    {
        // Accessing and using the sprites in the array
        foreach (Sprite sprite in spriteArray)
        {
            Debug.Log(sprite.name);
            // Perform other operations with the sprite
        }
    }
}
