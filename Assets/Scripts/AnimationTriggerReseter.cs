using UnityEngine;

public class AnimationTriggerReseter : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ResetTrigger(string animationName)
    {
        animator.SetTrigger(animationName);
    }
}
