using UnityEngine;

public class SpriteColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isClicked = false;

    // Called when the script is first initialized
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Called when the sprite is clicked
    void OnMouseDown()
    {

        // Toggle the color between green and gray
        isClicked = !isClicked;

        // Change the color based on the flag
        if (isClicked)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }

        Debug.Log("Clicked on " + gameObject.name);

    }
    public Color returnColor()
    {
        return spriteRenderer.color;
    }
}