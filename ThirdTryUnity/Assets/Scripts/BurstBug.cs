using UnityEngine;

public class BurstBug : MonoBehaviour
{
    public GameObject _bugToSpawn;

    public void SpawnBugs()
    {
        var localScale = _bugToSpawn.transform.localScale;
        localScale = new Vector2(localScale.x / 1.25f, localScale.y / 1.25f);
        _bugToSpawn.transform.localScale = localScale;
        //  Instantiate(_bugToSpawn, new Vector3(transform.position.x, transform.position.y, _bugToSpawn.transform.position.z), Quaternion.identity).GetComponent<Collider2D>().enabled = true;
        Instantiate(_bugToSpawn, new Vector3(transform.position.x, transform.position.y + .5f, _bugToSpawn.transform.position.z), Quaternion.identity).GetComponent<Collider2D>().enabled = true;
        Instantiate(_bugToSpawn, new Vector3(transform.position.x, transform.position.y - .5f, _bugToSpawn.transform.position.z), Quaternion.identity).GetComponent<Collider2D>().enabled = true;
    }
}
