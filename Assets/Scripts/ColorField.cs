using System;
using UnityEngine;

public class ColorField : MonoBehaviour
{
    public ColorType currentColor = ColorType.None;
    public float lifetime; // Dauer, wie lange die Fläche sichtbar bleibt

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    public void ApplyColor(ColorType newColor)
    {
        currentColor = newColor;
    }
    public void SetLifetime(float newLifetime)
    {
    lifetime = newLifetime;
    }
}