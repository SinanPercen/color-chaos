using UnityEngine;

public enum ColorType { None, Blue, Red, Yellow, Orange, Purple, Green, Gray  }

public class ColorField : MonoBehaviour
{
    public ColorType currentColor = ColorType.None;
    private MeshRenderer mr;

    private void Awake()
    {
        // MeshRenderer sicherstellen
        mr = GetComponent<MeshRenderer>();
        if (mr == null) mr = gameObject.AddComponent<MeshRenderer>();

        // Material sicherstellen
        if (mr.material == null)
            mr.material = new Material(Shader.Find("Standard"));

        // MeshFilter / Quad erzeugen
        if (GetComponent<MeshFilter>() == null)
        {
            var mf = gameObject.AddComponent<MeshFilter>();
            mf.mesh = CreateQuadMesh();
        }

        UpdateVisual();
    }

    public void ApplyColor(ColorType newColor)
    {
        currentColor = newColor;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (mr != null)
            mr.material.color = ColorFromType(currentColor);
    }

    Color ColorFromType(ColorType type)
    {
        switch (type)
        {
            case ColorType.Blue: return Color.blue;
            case ColorType.Red: return Color.red;
            case ColorType.Yellow: return Color.yellow;
            default: return Color.white;
        }
    }

    Mesh CreateQuadMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            new Vector3(-0.5f,0,0.5f),
            new Vector3(0.5f,0,0.5f),
            new Vector3(-0.5f,0,-0.5f),
            new Vector3(0.5f,0,-0.5f)
        };
        mesh.triangles = new int[] {0,1,2,2,1,3};
        mesh.RecalculateNormals();
        return mesh;
    }
}
