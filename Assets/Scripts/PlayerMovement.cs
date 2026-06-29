using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 6f;
    [SerializeField] private Camera gameplayCamera;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Vector2 clampOffset = new Vector2(1f, -1f);

    private SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (gameplayCamera == null) gameplayCamera = Camera.main;
        if (playerCollider == null) playerCollider = GetComponent<Collider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
        ClampToScreen();
    }

    private void ClampToScreen()
    {
        if (gameplayCamera == null) return;

        float zDist = Mathf.Abs(transform.position.z - gameplayCamera.transform.position.z);
        Vector3 min = gameplayCamera.ViewportToWorldPoint(new Vector3(0f, 0f, zDist));
        Vector3 max = gameplayCamera.ViewportToWorldPoint(new Vector3(1f, 1f, zDist));

        minViewport += new Vector3(clampOffset.x, clampOffset.y, 0f);
        maxViewport += new Vector3(clampOffset.x, clampOffset.y, 0f);

        Vector2 extents = GetExtents();

        float clampedX = Mathf.Clamp(rb.position.x, min.x + extents.x, max.x - extents.x);
        float clampedY = Mathf.Clamp(rb.position.y, min.y + extents.y, max.y - extents.y);

        rb.position = new Vector2(clampedX, clampedY);
    }

    private Vector2 GetExtents()
    {
        if (playerCollider != null) return playerCollider.bounds.extents;
        if (playerSpriteRenderer != null) return playerSpriteRenderer.bounds.extents;
        return Vector2.zero;
    }
}