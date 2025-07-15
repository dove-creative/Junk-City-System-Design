using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace JunkCity.World
{
    public class AccessoryManager : MonoBehaviour
    {
        public ReadOnlyCollection<GameObject> Accessories;
        [SerializeField] private List<GameObject> accessories;


        private void Awake()
        {
            Accessories = accessories.AsReadOnly();
        }

        public void Attach(GameObject accessory)
        {
            if (accessories.Contains(accessory))
                return;

            accessories.Add(accessory);
            accessory.transform.SetParent(transform, false);
        }
         
        public void Detach(GameObject accessory, bool destroy)
        {
            if (!accessories.Contains(accessory))
                return;

            accessories.Remove(accessory);

            if (destroy)
                Destroy(accessory);
            else
                accessory.transform.SetParent(null);
        }
    }
}
