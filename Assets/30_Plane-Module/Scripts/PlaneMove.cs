using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    [SerializeField] float _accelerationSpeed = 30f;
    [SerializeField] float _decelerationSpeed = 20f;
    [SerializeField] float _maxSpeed = 100f;
    [SerializeField] float _minSpeed = 30f;
    [SerializeField] float _rollSpeed = 50f;
    [SerializeField] float _pitchSpeed = 50f;
    [SerializeField] float _yawSpeed = 30f;
    [SerializeField] float _rotationSmoothFactor = 2f;

    float _currentSpeed = 30f;

    void Update ()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            _currentSpeed += _accelerationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            _currentSpeed -= _decelerationSpeed * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, _minSpeed, _maxSpeed);
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;

        float pitch = 0f;
        float yaw = 0f;
        float roll = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            pitch = (_pitchSpeed / 2) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pitch = -_pitchSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            yaw = -_yawSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            yaw = _yawSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            roll = _rollSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            roll = -_rollSpeed * Time.deltaTime;
        }

        Quaternion targetRotation = transform.rotation *
            Quaternion.Euler(pitch, yaw, roll);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            _rotationSmoothFactor * Time.deltaTime);
    }
}
