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
        /*
         * If the mouse is clicked, then the animator will set the correct bool 
         * to to true. The animator will then play the correct state. 
         * When the state is finished plating it will set the bool to false 
         * again so it will not play anything anymore (idle state).
         */


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
