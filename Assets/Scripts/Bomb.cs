using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Bomb Settings")]
    public float radius = 3f;
    public float duration;
    public float damage = 20f;
    public ColorType bombColor = ColorType.Red;

    [Header("Visuals")]
    public GameObject explosionEffect; // optionales Partikel/Prefab
    public GameObject colorFieldPrefab; // das ColorField-Prefab, das gefärbt wird

    private float timer;

    void Awake()
    {
        Debug.Log("Awake wird zuerst aufgerufen");
        duration = 12f;

}


    private void Start()
    {
        timer = duration;
        // Effekt sofort anwenden
        ApplyEffect();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
            Destroy(gameObject);
    }

    private void ApplyEffect()
    {
        // Gegner im Radius treffen
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var col in hits)
        {
            if (col.TryGetComponent<IDamageable>(out var dmg))
                dmg.TakeDamage(damage);
            else if (col.CompareTag("Enemy"))
            {
                var enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(damage);
            }
        }

        // Optional: Partikel/Explosion
        //if (explosionEffect != null)
         //   Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // ColorField auf dem Boden erzeugen
        if (colorFieldPrefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * 0.01f; // leicht über dem Boden
            GameObject fieldGO = Instantiate(colorFieldPrefab, spawnPos, Quaternion.identity);
            ColorField field = fieldGO.GetComponent<ColorField>();
            if (field != null)
            {
                field.ApplyColor(bombColor);
                field.SetLifetime(duration);

            }
        }
    }
}
