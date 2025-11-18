using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 10f;
    [SerializeField] float _moveSpeed = 5f;

    Rigidbody _rigidbody;
    Animator _animator;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
            vertical *= 2f;

        Vector3 velocity = new Vector3(horizontal, 0, vertical);
        
        if (velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);
            Vector3 movementDirection = velocity.normalized * (_moveSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(transform.position + movementDirection);
        }

        _animator.SetFloat("Speed", velocity.magnitude, 0.1f, Time.deltaTime);
    }
}