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
    private TapManager _tapManager;
    private void Start()
    {
        _tapManager = FindObjectOfType<TapManager>();
        _ketchupShoot = FindObjectOfType<KetchupShoot>();
        _ketchupShotgun = FindObjectOfType<KetchupShotgun>();
        _mustardShoot = FindObjectOfType<MustardShoot>();
        _mustardShotgun = FindObjectOfType<MustardShotgun>();

        if (_ketchupShotgun.enabled && _ketchupShotgun.enabled)
            _switchBtnText.text = "Ketchup";
        else
            _switchBtnText.text = "Mustard";
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
        else
        {
            _switchBtnText.text = "Ketchup";
            _mustardShoot.enabled = false;
            _mustardShotgun.enabled = false;
            _ketchupShoot.enabled = true;
            _ketchupShotgun.enabled = true;

        }
    }
}
