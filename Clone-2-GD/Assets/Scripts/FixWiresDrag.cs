using UnityEngine;

public class FixWiresDrag : MonoBehaviour
{
    public Vector3 originalPosition;
    public bool isDragging = false;
    private LineRenderer lineRenderer;
    private FixWiresTask fixWiresTask;

    void Start()
    {
        originalPosition = transform.position;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = GetComponent<SpriteRenderer>().color;
        lineRenderer.endColor = GetComponent<SpriteRenderer>().color;
        fixWiresTask = FindObjectOfType<FixWiresTask>(); // Find the FixWiresTask component in the scene
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        lineRenderer.positionCount = 0;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
        if (hitCollider != null && hitCollider.CompareTag("WireTarget") && hitCollider.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
        {
            transform.position = hitCollider.transform.position;
            fixWiresTask.CheckTaskCompletion();
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, originalPosition);
            lineRenderer.SetPosition(1, mousePosition);
        }
    }
}
