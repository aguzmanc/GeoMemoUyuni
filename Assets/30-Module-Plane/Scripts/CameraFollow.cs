using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    [SerializeField] Vector3 _offset = new Vector3(0, 5, -10);
    [SerializeField] float _followSpeed = 5f;
    [SerializeField] float _rotationSpeed = 3f;

    void Start ()
    {
        _target = FindObjectOfType<PlaneMove>().transform;
    }

    void LateUpdate ()
    {
        var desiredPosition = _target.position + _target.rotation * _offset;

        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        var desiredRotation = Quaternion.LookRotation(_target.position - transform.position);
        var smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);

        transform.rotation = smoothedRotation;
    }
}