using System;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using SMLHelper.V2.Handlers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace BaseBuilderToolV2
{
    public static class BaseBuilderToolV2
    {
        public static void Patch()
        {
            try
            {
                CraftDataHandler.AddToTechFabricator(TechType.SeaMoth, TechType.Builder);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }

    public class BaseBuilderV2 : MonoBehaviour
    {
        private Inventory inventory;

        void Start()
        {
            inventory = GetComponent<Inventory>();
        }

        public void DropItemAt(int index, Vector3 position, Quaternion rotation, Vector3 velocity)
        {
            inventory.RemoveItem(index, true);
            CraftData.Ingredient ingredient = new CraftData.Ingredient() { techType = TechType.Titanium, amount = 1 };
            GameObject titanium = CraftData.InstantiateFromPrefab(ingredient.techType);
            titanium.transform.position = position;
            titanium.transform.rotation = rotation;
            Rigidbody rigidBody = titanium.GetComponent<Rigidbody>();
            rigidBody.velocity = velocity;
        }
    }
}
