using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBlock : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 originalPosition;

    void Update()
    {
        if (isDragging)
        {
            // Update block position based on mouse/touch input
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }
    }

    void OnMouseDown()
    {
        // Start dragging when the block is clicked
        isDragging = true;
        originalPosition = transform.position;
    }

    void OnMouseUp()
    {
        // Stop dragging and snap to a grid or other blocks
        isDragging = false;
        SnapToGrid();
    }

    void SnapToGrid()
    {
        // Implement logic to snap the block to a grid or other blocks
        // For simplicity, let's just round the position to the nearest whole number
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }
}
