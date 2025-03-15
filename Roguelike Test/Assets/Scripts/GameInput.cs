using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions; // by utilising player inputs, it allows the camera to move smoothly and makes the code optimised
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // the input vectors are 2d vectors therefore making it a 2d object is much cleaner

        inputVector = inputVector.normalized;
        return inputVector;
    }
}
