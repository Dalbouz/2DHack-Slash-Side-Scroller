using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private CharacterController2D _controller;
    private float _horizontalMovement = 0f;
    private float _verticalMovement = 0f;
    
    [SerializeField]
    private float _playerSpeed = 40f;
    [Header("Jumping")]
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _smallJumpForce=.5f;
    private bool _isJumping = false;

    [SerializeField]
    private Rigidbody2D _rb;
    private bool _facingRight = true;
    [SerializeField]
    private float _slowDownOnAttack;
    [SerializeField]
    private SpriteRenderer _playerSR;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerSR = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        // Get the Input
        _horizontalMovement = Input.GetAxis("Horizontal");
        MovementLeftOrRight();

        if (Input.GetButtonDown("Jump"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }

        if(Input.GetButtonUp("Jump") && _rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _smallJumpForce);
        }
        
            

    }
    private void FixedUpdate()
    {
        //Move Player
        //_controller.Move(_horizontalMovement,false, _isJumping);
        //_isJumping = false; 
    }

    private void flip()
    {
        _facingRight = !_facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void MovementLeftOrRight()
    {
        if (_horizontalMovement != 0 && !AttackManager.instance.IsAttacking)
        {
            transform.position += new Vector3(_horizontalMovement, 0, 0) * Time.deltaTime * _playerSpeed;
            if (_horizontalMovement < 0 && _facingRight)
            {
                flip();
            }
            else if (_horizontalMovement > 0 && !_facingRight)
            {
                flip();
            }
            //if (_horizontalMovement > 0)
            //{
            //    _playerSR.flipX = false;
            //}
            //if (_horizontalMovement < 0)
            //{
            //    _playerSR.flipX = true;
            //}
                _animator.SetBool("IsWalking", true);

        }
        else if(_horizontalMovement !=0 && AttackManager.instance.IsAttacking)
        {
            _animator.SetBool("IsWalking", false);
            transform.position += new Vector3(_horizontalMovement, 0, 0) * Time.deltaTime * _playerSpeed*_slowDownOnAttack;
            if (_horizontalMovement < 0 && _facingRight)
            {
                flip();
            }
            else if (_horizontalMovement > 0 && !_facingRight)
            {
                flip();
            }
        }
        else
            _animator.SetBool("IsWalking", false);
    }
    //private void Jump()
    //{
    //    _isJumping = true;
    //    _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    //    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    //}
}
