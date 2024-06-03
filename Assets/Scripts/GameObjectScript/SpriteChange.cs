using UnityEngine;

public class SpriteChange : MonoBehaviour
{   
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _newSprite, _defaultSprite;
    private bool _isSpriteChanged = false;
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
    }

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        IfEPressed();
    }

    private void IfEPressed()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isSpriteChanged && !_isDead)
        {
            _isSpriteChanged = true;

            _renderer.sprite = _newSprite;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isSpriteChanged && !_isDead)
        {
            _isSpriteChanged = false;

            _renderer.sprite = _defaultSprite;
        }
    }
}
