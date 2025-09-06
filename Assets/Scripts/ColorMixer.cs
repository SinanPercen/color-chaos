using UnityEngine;

public static class ColorMixer {
    public static ColorType Mix(ColorType a, ColorType b) {
        if (a == b) return a;

        // Beispiele für Mischungen
        if ((a == ColorType.Red && b == ColorType.Yellow) || (a == ColorType.Yellow && b == ColorType.Red))
            return ColorType.Orange;

        if ((a == ColorType.Red && b == ColorType.Blue) || (a == ColorType.Blue && b == ColorType.Red))
            return ColorType.Purple;

        if ((a == ColorType.Yellow && b == ColorType.Blue) || (a == ColorType.Blue && b == ColorType.Yellow))
            return ColorType.Green;

        // Drei Farben gleichzeitig -> Chaos (Grau)
        if (a != ColorType.None && b != ColorType.None)
            return ColorType.Gray;

        return ColorType.None;
    }
}
