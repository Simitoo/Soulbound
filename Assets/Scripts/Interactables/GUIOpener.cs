using Assets.Scripts.Constants;
using Assets.Scripts.Repository.UI;
using UnityEngine;

namespace Assets.Scripts.Models.Interactables
{
    public class GUIOpener : Interactable
    {
        private Inventory inventory;
        private PlayerManager playerManager;
        private PlayerStats playerStats;
        private PopUp popUpMessage;

        [SerializeField]
        private GameObject panel;

        private void Start()
        {
            playerManager = PlayerManager.instance;
            playerStats = playerManager.player.GetComponent<PlayerStats>();
            inventory = Inventory.instance;
            popUpMessage = GetComponent<PopUp>();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if(playerStats.CurrHealth < playerStats.health.GetValue)
            {
                playerStats.FullRecover();
            }
        }

        public override void Interaction()
        {           
            if (inventory.items.Count == 0)
            {
                popUpMessage.Show(UserMessages.InventoryIsEmpty);

                return;
            }

            base.Interaction();

            panel.SetActive(true);
        }

        public override void CloseInteraction()
        {
            base.CloseInteraction();

            panel.SetActive(false);
        }
    }
}
