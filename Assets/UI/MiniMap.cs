using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        Vector3 pos = target.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }
}
