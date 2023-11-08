using Assets.Scripts.Enum;
using Assets.Scripts.Models.Items;
using Assets.Scripts.Repository.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [CreateAssetMenu(fileName = "New Spirit", menuName = "Inventory/Spirit")]
    public class Spirit : Item
    {
        public Element element;

        public float attackModifier;
        public float healthModifier;
        public float energyRegeneration;
        public float defenceModifier;
        public float speedModifier;
        public float resistModifier;
        public float fireDamageModifier;
        public float waterDamageModifier;
        public float windDamageModifier;
        public float earthDamageModifier;

        public GameObject skillOne;
        public GameObject skillTwo;

        //prefab/ gameobject of skill1 and skill2

        public override void Use()
        {
            base.Use();

            SpiritManager.instance.Equip(this);
        }

        public override void Remove()
        {
            base.Remove();

            SpiritManager.instance.Unequip(this);
        }
    }
}
