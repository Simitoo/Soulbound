using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Interactable : MonoBehaviour
    {
        public GameObject interactionText;        
        private bool isInRange;
        private bool isInteracting;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isInRange = true;
                if(interactionText != null )
                {
                    interactionText.SetActive(true);
                }
                
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isInRange = true;
                if (interactionText != null)
                {
                    interactionText.SetActive(true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isInRange = false;
                if (interactionText != null)
                {
                    interactionText.SetActive(false);
                }
                isInteracting = false;
                CloseInteraction();
            }
        }

        private void Update()
        {
            if (isInRange && Input.GetKeyDown(KeyCode.F))
            {
                Interaction();
                isInteracting = true;
            }

            if(isInteracting && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseInteraction();
            }
        }

        public virtual void Interaction()
        {
            Debug.Log("Interacting...");
        }

        public virtual void CloseInteraction()
        {
            Debug.Log("Interaction is closed!");
        }        
    }
}
