using UnityEngine;

public class SpriteChange : MonoBehaviour
{   
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _newSprite, _defaultSprite;
    private bool _isSpriteChanged = false;

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
        if (Input.GetKeyDown(KeyCode.E) && !_isSpriteChanged)
        {
            _isSpriteChanged = true;

            _renderer.sprite = _newSprite;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isSpriteChanged)
        {
            _isSpriteChanged = false;

            _renderer.sprite = _defaultSprite;
        }
    }
}
