using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    [SerializeField] Vector3 _offset = new Vector3(0, 5, -10);
    [SerializeField] float _followSpeed = 5f;
    [SerializeField] float _rotationSpeed = 3f;

    void Update ()
    {
        // Movimiento suavizado
        var desiredPosition = _target.position + _offset;
        var smoothedPosition = Vector3.Lerp(transform.position, _target.position, _followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Rotación suavizada
        //var direction = _target.position - transform.position;
        //direction.y = 0;  // Ignorar la rotación en Z (alabeo), mantenemos la cámara nivelada
        //var desiredRotation = Quaternion.LookRotation(direction);
        var desiredRotation = Quaternion.Euler(_target.eulerAngles.x, _target.eulerAngles.y, 0);
        var smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);

        transform.rotation = smoothedRotation;
    }
}