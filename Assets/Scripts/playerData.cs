[System.Serializable]
public class PlayerData
{
    public string playerName;
    public float health = 100f;
    public int score = 0; //oder was anderes

    // Attackenfarben
    public ColorType projectileColor = ColorType.Red;
    public ColorType bombColor = ColorType.Green;
    public ColorType beamColor = ColorType.Yellow;

    // Attackenwerte Bombe

    public float radius = 5f;
    public float duration = 12f;
    public float damage = 20f;

    // Später: gesammelte Items, Powerups, etc.
}
