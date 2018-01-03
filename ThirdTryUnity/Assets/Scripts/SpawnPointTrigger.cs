using UnityEngine;

public class SpawnPointTrigger : MonoBehaviour
{
    private VelocityBounce2 _playerVelocityScript;
    public float _increaseSpawnTriggerHeight;
    public float _distanceFromPlayerAtStart;
    private float _originalYPos;
    private EnemyBlockParent[] _enemyBlockArray;
    private EnemySpawnManager _enemySpawnManager;

    private void Start()
    {
        _enemySpawnManager = GetComponent<EnemySpawnManager>();
        _enemyBlockArray = new EnemyBlockParent[]{ new EnemyBlock1(), new EnemyBlock2()};
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
                var randomIndex = Random.Range(0, _enemyBlockArray.Length);
                var enemyBlock = _enemyBlockArray[randomIndex];
                _enemySpawnManager.Spawn(enemyBlock.SpawnEnemyBlock(cameraTopYPoint, false));
                enemyBlock.ClearDictionary();
                transform.position = new Vector3(transform.position.x, transform.position.y + _increaseSpawnTriggerHeight, transform.position.z);
            }
            else if (_playerVelocityScript._hitHeight && transform.position.y > (_originalYPos + _increaseSpawnTriggerHeight))
            {
                var cameraBottomYPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
                var randomIndex = Random.Range(0, _enemyBlockArray.Length);
                var enemyBlock = _enemyBlockArray[randomIndex];
                _enemySpawnManager.Spawn(enemyBlock.SpawnEnemyBlock(cameraBottomYPoint, true));
                enemyBlock.ClearDictionary();
                transform.position = new Vector3(transform.position.x, transform.position.y - _increaseSpawnTriggerHeight, transform.position.z);
            }
        }
    }
}
