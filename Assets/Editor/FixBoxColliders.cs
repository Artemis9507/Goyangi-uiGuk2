using UnityEngine;
using UnityEditor;

public class FixBoxColliders : MonoBehaviour
{
    [MenuItem("Tools/Fix Negative Scale BoxColliders")]
    static void FixColliders()
    {
        BoxCollider[] colliders = FindObjectsByType<BoxCollider>(FindObjectsSortMode.None);
        foreach (BoxCollider bc in colliders)
        {
            Transform t = bc.transform;
            Vector3 lossyScale = t.lossyScale;

            if (lossyScale.x < 0 || lossyScale.y < 0 || lossyScale.z < 0)
            {
                Debug.Log($"Fixing {bc.name} with negative scale {lossyScale}");

                // Make sure the collider is attached to an object with positive local scale
                t.localScale = new Vector3(
                    Mathf.Abs(t.localScale.x),
                    Mathf.Abs(t.localScale.y),
                    Mathf.Abs(t.localScale.z)
                );
            }
        }

        Debug.Log("Done fixing BoxColliders!");
    }
}
