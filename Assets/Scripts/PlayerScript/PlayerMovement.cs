using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D PlayerRB;
    [SerializeField] private Collider2D _groundCheck;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private LayerMask _floorLayerMask;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private float _accel;
    [SerializeField][Range (0f, 1f)] private float _drag;

    [HideInInspector] public float InputMove { get; private set; }
    [HideInInspector] public bool InputJump { get; private set; }

    private  bool IsDead = false;

    private float _coyoteTime = 0.2f;
    private float _coyoteCounter;

    private float _jumpBuffer = 0.1f;
    private float _jumpCounter;

    private void Start()
    {
        IsDead = false;
        PlayerRB = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        InputMove = Input.GetAxisRaw("Horizontal");
        InputJump = Input.GetButtonDown("Jump");

        if (InputJump && !IsDead)
        {
            FindObjectOfType<AudioManager>().Play("Jump");
        }

        if (Input.GetKeyDown(KeyCode.E) && !IsDead)
        {
            FindObjectOfType<AudioManager>().Play("Change");
        }

        // Plays the footstep sound
        // i don't know how to do it without adding AudioSource (i'm stupid)
        if (Mathf.Abs(InputMove) != 0 && PlayerRB.velocity.y == 0 && !IsDead)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

        }
        else if (Mathf.Abs(InputMove) == 0)
        {
            _audioSource.Stop();
        }

        Jump();
       
    }

    private void FixedUpdate()
    {
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
        FindObjectOfType<AudioManager>().Play("Death");
        PlayerRB.velocity = Vector3.zero;
        IsDead = true;
    }
    #endregion

    #region Jump
    private void Jump() 
    {
        // Coyote time
        if (IsGrounded())
        {
            _coyoteCounter = _coyoteTime;
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;
        }

        // Jump buffering
        if (InputJump)
        {
            _jumpCounter = _jumpBuffer;
        }
        else
        {
            _jumpCounter -= Time.deltaTime;
        }

        // Actual jump code
        if (_jumpCounter > 0f && _coyoteCounter > 0f && !IsDead)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, _jump);

            _jumpCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && PlayerRB.velocity.y > 0)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, PlayerRB.velocity.y * 0.5f);

            _coyoteCounter = 0f;
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
        ApplyFriction();

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
