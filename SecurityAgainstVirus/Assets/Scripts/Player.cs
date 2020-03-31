using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    private float rotationSpeed, movementSpeed;

    private Rigidbody rb;
    public PlayerProps playerProps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerProps = new PlayerProps();
    }

    private void Update()
    {
        Move();
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
    }
}
