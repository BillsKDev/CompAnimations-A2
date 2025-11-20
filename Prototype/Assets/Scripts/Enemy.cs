using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _player;
    NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Move()
    {
        _agent.destination = _player.transform.position;
        if (_agent.remainingDistance <= _agent.stoppingDistance)
            Debug.Log("HI");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}