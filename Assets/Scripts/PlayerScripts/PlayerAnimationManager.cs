using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Update()
    {
        SetDirectionValue();
    }

    private void SetDirectionValue()
    {
        _animator.SetFloat("VerticalDirection", _playerMovement.verticalDistance);
        _animator.SetFloat("HorizontalDirection", _playerMovement.horizontalDistance);
    }
}
