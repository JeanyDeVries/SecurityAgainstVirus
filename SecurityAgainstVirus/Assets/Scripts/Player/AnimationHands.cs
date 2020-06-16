using UnityEngine;
public class AnimationHands : MonoBehaviour
{
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
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime
            >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            animator.SetBool("LeftIsPunching", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("RightIsPunching", true);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime 
            >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            animator.SetBool("RightIsPunching", false);
        }
    }

}
