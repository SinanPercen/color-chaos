[System.Serializable]
public class PlayerData
{
    public string playerName;
    public float health = 100f;
    public int score = 0; //oder was anderes

    // Attackenfarben
    public ColorType projectileColor = ColorType.Red;
    public ColorType bombColor = ColorType.Blue;
    public ColorType beamColor = ColorType.Yellow;

    // Später: gesammelte Items, Powerups, etc.
}
