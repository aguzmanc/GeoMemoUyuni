using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float _speedRotation;
    [SerializeField] float _speedMove;
    [SerializeField] float _horizontalMove;
    [SerializeField] float _verticalMove;

    void Start ()
    {
        if (_speedRotation != 0)
        {
            StartCoroutine(RotateCoroutine());
        }

        if (_verticalMove != 0)
        {
            StartCoroutine(MoveVerticallyCoroutine());
        }

        if (_horizontalMove != 0)
        {
            StartCoroutine(MoveHorizontallyCoroutine());
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    IEnumerator RotateCoroutine ()
    {
        while (true)
        {
            transform.Rotate(0, _speedRotation * Time.deltaTime, 0);
            yield return null;
        }
    }

    IEnumerator MoveVerticallyCoroutine ()
    {
        var startPosition = transform.position;

        while (true)
        {
            yield return StartCoroutine(MoveToPositionVerticallyCoroutine(startPosition + Vector3.up * _verticalMove));

            yield return StartCoroutine(MoveToPositionVerticallyCoroutine(startPosition - Vector3.up * _verticalMove * 2));

            yield return StartCoroutine(MoveToPositionVerticallyCoroutine(startPosition));
        }
    }

    IEnumerator MoveToPositionVerticallyCoroutine (Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedMove * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }

    IEnumerator MoveHorizontallyCoroutine ()
    {
        var startPosition = transform.position;

        while (true)
        {
            yield return StartCoroutine(MoveToPositionHorizontallyCoroutine(startPosition + Vector3.right * _horizontalMove));

            yield return StartCoroutine(MoveToPositionHorizontallyCoroutine(startPosition - Vector3.right * _horizontalMove * 2));

            yield return StartCoroutine(MoveToPositionHorizontallyCoroutine(startPosition));
        }
    }

    IEnumerator MoveToPositionHorizontallyCoroutine (Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedMove * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }
}
