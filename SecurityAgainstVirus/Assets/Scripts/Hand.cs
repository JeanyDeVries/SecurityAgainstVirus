using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    private Transform leftHand, rightHand;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("LeftIsPunching", true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("RightIsPunching", true);
        }
    }
}
