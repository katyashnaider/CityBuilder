using System.Collections;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    //[SerializeField] private float _speed = 5f;
    //[SerializeField] private float _movementDuration = 3f;
    //[SerializeField] private float _pauseDuration = 2f;

    //private Rigidbody _rigidbody;
    //private float _nextMovementTime;

    //private void Start()
    //{
    //    _rigidbody = GetComponent<Rigidbody>();
    //    _nextMovementTime = Time.time;
    //}

    //private void FixedUpdate()
    //{
    //    if (Time.time >= _nextMovementTime + _movementDuration)
    //    {
    //        Vector3 position = _rigidbody.position;
    //        _rigidbody.position += Vector3.right * _speed * Time.fixedDeltaTime;

    //        if (Time.time < _nextMovementTime + _movementDuration)
    //        {
    //            _rigidbody.MovePosition(position);
    //        }
    //        else
    //        {
    //            _nextMovementTime += _movementDuration + _pauseDuration;
    //        }
    //    }
    //}

    //private void Start()
    //{
    //    _rigidbody = GetComponent<Rigidbody>();
    //    StartCoroutine(MoveConveyor());
    //}

    //private IEnumerator MoveConveyor()
    //{
    //    while (true)
    //    {
    //        float endTime = Time.time + _movementDuration;
    //        while (Time.time < endTime)
    //        {
    //            Vector3 position = _rigidbody.position;
    //            _rigidbody.position += Vector3.right * _speed * Time.fixedDeltaTime;
    //            yield return new WaitForFixedUpdate();
    //        }

    //        yield return new WaitForSeconds(_pauseDuration);
    //    }
    //}

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _movementDuration = 3f;
    [SerializeField] private float _pauseDuration = 2f;

    private void Start()
    {
        StartCoroutine(MoveConveyor());
    }

    private IEnumerator MoveConveyor()
    {
        while (true)
        {
            float endTime = Time.time + _movementDuration;
            while (Time.time < endTime)
            {
                MoveItemsOnBelt();
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(_pauseDuration);
        }
    }

    private void MoveItemsOnBelt()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Block"))
            {
                col.transform.position += -transform.right * _speed * Time.fixedDeltaTime;
            }
        }
    }
}