using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDropHandler : MonoBehaviour
{
    private GameObject _superBar;
    private SuperMode _superMode;
    private void Awake()
    {
        var player = GameObject.Find("Player");
        if (player != null)
            _superMode = player.GetComponent<SuperMode>();
        _superBar = GameObject.Find("SuperBar");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _superBar.GetComponent<Image>().fillAmount < 1f && !_superMode._superActivated)
        {
            Destroy(gameObject);
        }
    }
}
