using UnityEngine;

public class PlaneMove1 : MonoBehaviour
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
    float _targetPitch = 0f;
    float _targetYaw = 0f;
    float _targetRoll = 0f;

    void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _currentSpeed += _accelerationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _currentSpeed -= _decelerationSpeed * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, _minSpeed, _maxSpeed);
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;

        var pitchInput = -Input.GetAxis("Mouse Y");
        _targetPitch += pitchInput * _pitchSpeed * Time.deltaTime;

        var rollInput = Input.GetAxis("Mouse X");
        if (Input.GetKey(KeyCode.Q) || rollInput < -0.1f)
        {
            _targetYaw -= _yawSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E) || rollInput > 0.1f)
        {
            _targetYaw += _yawSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _targetRoll += _rollSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _targetRoll -= _rollSpeed * Time.deltaTime;
        }

        var smoothPitch = Mathf.LerpAngle(transform.eulerAngles.x, _targetPitch, _rotationSmoothFactor * Time.deltaTime);
        var smoothYaw = Mathf.LerpAngle(transform.eulerAngles.y, _targetYaw, _rotationSmoothFactor * Time.deltaTime);
        var smoothRoll = Mathf.LerpAngle(transform.eulerAngles.z, _targetRoll, _rotationSmoothFactor * Time.deltaTime);

        transform.rotation = Quaternion.Euler(smoothPitch, smoothYaw, smoothRoll);
    }
}