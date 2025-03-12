
using UnityEngine;

public class Movement : MonoBehaviour
{   
    [SerializeField] // making private variable appear in the editor of the class it is located
    private float moveSpeed = 5f;
    //private float jumpPower = 10f;
    
    public void Update()
    {
        Vector2 inputVector = new Vector2(0,0); // the input vectors are 2d vectors therefore making it a 2d object is much cleaner

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }
        


        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.position += moveDir * Time.deltaTime * moveSpeed;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // slerp is for rotation, lerp is for movement

        
    }

    
}
