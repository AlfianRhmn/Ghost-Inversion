using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _newSprite, _defaultSprite;
    private bool _isSpriteChanged = false;
    
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
        if (Input.GetKeyDown(KeyCode.E) && !_isSpriteChanged)
        {
            _isSpriteChanged = true;

            _button.GetComponent<Image>().sprite = _newSprite;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isSpriteChanged)
        {
            _isSpriteChanged = false;

            _button.GetComponent<Image>().sprite = _defaultSprite;
        }
    }
}
