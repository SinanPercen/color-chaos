using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // nötig für MovePosition
    }

    // Wird vom Player Input Component aufgerufen
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
{
    // Kamera-Vektor
    Vector3 camForward = Camera.main.transform.forward;
    Vector3 camRight = Camera.main.transform.right;

    // Y-Achse ignorieren, damit Bewegung flach bleibt
    camForward.y = 0;
    camRight.y = 0;
    camForward.Normalize();
    camRight.Normalize();

    // Movement entlang Kamera
    Vector3 move = camForward * moveInput.y + camRight * moveInput.x;
    rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
}

}
