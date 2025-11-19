using UnityEngine;

public class SimpleCam : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Vector3 _offset = new Vector3(0, 6, -6);

    void LateUpdate()
    {
        transform.position = _player.position + _offset;
    }
}