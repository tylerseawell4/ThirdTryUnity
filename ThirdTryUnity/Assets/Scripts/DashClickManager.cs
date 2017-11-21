using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashClickManager : MonoBehaviour
{
    public RectTransform _UpDownButton;
    private PlayerControl _playerControl;
    public bool _isLeftClicked;
    public bool _isRightClicked;
    public bool _isUpDownClicked;
    private float _rotateAmount;

    private void Awake()
    {
        _rotateAmount = _UpDownButton.rotation.eulerAngles.z;
        _playerControl = FindObjectOfType<PlayerControl>();
    }
    private void Update()
    {
        if (_playerControl._player.velocity.y >= 0 && _rotateAmount != 90)
        {
            _rotateAmount += 5;
            _UpDownButton.rotation = Quaternion.Euler(_UpDownButton.rotation.x, _UpDownButton.rotation.y, _rotateAmount);
        }
        else if (_playerControl._player.velocity.y < 0 && _rotateAmount != -90)
        {
            _rotateAmount -= 5;
            _UpDownButton.rotation = Quaternion.Euler(_UpDownButton.rotation.x, _UpDownButton.rotation.y, _rotateAmount);
        }
    }
    public void LeftDashClicked()
    {
        _isLeftClicked = true;
    }
    public void RightDashClicked()
    {
        _isRightClicked = true;
    }
    public void UpDownDashClicked()
    {
        _isUpDownClicked = true;
    }
}
