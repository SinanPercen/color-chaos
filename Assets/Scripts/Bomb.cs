using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Bomb Settings")]
    public float radius = 3f;
    public float duration = 12f;     // Standard-Dauer
    public float damage = 20f;
    public ColorType bombColor;

    [Header("Visuals")]
    public GameObject explosionEffect;   // Optionales Partikel/Prefab
    public GameObject colorFieldPrefab;  // Das ColorField, das gefärbt wird


    private float timer;

    private void Start()
    {
        timer = duration;
        ApplyEffect(); // Schaden & ColorField einmal anwenden
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
            Destroy(gameObject);
    }

    private void ApplyEffect()
    {
        // 1️⃣ Gegner im Radius treffen
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var col in hits)
        {
            if (col.TryGetComponent<IDamageable>(out var dmg))
            {
                dmg.TakeDamage(damage);
            }
        }

        // 2️⃣ Optional: Explosionseffekt
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // 3️⃣ ColorField einmal erzeugen
        if (colorFieldPrefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * 0.01f; // leicht über Boden
            GameObject fieldGO = Instantiate(colorFieldPrefab, spawnPos, Quaternion.identity);

            ColorField field = fieldGO.GetComponent<ColorField>();
            if (field != null)
            {
                field.ApplyColor(bombColor); // Farbe setzen
                field.SetLifetime(duration);  // Dauer setzen
            }

            Debug.Log($"Bomb erzeugt ColorField mit Farbe: {bombColor}");
        }
    }

    public void setBombColor(ColorType newColor)
    {
        bombColor = newColor;
    }
}
