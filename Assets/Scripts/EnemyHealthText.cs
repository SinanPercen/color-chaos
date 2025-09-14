using UnityEngine;
using TMPro;

public class EnemyHealthText : MonoBehaviour
{
    public Enemy enemy;         // Referenz auf Enemy-Skript
    public TMP_Text healthText; // Referenz auf TextMeshPro Text

    void Update()
    {
        if (enemy != null && healthText != null)
            healthText.text = $"HP: {enemy.health:0}";
    }
}
