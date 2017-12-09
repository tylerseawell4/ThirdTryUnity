using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDropHandler : MonoBehaviour
{
    private GameObject _superBar;
    private SuperMode _superMode;
    private SuperIce _superIce;

    private void Awake()
    {
        var player = GameObject.Find("Player");
        if (player != null)
            _superMode = player.GetComponent<SuperMode>();
        _superIce = player.GetComponent<SuperIce>();
        _superBar = GameObject.Find("SuperBar");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _superBar.GetComponent<Image>().fillAmount < 1f && !_superMode._superActivated && !_superIce._superIceActivated)
        {
            var inWormColor = new Color(.25f, .25f, .25f, .6f);
            if (!_superMode._superActivated && !_superIce._superIceActivated && (collision.gameObject.GetComponent<SpriteRenderer>().color != inWormColor))
                Destroy(gameObject);
        }
    }
}
