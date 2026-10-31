using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInputs;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody2D rigidbody;

    private Vector2 moveDirection;
    private bool isJumping = false;

    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        moveAction = playerInputs.FindActionMap("Player").FindAction("Move");
        jumpAction = playerInputs.FindActionMap("Player").FindAction("Jump");
    }

    private void OnEnable()
    {
        playerInputs.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        playerInputs.FindActionMap("Player").Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame() && !isJumping)
        {
            Jump();
            isJumping = true;
        }

    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = new Vector2(moveSpeed * moveDirection.x, rigidbody.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
           if(contact.normal.y >= 0.8)
            {
                isJumping = false;
            }
        }
    }

    private void Jump()
    {
        rigidbody.AddForce(new Vector2(rigidbody.linearVelocity.x, jumpForce), ForceMode2D.Impulse);
    }
}
