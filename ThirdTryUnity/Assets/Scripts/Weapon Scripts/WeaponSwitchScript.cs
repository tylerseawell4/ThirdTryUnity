using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitchScript : MonoBehaviour
{
    public Text _switchBtnText;
    private KetchupShoot _ketchupShoot;
    private KetchupShotgun _ketchupShotgun;
    private MustardShoot _mustardShoot;
    private MustardShotgun _mustardShotgun;
    private IceShoot _iceShoot;
    private IceShotgun _iceShotgun;
    private TapManager _tapManager;
    private void Start()
    {
        _tapManager = FindObjectOfType<TapManager>();
        _ketchupShoot = FindObjectOfType<KetchupShoot>();
        _ketchupShotgun = FindObjectOfType<KetchupShotgun>();
        _mustardShoot = FindObjectOfType<MustardShoot>();
        _mustardShotgun = FindObjectOfType<MustardShotgun>();
        _iceShoot = FindObjectOfType<IceShoot>();
        _iceShotgun = FindObjectOfType<IceShotgun>();

        if (_ketchupShotgun.enabled && _ketchupShotgun.enabled)
            _switchBtnText.text = "Ketchup";
        else if (_mustardShoot.enabled && _mustardShotgun.enabled)
            _switchBtnText.text = "Mustard";
        else if (_iceShoot.enabled && _iceShotgun.enabled)
            _switchBtnText.text = "Ice";
    }

    public void SwitchWeapon()
    {
        _tapManager._singleTap = false;
        _tapManager._doubleTap = false;
        if (_ketchupShotgun.enabled && _ketchupShotgun.enabled)
        {
            _switchBtnText.text = "Mustard";
            _ketchupShoot.enabled = false;
            _ketchupShotgun.enabled = false;
            _mustardShoot.enabled = true;
            _mustardShotgun.enabled = true;
        }
        else if (_mustardShoot.enabled && _mustardShotgun.enabled)
        {
            _switchBtnText.text = "Ice";
            _mustardShoot.enabled = false;
            _mustardShotgun.enabled = false;
            _iceShoot.enabled = true;
            _iceShotgun.enabled = true;
        }
        else if (_iceShoot.enabled && _iceShotgun.enabled)
        {
            _switchBtnText.text = "Ketchup";
            _iceShotgun.enabled = false;
            _iceShoot.enabled = false;
            _ketchupShoot.enabled = true;
            _ketchupShotgun.enabled = true;
        }
    }
}
