using UnityEngine;

public class ProceduralHouse : MonoBehaviour
{
    public Vector3 size = new Vector3(6f, 3f, 6f);
    public Vector3 doorSize = new Vector3(1.2f, 2f, 0.1f);

    void Start()
    {
        Build();
    }

    void Build()
    {
        float wallThickness = 0.1f;
        float halfX = size.x / 2f;
        float halfY = size.y / 2f;
        float halfZ = size.z / 2f;

        // Floor
        CreatePart(new Vector3(0, -0.05f, 0), new Vector3(size.x, 0.1f, size.z));
        // Roof
        CreatePart(new Vector3(0, size.y, 0), new Vector3(size.x, 0.1f, size.z));

        // Back wall
        CreatePart(new Vector3(0, halfY, -halfZ), new Vector3(size.x, size.y, wallThickness));
        // Left wall
        CreatePart(new Vector3(-halfX, halfY, 0), new Vector3(wallThickness, size.y, size.z));
        // Right wall
        CreatePart(new Vector3(halfX, halfY, 0), new Vector3(wallThickness, size.y, size.z));

        // Front wall divided into two segments to make a doorway
        float doorOffset = doorSize.x / 2f + 0.01f;
        float frontSegmentWidth = halfX - doorOffset;
        CreatePart(new Vector3(-frontSegmentWidth / 2f - doorOffset / 2f, halfY, halfZ),
                   new Vector3(frontSegmentWidth, size.y, wallThickness));
        CreatePart(new Vector3(frontSegmentWidth / 2f + doorOffset / 2f, halfY, halfZ),
                   new Vector3(frontSegmentWidth, size.y, wallThickness));
    }

    void CreatePart(Vector3 localPos, Vector3 scale)
    {
        GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.transform.SetParent(transform);
        part.transform.localPosition = localPos;
        part.transform.localScale = scale;
        part.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
