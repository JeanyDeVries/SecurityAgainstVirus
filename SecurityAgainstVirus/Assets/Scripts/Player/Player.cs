using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static PlayerProps playerProps;

    [Header("Properties that can be changed and balanced")]
    [SerializeField, Range(0f, 50f)] private float movementSpeed;
    [SerializeField] private float maxHealth, 
        startMoney, jumpHeight;

    [Header("Objects that needs to be dragged in the inspector")]
    public HealthBar healthBar;

    [Header("Properties that needs to be accessed by other scripts")]
    public bool isInFirewall;

    private Rigidbody rb;
    private bool onGround;
    private Vector3 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerProps = new PlayerProps();

        playerProps.health = maxHealth;
        playerProps.money = startMoney;
        playerProps.healthBar = healthBar;
        Cursor.lockState = CursorLockMode.Locked;

        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        MoveForward();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
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
            onGround = normal.y >= 0.8f;
        }
    }
}
