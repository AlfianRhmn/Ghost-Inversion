using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Collider2D _collider;

    [Header("Condition")]
    [SerializeField] private bool _isBlack = false;
    private bool _isChanged = false;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

        HandleInitialCondition();
    }

    private void HandleInitialCondition()
    {
        if (_isBlack)
        {
            _collider.enabled = true;
        }
        else if (!_isBlack)
        {
            _collider.enabled = false;
        }
    }

    private void Update()
    {
        HandleCondition();
    }

    private void HandleCondition()
    {
        if (_isBlack)
        {
            UpdateWhite();
        }
        else if (!_isBlack)
        {
            UpdateBlack();
        }
    }

    private void UpdateWhite()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isChanged)
        {
            _isChanged = true;

            _collider.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isChanged)
        {
            _isChanged = false;

            _collider.enabled = true;
        }
    }

    private void UpdateBlack()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isChanged)
        {
            _isChanged = true;

            _collider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isChanged)
        {
            _isChanged = false;

            _collider.enabled = false;
        }
    }
}
