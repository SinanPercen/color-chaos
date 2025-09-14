public static class ColorMixer
{
    public static ColorType MixColors(ColorType a, ColorType b)
    {
        if ((a == ColorType.Red && b == ColorType.Blue) || (a == ColorType.Blue && b == ColorType.Red))
            return ColorType.Purple; // Beispiel
        
        if ((a == ColorType.Red && b == ColorType.Yellow) || (a == ColorType.Yellow && b == ColorType.Red))
            return ColorType.Orange;

        if ((a == ColorType.Blue && b == ColorType.Yellow) || (a == ColorType.Yellow && b == ColorType.Blue))
            return ColorType.Green;

        return a; // Standard: keine Mischung
    }
}
