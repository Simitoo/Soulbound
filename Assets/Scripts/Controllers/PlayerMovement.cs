using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Animator characterAnimator;
    public PlayerStats playerStats;

    private float horizontalMove = 0f;
    private float stunTimer = 0f;

    private bool jump = false;
    private bool isStunned;

    public float StunDuration { get; set; }

    void Update()
    {
        if (isStunned)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= StunDuration)
            {
                isStunned = false;
                stunTimer = 0f;
            }
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * playerStats.speed.GetValue;
            characterAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                characterAnimator.SetBool("IsJumping", true);

                jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void CastSkill()
    {
        isStunned = true;
        //future attack animation
    }

    public void ReciveHit()
    {
        isStunned = true;

        StunDuration = 0.1f;
        horizontalMove = 0f;
        characterAnimator.SetTrigger("Hit");
    }

    public void OnLanding()
    {
        characterAnimator.SetBool("IsJumping", false);
    }
}
