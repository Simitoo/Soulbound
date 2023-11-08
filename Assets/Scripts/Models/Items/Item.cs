using UnityEngine;

namespace Assets.Scripts.Models.Items
{
    [CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New item";
        public Sprite icon = null;
        
        public virtual void Use()
        {
            Debug.Log("Using " + name);
        } 
        
        public virtual void Remove()
        {
            Debug.Log("Remove " + name);
        }
    }
}
