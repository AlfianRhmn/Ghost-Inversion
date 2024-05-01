using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private Collider2D _groundCheck;
    [SerializeField] private LayerMask _floorLayerMask;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private float _accel;
    [SerializeField] private float _drag;

    [HideInInspector] public float InputMove { get; private set; }
    [HideInInspector] public bool InputJump { get; private set; }

    private float _coyoteTime = 0.2f;
    private float _coyoteCounter;

    private float _jumpBuffer = 0.2f;
    private float _jumpCounter;

    private float _fall = 4f;

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
        enabled = false;
        InputMove = 0;
        Rb.velocity = Vector3.zero;
    }
    #endregion

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
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
        if (_jumpCounter > 0f && _coyoteCounter > 0f)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, _jump);

            _jumpCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && Rb.velocity.y > 0)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * 0.5f);

            _coyoteCounter = 0f;
        }

        if (Rb.velocity.y < 0f)
        {
            Rb.velocity += Vector2.up * Physics2D.gravity.y * (_fall - 1f) * Time.deltaTime;
        }
    }
    #endregion

    #region Movement
    private void ApplyFriction()
    {
        if (IsGrounded() && InputMove == 0 && Rb.velocity.y == 0)
        {
            Rb.velocity *= _drag;
        }
    }

    private void Move()
    {
        if (Mathf.Abs(InputMove) > 0)
        { 
            float _Localincrament = InputMove * _accel;
            float _LocalnewSpeed = Mathf.Clamp(Rb.velocity.x + _Localincrament, -_speed, _speed);
            Rb.velocity = new Vector2(_LocalnewSpeed, Rb.velocity.y);

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
