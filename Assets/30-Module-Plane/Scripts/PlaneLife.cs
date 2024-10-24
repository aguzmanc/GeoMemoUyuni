using UnityEngine;

public class PlaneLife : MonoBehaviour
{
    [SerializeField] GameObject _explosion;

    public void Dead ()
    {
        Instantiate(_explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
