using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking";
    private Animator animator;

    [SerializeField] private Movement movement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
        animator.SetBool(IS_WALKING, movement.IsWalking());
    }

}
