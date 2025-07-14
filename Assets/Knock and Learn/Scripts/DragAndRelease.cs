using UnityEngine;
using UnityEngine.SceneManagement;

public class DragAndRelease : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _dragPos;
    private Vector3 _dragOffset;
    private Camera _mainCamera;
    private Rigidbody2D _rb;
    private bool _isDragging = false;

    public float launchForce = 500f;
    public float maxDragDistance = 2f;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
        _rb.isKinematic = true;
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _rb.linearVelocity = Vector2.zero;
    }

    private void OnMouseDrag()
    {
        _dragPos = GetMouseWorldPosition();

        // Optionally visualize drag line with Debug.DrawLine or LineRenderer (for later)
        float dragDistance = Vector2.Distance(_startPos, _dragPos);
        if (dragDistance > maxDragDistance)
        {
            Vector2 direction = (_dragPos - _startPos).normalized;
            _dragPos = _startPos + (Vector3)(direction * maxDragDistance);
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        Vector2 direction = (_startPos - _dragPos).normalized;
        float distance = Vector2.Distance(_startPos, _dragPos);

        if (distance > maxDragDistance)
            distance = maxDragDistance;

        _rb.isKinematic = false; // Enable physics
        _rb.AddForce(direction * distance * launchForce);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        return _mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}