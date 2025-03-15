using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, 10f, target.position.z-5);
        transform.position = newPosition;
    }
}