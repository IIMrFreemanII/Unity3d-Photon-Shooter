using Photon.Pun;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInputController _inputController;
        private Rigidbody _rb;
        private CapsuleCollider _collider;
        private PhotonView _photonView;
        
        [SerializeField]
        private LayerMask groundLayer = 1; // 1 == "Default"
        
        [SerializeField]
        private float jumpForce = 5f;
        [SerializeField]
        private float movementSpeed = 40f;
        [SerializeField]
        private float rotationSpeed = 1f;
        [SerializeField][Range(0f, 1f)]
        private float groundCheckRadius = 1f;


        private bool IsGrounded
        {
            get
            {
                Vector3 bottomCenterPoint = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);

                //создаем невидимую физическую капсулу и проверяем не пересекает ли она обьект который относится к полу

                //_collider.bounds.size.x / 2 * 0.9f -- эта странная конструкция берет радиус обьекта.
                // был бы обязательно сферой -- брался бы радиус напрямую, а так пишем по-универсальнее

                return Physics.CheckCapsule(_collider.bounds.center, bottomCenterPoint, _collider.bounds.size.x / 2 * groundCheckRadius, groundLayer);
                // если можно будет прыгать в воздухе, то нужно будет изменить коэфициент 0.9 на меньший.
            }
        }

        public float MovementSpeed => movementSpeed;
        public float RotationSpeed => rotationSpeed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _inputController = GetComponent<PlayerInputController>();
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            
            if (groundLayer == gameObject.layer)
            {
                Debug.LogError("Player SortingLayer must be different from Ground Sorting Layer!");
            }
        }

        private void FixedUpdate()
        {
            if (_photonView.IsMine || GameManager.IsOfflineMode)
            {
                HandleMovement();
                HandleRotation();
                HandleJump();
            }
        }

        private void HandleMovement()
        {
            float horizontal = _inputController.Horizontal;
            float vertical = _inputController.Vertical;
            
            if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
            {
                Vector3 newPosition = new Vector3(horizontal, 0, vertical) * (movementSpeed * Time.fixedDeltaTime);
               transform.Translate(newPosition);
            }
        }

        private void HandleRotation()
        {
            float deltaMouseX = _inputController.DeltaMouseX;

            if (Mathf.Abs(deltaMouseX) > 0)
            {
                Vector3 newRotation = new Vector3(0, deltaMouseX, 0) * (RotationSpeed * Time.fixedDeltaTime);
                transform.Rotate(newRotation);
            }
        }

        private void HandleJump()
        {
            if (IsGrounded && _inputController.Jump > 0)
            {
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }
    }
}
