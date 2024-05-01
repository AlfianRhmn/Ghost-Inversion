using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerMovement Player;
    [SerializeField] private Animator Animator;

    private bool _isChanged = false;
    private bool _isDead = false;

    private void Awake()
    {
        PlayerCollision.OnObstacleCollision += OnCollision;
    }

    private void OnDestroy()
    {
        PlayerCollision.OnObstacleCollision -= OnCollision;
    }

    private void OnCollision()
    {
       _isDead = true;

       if (_isDead)
       {
            Animator.CrossFade("White_Dead", 0, 0);
       }
    }

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
        if (Mathf.Abs(Player.InputMove) == 0f && !_isDead)
        {
            Animator.CrossFade("Black_Idle", 0, 0);
        }
        else if (Mathf.Abs(Player.InputMove) > 0f && !_isDead)
        {
            Animator.CrossFade("Black_Walk", 0, 0);
        }
    }

    private void WhiteAnimation()
    {
        if (Mathf.Abs(Player.InputMove) == 0f && !_isDead)
        {
            Animator.CrossFade("White_Idle", 0, 0);
        }
        else if (Mathf.Abs(Player.InputMove) > 0f && !_isDead)
        {
            Animator.CrossFade("White_Walk", 0, 0);
        }
    }
}
