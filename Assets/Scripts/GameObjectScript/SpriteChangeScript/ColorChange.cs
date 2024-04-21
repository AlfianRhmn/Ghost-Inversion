using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _newColor, _defaultColor;
    private bool _isColorChanged = false;

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
        if (Input.GetKeyDown(KeyCode.E) && !_isColorChanged)
        {
            _isColorChanged = true;

            _renderer.color = _newColor;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isColorChanged)
        {
            _isColorChanged = false;

            _renderer.color = _defaultColor;
        }
    }
}
