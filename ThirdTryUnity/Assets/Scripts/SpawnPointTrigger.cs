using UnityEngine;

public class SpawnPointTrigger : MonoBehaviour
{
    private VelocityBounce2 _playerVelocityScript;
    public GameObject _bugToSpawn;
    public float _increaseSpawnTriggerHeight;
    public float _distanceFromPlayerAtStart;
    private float _originalYPos;

    private void Start()
    {
        _playerVelocityScript = FindObjectOfType<VelocityBounce2>();
        transform.position = new Vector3(0, _playerVelocityScript._player.transform.position.y + _distanceFromPlayerAtStart, transform.position.z);
        _originalYPos = transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!_playerVelocityScript._hitHeight &&  (_playerVelocityScript._startingHeight > transform.position.y + _increaseSpawnTriggerHeight))
            {
                var cameraTopYPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
                Instantiate(_bugToSpawn, new Vector3(0, cameraTopYPoint + 2, _bugToSpawn.transform.position.z), _bugToSpawn.transform.rotation);
                Instantiate(_bugToSpawn, new Vector3(0, cameraTopYPoint + 9, _bugToSpawn.transform.position.z), _bugToSpawn.transform.rotation);
                Instantiate(_bugToSpawn, new Vector3(0, cameraTopYPoint + 16, _bugToSpawn.transform.position.z), _bugToSpawn.transform.rotation);
                transform.position = new Vector3(transform.position.x, transform.position.y + _increaseSpawnTriggerHeight, transform.position.z);
            }
            else if (_playerVelocityScript._hitHeight && transform.position.y > (_originalYPos + _increaseSpawnTriggerHeight))
            {
                var cameraBottomYPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
                Instantiate(_bugToSpawn, new Vector3(0, cameraBottomYPoint - 2, _bugToSpawn.transform.position.z), _bugToSpawn.transform.rotation);
                Instantiate(_bugToSpawn, new Vector3(0, cameraBottomYPoint - 9, _bugToSpawn.transform.position.z), _bugToSpawn.transform.rotation);
                Instantiate(_bugToSpawn, new Vector3(0, cameraBottomYPoint - 16, _bugToSpawn.transform.position.z), _bugToSpawn.transform.rotation);
                transform.position = new Vector3(transform.position.x, transform.position.y - _increaseSpawnTriggerHeight, transform.position.z);
            }
        }
    }
}
