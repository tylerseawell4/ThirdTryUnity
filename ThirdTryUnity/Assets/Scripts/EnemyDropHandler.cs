using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDropHandler : MonoBehaviour
{
    public GameObject _superBar;
    private void Awake()
    {
        _superBar = GameObject.Find("SuperBar");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _superBar.GetComponent<Image>().fillAmount < 1f)
        {
            Destroy(gameObject);
        }
    }
}
