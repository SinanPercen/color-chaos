using UnityEngine;

public class ColorField : MonoBehaviour {
    public ColorType currentColor = ColorType.None;
    private SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ApplyColor(ColorType newColor) {
        currentColor = ColorMixer.Mix(currentColor, newColor);
        UpdateVisual();
        TriggerEffect(); // Hier kannst du Effekte einbauen
    }

    private void UpdateVisual() {
        switch (currentColor) {
            case ColorType.Red: sr.color = Color.red; break;
            case ColorType.Yellow: sr.color = Color.yellow; break;
            case ColorType.Blue: sr.color = Color.blue; break;
            case ColorType.Green: sr.color = Color.green; break;
            case ColorType.Orange: sr.color = new Color(1f, 0.5f, 0f); break;
            case ColorType.Purple: sr.color = new Color(0.5f, 0f, 0.5f); break;
            case ColorType.Gray: sr.color = Color.gray; break;
            default: sr.color = Color.white; break;
        }
    }

    private void TriggerEffect() {
        // Beispiel: Effekte je nach Farbe
        if (currentColor == ColorType.Green) {
            Debug.Log("Heilfeld entsteht!");
        } else if (currentColor == ColorType.Orange) {
            Debug.Log("Explosion!");
        } else if (currentColor == ColorType.Purple) {
            Debug.Log("Giftwolke!");
        }
    }
}
