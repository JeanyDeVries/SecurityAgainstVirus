using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    private float rotationSpeed, movementSpeed;

    [SerializeField]
    private float maxHealth, startMoney, jumpHeight;

    public static PlayerProps playerProps;
    public HealthBar healthBar;

    private Rigidbody rb;
    private bool onGround;
    private Vector3 velocity;
    private float yaw, pitch;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerProps = new PlayerProps();

        playerProps.health = maxHealth;
        playerProps.money = startMoney;

        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        Move();
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

    private void Move()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        playerInput *= Time.deltaTime;

        transform.Rotate(0, playerInput.x * rotationSpeed, 0);
        transform.Translate(0, 0, playerInput.y * movementSpeed);


       // yaw += rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
       // pitch -= rotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

       // transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
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
