using UnityEngine;

public class BurstBug : MonoBehaviour
{
    public GameObject _bugToSpawn;
    public bool _shouldReplicate;
    public void SpawnBugs()
    {
        if (_shouldReplicate)
        {
            Instantiate(_bugToSpawn, new Vector3(transform.position.x, transform.position.y + .5f, _bugToSpawn.transform.position.z), Quaternion.identity).GetComponent<Collider2D>().enabled = true;
            Instantiate(_bugToSpawn, new Vector3(transform.position.x, transform.position.y - .5f, _bugToSpawn.transform.position.z), Quaternion.identity).GetComponent<Collider2D>().enabled = true;
        }
    }
}
