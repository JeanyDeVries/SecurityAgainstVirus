using UnityEngine;

public class Hands : MonoBehaviour
{
    private Animator animator;
    public static bool isPunching;

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
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime
            >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            animator.SetBool("LeftIsPunching", false);
            isPunching = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("RightIsPunching", true);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime 
            >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            animator.SetBool("RightIsPunching", false);
            isPunching = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime
            <= animator.GetCurrentAnimatorStateInfo(0).length / 2)
        {
            isPunching = true;
        }
    }

}
