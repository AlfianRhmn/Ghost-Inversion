using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    [SerializeField] private Button _button;
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
        _button.GetComponent<Image>();
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

            _button.GetComponent<Image>().sprite = _newSprite;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isSpriteChanged && !_isDead)
        {
            _isSpriteChanged = false;

            _button.GetComponent<Image>().sprite = _defaultSprite;
        }
    }
}
