using Assets.Scripts.Models.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Repository.UI
{
    public class SelectionSlot : MonoBehaviour
    {
        private Item item;

        public Image icon;

        public void AddItem(Item itemToAdd)
        {
            item = itemToAdd;
            icon.sprite = itemToAdd.icon;
            icon.enabled = true;
        }

        public void ClearSlot()
        {
            if(item != null)
            {
                item.Remove();

                item = null;
                icon.sprite = null;
                icon.enabled = false;
            }
                      
        }
    }
}
