using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 moveInput;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(x, y).normalized;
    }
    private void FixedUpdate () {
        rb.linearVelocity = moveInput * speed;
    }
}
