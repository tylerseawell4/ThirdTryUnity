using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupElectricOrb : MonoBehaviour
{
    public GameObject _lightningPrefab;
    private GameObject _superLightningAttack;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            SpawnLightning(collision.gameObject.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            SpawnLightning(collision.gameObject.transform);
        }
    }

    private void SpawnLightning(Transform enemy)
    {
        _superLightningAttack = Instantiate(_lightningPrefab, transform.position, Quaternion.identity);
        _superLightningAttack.transform.parent = transform;
        _superLightningAttack.GetComponent<LightningAttackTime>().SpawnLightning(enemy);
    }

}
