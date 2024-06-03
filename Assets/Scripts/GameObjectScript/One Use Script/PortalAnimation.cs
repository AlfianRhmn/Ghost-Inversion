using UnityEngine;

public class PortalAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private bool _isBlack;

    private bool _isChanged;

    void Start()
    {
        _animator = GetComponent<Animator>();

        HandleAnimations();
    }

    private void HandleAnimations()
    {
        if (_isBlack)
        {
            _animator.CrossFade("Portal_White", 0, 0);
        }
        if (!_isBlack)
        {
            _animator.CrossFade("Portal_Black", 0, 0);
        }
    }

}
