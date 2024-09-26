using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    [SerializeField] float _accelerationSpeed = 30f;
    [SerializeField] float _decelerationSpeed = 20f;
    [SerializeField] float _maxSpeed = 100f;
    [SerializeField] float _minSpeed = 0f;
    [SerializeField] float _rollSpeed = 50f;
    [SerializeField] float _pitchSpeed = 50f;
    [SerializeField] float _yawSpeed = 30f;

    float _currentSpeed = 0f;

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

        var pitch = -Input.GetAxis("Mouse Y") * _pitchSpeed * Time.deltaTime;
        //var yaw = Input.GetAxis("Mouse X") * _yawSpeed * Time.deltaTime;
        var yaw = 0f;
        transform.Rotate(pitch, yaw, 0);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * _rollSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * _rollSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(-Vector3.up * _yawSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * _yawSpeed * Time.deltaTime);
        }
    }
}