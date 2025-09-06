using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 10f;
    public ColorType projectileColor = ColorType.None;

    private void Update() {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Falls Projektil auf ColorField trifft
        ColorField field = collision.gameObject.GetComponent<ColorField>();
        if (field != null) {
            field.ApplyColor(projectileColor);
        } else {
            // Neues Farbfeld erzeugen
            GameObject newField = new GameObject("ColorField");
            newField.transform.position = transform.position;
            var sr = newField.AddComponent<SpriteRenderer>();
            var cf = newField.AddComponent<ColorField>();
            cf.ApplyColor(projectileColor);
        }

        Destroy(gameObject);
    }
}
