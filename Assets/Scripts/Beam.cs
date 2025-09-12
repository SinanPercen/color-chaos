using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [Header("Beam Settings")]
    public float length = 10f;
    public float duration = 0.8f;
    public float damage = 10f;
    public LayerMask hitMask = Physics.DefaultRaycastLayers;
    public float beamHeightOffset = 0.2f; // Abstand über Spieler/Boden

    [Header("Visualization")]
    public LineRenderer lineRenderer;

    // intern
    private Vector3 origin;
    private Vector3 direction;
    private float timer;
    private HashSet<Collider> hitColliders = new HashSet<Collider>();

    // Aufruf direkt nach Instantiate
    public void Initialize(Vector3 origin, Vector3 direction, float length, float duration, float damage, LayerMask mask, float heightOffset = 0.2f)
    {
        this.origin = origin;
        this.direction = direction.normalized;
        this.length = length;
        this.duration = duration;
        this.damage = damage;
        this.hitMask = mask;
        this.beamHeightOffset = heightOffset;
        timer = duration;

        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.useWorldSpace = true;
        }

        UpdateBeamVisual();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        UpdateBeamVisual();

        // RaycastAll für alle Objekte entlang des Strahls
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, length, hitMask);
        foreach (var h in hits)
        {
            Collider col = h.collider;
            if (!hitColliders.Contains(col))
            {
                hitColliders.Add(col);

                // Schaden anwenden
                if (col.TryGetComponent<IDamageable>(out var dmg))
                    dmg.TakeDamage(damage);
            }
        }

        if (timer <= 0f)
            Destroy(gameObject);
    }

    private void UpdateBeamVisual()
    {
        // Endpunkt berechnen
        Vector3 endPoint = origin + direction * length;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, length, hitMask))
        {
            // Leicht über dem getroffenen Objekt positionieren
            endPoint = hit.point + Vector3.up * beamHeightOffset;
        }

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, new Vector3(origin.x, origin.y + beamHeightOffset, origin.z));
            lineRenderer.SetPosition(1, new Vector3(endPoint.x, endPoint.y + beamHeightOffset, endPoint.z));
        }
    }
}
