using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroyWeapon : MonoBehaviour {
    [Tooltip("How fast the weapon will shoot from player")]
    public int _weaponMoveSpeed;

    [Tooltip("How long until the weapon shot will be destroyed from the game view")]
    public float _destroyWeaponTime;

	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * _weaponMoveSpeed);
        Destroy(gameObject, _destroyWeaponTime);
        //todo: need to increase the weapon move speed based on the players speed/number of bounces
        //so the player doesn't move ahead of the weapon being shot from up top.
	}
}
