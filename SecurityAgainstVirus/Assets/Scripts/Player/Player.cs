using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField, Range(0f, 50f)]
    private float movementSpeed;

    [SerializeField]
    private float maxHealth, startMoney, jumpHeight;

    public static PlayerProps playerProps;
    public HealthBar healthBar;

    private Rigidbody rb;
    private bool onGround;
    private Vector3 velocity;

    public bool isInFirewall;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerProps = new PlayerProps();

        playerProps.health = maxHealth;
        playerProps.money = startMoney;
        Cursor.lockState = CursorLockMode.Locked;

        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        MoveForward();
    }

    private void FixedUpdate()
    {
        if (onGround && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        onGround = false;
    }

    private void Jump()
    {
        if (!onGround) return;
        velocity = rb.velocity;
        velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        rb.velocity = velocity;
    }

    private void MoveForward()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        playerInput *= Time.deltaTime;

        transform.Translate(0, 0, playerInput.y * movementSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }
}
