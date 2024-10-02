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
    [SerializeField] float _rotationSmoothFactor = 2f; // Factor de suavizado

    float _currentSpeed = 30f;
    float _targetPitch = 0f;
    float _targetYaw = 0f;
    float _targetRoll = 0f;

    void Update ()
    {
        // Control de velocidad
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

        // Control del tangaje (pitch) con el ratón
        var pitchInput = -Input.GetAxis("Mouse Y");
        _targetPitch += pitchInput * _pitchSpeed * Time.deltaTime;

        // Control de la guiñada (yaw) con el teclado (Q y E)
        var rollInput = Input.GetAxis("Mouse X");
        if (Input.GetKey(KeyCode.Q) || rollInput < -0.1f)
        {
            _targetYaw -= _yawSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E) || rollInput > 0.1f)
        {
            _targetYaw += _yawSpeed * Time.deltaTime;
        }

        // Control del alabeo (roll) con el teclado (A y D) o el ratón
        if (Input.GetKey(KeyCode.A))
        {
            _targetRoll += _rollSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _targetRoll -= _rollSpeed * Time.deltaTime;
        }

        // Suavizado de las rotaciones
        float smoothPitch = Mathf.LerpAngle(transform.eulerAngles.x, _targetPitch, _rotationSmoothFactor * Time.deltaTime);
        float smoothYaw = Mathf.LerpAngle(transform.eulerAngles.y, _targetYaw, _rotationSmoothFactor * Time.deltaTime);
        float smoothRoll = Mathf.LerpAngle(transform.eulerAngles.z, _targetRoll, _rotationSmoothFactor * Time.deltaTime);

        // Aplicar la rotación suavizada
        transform.rotation = Quaternion.Euler(smoothPitch, smoothYaw, smoothRoll);
    }
}