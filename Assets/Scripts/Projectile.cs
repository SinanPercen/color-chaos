using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;           // Fluggeschwindigkeit
    public float lifetime = 5f;         // Lebensdauer des Projektils
    public ColorType projectileColor;   // Farbe (noch optional)

    private Rigidbody rb;
    public float damage = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.useGravity = false;
        //rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    // Initialisierung direkt nach Instanziierung
    public void Initialize(Vector3 direction, ColorType color)
    {
        rb.linearVelocity = direction.normalized * speed;

        Destroy(gameObject, lifetime); // Selbstzerstörung nach 'lifetime' Sekunden
    }

    private void OnCollisionEnter(Collision collision)
    {
        //

    }

    private void OnTriggerEnter(Collider other)
    {
        // Schaden anwenden, falls Gegner
        if (other.CompareTag("Enemy") && other.TryGetComponent<IDamageable>(out var dmg))
        {
            dmg.TakeDamage(damage);
        }

        // Projectile zerstören, wenn Gegner oder Wand
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
