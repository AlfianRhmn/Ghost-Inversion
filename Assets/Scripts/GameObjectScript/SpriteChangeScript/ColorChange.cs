using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _newColor, _defaultColor;
    private bool _isColorChanged = false;
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
        if (Input.GetKeyDown(KeyCode.E) && !_isColorChanged && !_isDead)
        {
            _isColorChanged = true;

            _renderer.color = _newColor;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isColorChanged && !_isDead)
        {
            _isColorChanged = false;

            _renderer.color = _defaultColor;
        }
    }
}
