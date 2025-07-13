using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JunkCity.World
{
    public class AccessoryManager : MonoBehaviour
    {
        public IEnumerable<IWeapon> Weapons
        {
            get => accessories?.Select(a =>
            {
                if (a.TryGetComponent(out IWeapon weapon))
                    return weapon;
                else
                    return null;
            }).Where(w => w != null) ?? Array.Empty<IWeapon>();
        }

        [SerializeField] private List<GameObject> accessories;


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
