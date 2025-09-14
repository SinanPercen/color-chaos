using System;
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
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Initialisierung direkt nach Instanziierung
    public void Initialize(Vector3 direction, ColorType color)
    {
        rb.linearVelocity = direction.normalized * speed;

        Destroy(gameObject, lifetime); // Selbstzerstörung nach 'lifetime' Sekunden
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var dmg))
        {
            dmg.TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
