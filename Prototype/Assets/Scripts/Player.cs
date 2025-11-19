using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 10f;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Cap _cap;
    [SerializeField] float _groundCheckDistance = 0.1f;

    Rigidbody _rigidbody;
    Animator _animator;
    public bool _isGrounded;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        CheckGrounded();
        
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

    void Update()
    {
        if (_isGrounded)
            _animator.SetBool("Jumping", false);
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _animator.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Throw");
            _cap.Show();
        }
    }

    void CheckGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance + 0.2f, _groundLayer);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
            _animator.SetBool("Water", true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
            _animator.SetBool("Water", false);
    }
}