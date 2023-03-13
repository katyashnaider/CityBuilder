using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _movementDuration = 3f;
    [SerializeField] private float _pauseDuration = 2f;

    private Rigidbody _rigidbody;
    private float _nextMovementTime;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _nextMovementTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time >= _nextMovementTime)
        {
            Vector3 position = _rigidbody.position;
            _rigidbody.position += Vector3.right * _speed * Time.fixedDeltaTime;

            if (Time.time < _nextMovementTime + _movementDuration)
            {
                _rigidbody.MovePosition(position);
            }
            else
            {
                _nextMovementTime += _movementDuration + _pauseDuration;
            }
        }
    }
}