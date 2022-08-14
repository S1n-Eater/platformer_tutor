using UnityEngine;
using Scripts.Components;


namespace Scripts
{


    public class Hero : MonoBehaviour
    {
        private Vector2 _direction;

        [SerializeField] private float _speed;
        [SerializeField] private float _jumSpeed;
        [SerializeField] private float _damageJumSpeed;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private Vector3 _groundCheckPositionDelta;

        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;

        private Collider2D[] _interactionResult = new Collider2D[1];

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private SpriteRenderer _sprite;
        private bool _isGrounded;
        private bool _allowDoubleJump;

        private static readonly int IsGroundKey = Animator.StringToHash("is_ground");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical_velocity");
        private static readonly int IsRunningKey = Animator.StringToHash("is_running");
        private static readonly int Hit = Animator.StringToHash("hit");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;

        }

        public void Update()
        {
            _isGrounded = IsGrounded();
        }

        private void FixedUpdate()
        {
            //_rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

            //var isJumping = _direction.y > 0;
            //if (isJumping && IsGrounded())
            //{
            //    _rigidbody.AddForce(Vector2.up * _jumSpeed, ForceMode2D.Impulse);
            //}


            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetBool(IsRunningKey, _direction.x != 0);
            _animator.SetFloat(VerticalVelocityKey, _rigidbody.velocity.y);
            _animator.SetBool(IsGroundKey, _isGrounded);

            UpdateSpriteDirection();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded) _allowDoubleJump = true;

            if (isJumpPressing)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigidbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling =_rigidbody.velocity.y <= 0.001f;

            if (!isFalling) return yVelocity;

            if (_isGrounded)
            {
                yVelocity += _jumSpeed;
            } 
            else if (_allowDoubleJump)
            {  
                yVelocity = _jumSpeed;
                _allowDoubleJump = false;
            }
            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                _sprite.flipX = false;
            }
            else if (_direction.x < 0)
            {
                _sprite.flipX = true;
            }
        }


        private bool IsGrounded()
        {
            var hit = Physics2D.CircleCast(
                transform.position + _groundCheckPositionDelta,
                _groundCheckRadius,
                Vector2.down, 
                0,
                _groundLayer);
            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
        }

        public void SaySomething()
        {
            Debug.Log("Hello Pigs!");
        }

        public void OnApplicationQuit()
        {
            PlayerPrefs.DeleteAll();
        }

        public void TakeDamage()
        {
            _animator.SetTrigger(Hit);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumSpeed);
        }

        
       public void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _interactionRadius,
                _interactionResult,
                _interactionLayer);

            for(int i = 0; i < size; i++)
            {
                var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
                if(interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}