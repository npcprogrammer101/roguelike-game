using UnityEngine;

public class Player : MonoBehaviour
{   
    [Header("Movements")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;

    [Header("Crouching")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float playerHeight = 2.8f;

    [Header("Keybinds")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.C;

    [Header("System")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider col;

    private bool isWalking;
    private bool isCrouching;

    public MovementState state;

    public enum MovementState{
        walking,
        sprinting,
        crouching
    }

    private void Start()
    {

    }
    public void Update()
    {
        PlayerMovement();
        StateHandler();
        MyInput();
    }

    public bool IsWalking(){
        return isWalking;
    }
    public bool IsCrouching() {
        return isCrouching;
    }

    private void PlayerInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float interactDistance = 2f;
        Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance);


    }
    
    private void PlayerMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        
        if (!canMove) {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDir = moveDirX;
            } else {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                } else {
                    // Cannot move in any direction
                }
            }

        }
        
        if (canMove) {
            transform.position += moveDir * moveDistance;
        }
        
        isWalking = moveDir != Vector3.zero; // if movement is away from 0

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // slerp is for rotation, lerp is for movement
    }

    private void MyInput() {
        
        if (Input.GetKeyDown(crouchKey)) {
            isCrouching = true;
            playerHeight = 1.4f;
            col.center = new Vector3(0f, 0.8f, 0f);
            col.size = new Vector3(col.size.x, playerHeight/2, col.size.z);
        }

        if (Input.GetKeyUp(crouchKey)) {
            isCrouching = false;
            playerHeight = 2.8f;
            col.center = new Vector3(0f, 1.5f, 0f);
            col.size = new Vector3(col.size.x, playerHeight, col.size.z);
        }
    }

    private void StateHandler() {
        if (Input.GetKey(crouchKey)) {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else {
            if (Input.GetKey(sprintKey)) {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }
            else {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }
        }
    }
}
