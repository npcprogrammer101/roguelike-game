using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    [SerializeField] private Transform target;

    void LateUpdate() // Avoids camera jitter
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y+10, target.position.z-5); // Directly follow the target position
        }
    }

}