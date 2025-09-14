using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public float fireOffset = 0.5f;          
    public ColorType playerColor = ColorType.Blue;

    [Header("Beam Settings")]
    public GameObject beamPrefab;
    public float beamLength = 10f;
    public float beamDamage = 10f;
    public float beamDuration = 0.8f;
    public LayerMask beamHitMask; // nur Wände/Enemies
    public float beamSpawnOffset = 0.5f;

    [Header("Bomb")]
    public GameObject bombPrefab;
    public float bombRadius = 3f;
    public float bombDuration = 2f;
    public float bombDamage = 20f;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;
        rb.linearVelocity = move * moveSpeed;
    }

    #region Input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            Shoot();
    }
    public void OnBeam(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector3 target = GetMouseWorldPosition();

        // Richtung XZ-Ebene, Y ignorieren
        Vector3 direction = target - transform.position;
        direction.y = 0;
        direction.Normalize();

Vector3 spawnPos = transform.position + direction * 0.5f;
GameObject beamGO = Instantiate(beamPrefab, spawnPos, Quaternion.identity);
Beam beam = beamGO.GetComponent<Beam>();
if (beam != null)
{
    beam.Initialize(spawnPos, direction, beamLength, beamDuration, beamDamage, beamHitMask, 0.2f);
}

    }
public void OnBomb(InputAction.CallbackContext context)
{
    if (!context.performed) return;

    Vector3 target = GetMouseWorldPosition();
    Vector3 spawnPos = target + Vector3.up * 0.1f;

    GameObject bombGO = Instantiate(bombPrefab, spawnPos, Quaternion.identity);
    Bomb bomb = bombGO.GetComponent<Bomb>();
    if (bomb != null)
    {
        bomb.bombColor = playerColor;
        bomb.radius = bombRadius;
        bomb.duration = bombDuration;
        bomb.damage = bombDamage;
    }
}

    #endregion

    private void Shoot()
    {
        Vector3 target = GetMouseWorldPosition();
        Vector3 direction = (target - transform.position).normalized;

        Vector3 spawnPos = transform.position + direction * fireOffset;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(direction));

        Projectile projScript = proj.GetComponent<Projectile>();
        if (projScript != null)
            projScript.Initialize(direction, playerColor);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);
        return Vector3.zero;
    }
}
