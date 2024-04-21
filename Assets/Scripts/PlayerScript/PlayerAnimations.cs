using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerMovement Player;
    [SerializeField] private Animator Animator;

    private static bool _isChanged = false;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PressedE();
    }

    private void PressedE()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isChanged)
        {
            _isChanged = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isChanged)
        {
            _isChanged = false;
        }
    }

    private void FixedUpdate()  
    {
        HandleAnimation();
    }
    
    private void HandleAnimation()
    {
        if (!_isChanged)
        {
            WhiteAnimation();
        } 
        else if (_isChanged)
        {
            BlackAnimation();
        } 
    }

    private void BlackAnimation()
    {
        if (Mathf.Abs(Player.InputMove) == 0f)
        {
            Animator.CrossFade("Black_Idle", 0, 0);
        }
        else if (Mathf.Abs(Player.InputMove) > 0f)
        {
            Animator.CrossFade("Black_Walk", 0, 0);
        }
    }

    private void WhiteAnimation()
    {
        if (Mathf.Abs(Player.InputMove) == 0f)
        {
            Animator.CrossFade("White_Idle", 0, 0);
        }
        else if (Mathf.Abs(Player.InputMove) > 0f)
        {
            Animator.CrossFade("White_Walk", 0, 0);
        }
    }
}
