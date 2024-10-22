using UnityEngine;

public class PlaneCollider : MonoBehaviour
{
    PlaneMove _planeMove;

    private void Start ()
    {
        _planeMove = GetComponentInParent<PlaneMove>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Limit")
        {
            _planeMove.BackScene();
        }
    }
}
