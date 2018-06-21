using UnityEngine;
public class MoveBackGround : MonoBehaviour
{
    private Rigidbody2D _pointToMove;
    private float _leftCam;
    private float _rightCam;
    // Use this for initialization
    void Start()
    {
        _leftCam = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _rightCam = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        _pointToMove = GetComponent<Rigidbody2D>();
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void FixedUpdate()
    {
        if (Input.acceleration.x > .015f && _pointToMove.position.x < _rightCam + 10f)
            _pointToMove.velocity = new Vector3(20f * Input.acceleration.x, _pointToMove.velocity.y, 0f);
        else if (Input.acceleration.x < -.015f & _pointToMove.position.x > _leftCam + -10f)
            _pointToMove.velocity = new Vector3(20f * Input.acceleration.x, _pointToMove.velocity.y, 0f);
        else
            _pointToMove.velocity = new Vector3(0f, 0f, 0f);
    }
}
