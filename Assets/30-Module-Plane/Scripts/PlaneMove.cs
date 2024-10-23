using System;
using System.Collections;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public static Action onBackScene;

    [SerializeField] float _accelerationSpeed = 30f;
    [SerializeField] float _decelerationSpeed = 20f;
    [SerializeField] float _maxSpeed = 100f;
    [SerializeField] float _minSpeed = 30f;
    [SerializeField] float _rollSpeed = 50f;
    [SerializeField] float _pitchSpeed = 50f;
    [SerializeField] float _yawSpeed = 30f;
    [SerializeField] float _rotationSmoothFactor = 2f;
    [SerializeField] float _maximumHeight = 1000f;

    [SerializeField] bool _keyboardActived = true;
    [SerializeField] bool _mouseActived = true;

    [SerializeField] float _speedTest;

    Vector3 _positionCollisionLimit;
    float _currentSpeed = 30f;
    bool _noMove;

    void Update ()
    {
        Acceleration(UserInput.PlaneAcceleration);

        MaximumHeightControlRoll();

        if (_noMove) return;
        var targetRotation = new Quaternion();

        if (_keyboardActived)
        {
            targetRotation = transform.rotation *
                Quaternion.Euler(MovePitch(UserInput.PlanePitch), MoveYaw(UserInput.PlaneYaw), MoveRoll(UserInput.PlaneRoll));
        }

        if (_mouseActived)
        {
            targetRotation = transform.rotation *
                Quaternion.Euler(MovePitch(UserInput.PlanePitchMouse), MoveYaw(UserInput.PlaneYaw), MoveRoll(UserInput.PlaneRollMouse));
        }

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            _rotationSmoothFactor * Time.deltaTime);
    }

    public void BackScene ()
    {
        if (_noMove) return;

        StartCoroutine(BackSceneCoroutine());
    }

    float MovePitch (float value)
    {
        var pitch = 0f;

        if (transform.position.y > _maximumHeight)
        {
            if (value > 0)
            {
                pitch = (_pitchSpeed / 2) * Time.deltaTime;
            }
            else
            {
                MaximumHeightControlPitch();
            }
        }
        else
        {
            if (value > 0)
            {
                pitch = (_pitchSpeed / 2) * Time.deltaTime;
            }
            else if (value < 0)
            {
                pitch = -_pitchSpeed * Time.deltaTime;
            }
        }

        return pitch;
    }

    float MoveYaw (float value)
    {
        var yaw = 0f;

        if (value > 0)
        {
            yaw = -_yawSpeed * Time.deltaTime;
        }
        else if (value < 0)
        {
            yaw = _yawSpeed * Time.deltaTime;
        }

        return yaw;
    }

    float MoveRoll (float value)
    {
        var roll = 0f;

        if (transform.position.y < _maximumHeight)
        {
            if (value > 0)
            {
                roll = _rollSpeed * Time.deltaTime;
            }
            else if (value < 0)
            {
                roll = -_rollSpeed * Time.deltaTime;
            }
        }

        return roll;
    }

    void Acceleration (float value)
    {
        if (value > 0)
        {
            _currentSpeed += _accelerationSpeed * Time.deltaTime;
        }
        else if (value < 0)
        {
            _currentSpeed -= _accelerationSpeed * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, _minSpeed, _maxSpeed);
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    }

    void LookAtCenter ()
    {
        var directionToTarget = Vector3.zero - transform.position;
        directionToTarget.y = 0;

        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(directionToTarget);
        }

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void MaximumHeightControlRoll ()
    {
        if (transform.position.y > _maximumHeight)
        {
            var currentRotationZ = transform.eulerAngles.z;
            var desiredRotationZ = Mathf.LerpAngle(currentRotationZ, 0, Time.deltaTime * _speedTest);

            var eulerAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRotationZ);
            transform.rotation = Quaternion.Euler(eulerAngle);
        }
    }

    void MaximumHeightControlPitch ()
    {
        var currentRotationX = transform.eulerAngles.x;
        var desiredRotationX = Mathf.LerpAngle(currentRotationX, 0, Time.deltaTime * _speedTest);

        var eulerAngle = new Vector3(desiredRotationX, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngle);
    }

    IEnumerator BackSceneCoroutine ()
    {
        _noMove = true;
        onBackScene?.Invoke();
        _positionCollisionLimit = transform.position;
        yield return new WaitForSeconds(2.5f);
        transform.position = _positionCollisionLimit;
        LookAtCenter();
        yield return new WaitForSeconds(1);
        _noMove = false;
    }
}