using UnityEngine;

public class ForestColliderChildren : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            // Skip flowers and grass by checking their names
            if (child.name.Contains("Flower") || child.name.Contains("Grass"))
            {
                continue; // Skip this object, move to the next one
            }

            // Add a Mesh Collider if none exists
            if (child.GetComponent<Collider>() == null)
            {
                MeshCollider meshCollider = child.gameObject.AddComponent<MeshCollider>();
                meshCollider.convex = false;
            }
        }
    }
}
