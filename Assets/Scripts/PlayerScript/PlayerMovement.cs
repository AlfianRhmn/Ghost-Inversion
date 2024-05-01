using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D PlayerRB;
    [SerializeField] private Collider2D _groundCheck;
    [SerializeField] private LayerMask _floorLayerMask;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private float _accel;
    [SerializeField] private float _drag;

    [HideInInspector] public float InputMove { get; private set; }
    [HideInInspector] public bool InputJump { get; private set; }
    [HideInInspector] public bool IsDead { get; private set; }

    private float _coyoteTime = 0.2f;
    private float _coyoteCounter;

    private float _jumpBuffer = 0.2f;
    private float _jumpCounter;

    private float _fall = 4f;

    private void Start()
    {
        IsDead = false;
        PlayerRB = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<Collider2D>();
    }

    private void Update()
    {
        InputMove = Input.GetAxisRaw("Horizontal");
        InputJump = Input.GetButtonDown("Jump");

        CoyoteTime();
        JumpBufferTime();

        Jump();
    }

    private void FixedUpdate()
    {
        ApplyFriction();

        Move();
    }

    #region Events
    private void Awake()
    {
        PlayerCollision.OnObstacleCollision += OnCollisionDisableMovement;
    }

    private void OnDestroy()
    {
        PlayerCollision.OnObstacleCollision -= OnCollisionDisableMovement;
    }

    private void OnCollisionDisableMovement()
    {
        PlayerRB.velocity = Vector3.zero;
        IsDead = true;
    }
    #endregion

    #region Calculate jump timer
    private void CoyoteTime()
    {
        if (IsGrounded())
        {
            _coyoteCounter = _coyoteTime;
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;
        }
    }

    private void JumpBufferTime()
    {
        if (InputJump)
        {
            _jumpCounter = _jumpBuffer;
        }
        else
        {
            _jumpCounter -= Time.deltaTime;
        }
    }
    #endregion

    #region Jump
    private void Jump() 
    {
        if (_jumpCounter > 0f && _coyoteCounter > 0f && !IsDead)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, _jump);

            _jumpCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && PlayerRB.velocity.y > 0 && !IsDead)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, PlayerRB.velocity.y * 0.5f);

            _coyoteCounter = 0f;
        }

        if (PlayerRB.velocity.y < 0f && !IsDead)
        {
            PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (_fall - 1f) * Time.deltaTime;
        }
    }
    #endregion

    #region Movement
    private void ApplyFriction()
    {
        if (IsGrounded() && InputMove == 0 && PlayerRB.velocity.y == 0 && !IsDead)
        {
            PlayerRB.velocity *= _drag;
        }
    }

    private void Move()
    {
        if (Mathf.Abs(InputMove) > 0 && !IsDead)
        { 
            float _Localincrament = InputMove * _accel;
            float _LocalnewSpeed = Mathf.Clamp(PlayerRB.velocity.x + _Localincrament, -_speed, _speed);
            PlayerRB.velocity = new Vector2(_LocalnewSpeed, PlayerRB.velocity.y);

            float _LocalDirection = Mathf.Sign(InputMove);
            transform.localScale = new Vector3(_LocalDirection, 1, 1);
        }
    }
    #endregion

    #region Grounded Check
    private bool IsGrounded()
    {
        float _distance = 1f;
        RaycastHit2D rayHit = Physics2D.BoxCast(_groundCheck.bounds.center, _groundCheck.bounds.size, 0f, Vector2.down, _distance, _floorLayerMask);
        return rayHit.collider != null;
    }
    #endregion
}
