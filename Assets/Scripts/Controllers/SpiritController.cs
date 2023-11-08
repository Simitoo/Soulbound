using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Controllers
{
    public class SpiritController : MonoBehaviour
    {
        private PlayerManager playerManager;
        private Animator spiritAnimator;

        private CharacterController characterController;

        private bool isWalking;

        private void Start()
        {
            playerManager = PlayerManager.instance;
            spiritAnimator = GetComponent<Animator>();

            characterController = playerManager.player.GetComponent<CharacterController>();

            characterController.OnLandEvent.AddListener(OnJumping);
        }

        private void Update()
        {          
            if (spiritAnimator != null)
            {
                isWalking = Input.GetButton("Horizontal");
                spiritAnimator.SetBool("IsWalking", isWalking);

                if (Input.GetButtonDown("Jump"))
                {
                    spiritAnimator.SetBool("Jump", true);
                }
            }
            else
            {
                return;
            }
        }

        private void OnJumping()
        {
            if (spiritAnimator == null)
            {
                return;
            }

            spiritAnimator.SetBool("Jump", false);
        }
    }
}
