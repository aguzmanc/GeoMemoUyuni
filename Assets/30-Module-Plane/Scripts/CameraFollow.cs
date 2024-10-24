using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    [SerializeField] Vector3 _offset = new Vector3(0, 5, -10);
    [SerializeField] float _followSpeed = 5f;
    [SerializeField] float _rotationSpeed = 3f;

    bool _noMove;

    private void OnEnable ()
    {
        PlaneMove.onBackScene += BackScene;
    }

    void Start ()
    {
        _target = FindObjectOfType<PlaneMove>().transform;
    }

    private void OnDisable ()
    {
        PlaneMove.onBackScene -= BackScene;
    }

    void LateUpdate ()
    {
        if (_noMove || _target == null) return;

        var desiredPosition = _target.position + _target.rotation * _offset;

        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        var desiredRotation = Quaternion.LookRotation(_target.position - transform.position);
        var smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);

        transform.rotation = smoothedRotation;
    }

    void BackScene ()
    {
        StartCoroutine(BackSceneCoroutine());
    }

    IEnumerator BackSceneCoroutine ()
    {
        _noMove = true;
        yield return new WaitForSeconds(2.5f);
        _noMove = false;
    }
}