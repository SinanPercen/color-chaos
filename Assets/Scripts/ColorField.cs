using System;
using UnityEngine;



public class ColorField : MonoBehaviour
{

    [Header("Color Materials")]
    public Material redMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
    public Material orangeMaterial;
    public Material purpleMaterial;
    public Material grayMaterial;
    public ColorType currentColor = ColorType.Green;
    public float lifetime; // Dauer, wie lange die Fläche sichtbar bleibt

    public void ApplyColor(ColorType newColor)
    {
        currentColor = newColor;

        Renderer renderer = GetComponentInChildren<Renderer>();
    if (renderer == null) return;

    switch (newColor)
    {
        case ColorType.Red: renderer.material = redMaterial; break;
        case ColorType.Blue: renderer.material = blueMaterial; break;
        case ColorType.Yellow: renderer.material = yellowMaterial; break;
        case ColorType.Green: renderer.material = greenMaterial; break;
        case ColorType.Orange: renderer.material = orangeMaterial; break;
        case ColorType.Purple: renderer.material = purpleMaterial; break;
        case ColorType.Gray: renderer.material = grayMaterial; break;
        default: break;
    }
    }
    public void SetLifetime(float newLifetime)
    {
        lifetime = newLifetime;
        Destroy(gameObject, lifetime);
    }
}