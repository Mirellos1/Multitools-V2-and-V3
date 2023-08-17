using BepInEx;
using HarmonyLib;
using SMLHelper.V2.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using HarmonyLib.Tools;
using UnityEngine;
using 

namespace MultiTools
{
    [BepInPlugin("com.My.MultiTools", "MultiTools", "1.0.0")]
    [BepInProcess("Subnautica.exe")]
    public class MultiToolsPlugin : BaseUnityPlugin
    {
        private static TechType baseBuilderV2TechType;
        private static TechType baseBuilderV3TechType;

        private void Awake()
        {
            baseBuilderV2TechType = TechTypeHandler.AddTechType("BaseBuilderV2", "Advanced base building tool that can recover basic materials from wrecks", true);
            baseBuilderV3TechType = TechTypeHandler.AddTechType("BaseBuilderV3", "Ultimate base building tool that can randomly recover 1-3 materials", true);

            var harmony = new Harmony("com.My.MultiTools");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(BaseTool))]
        [HarmonyPatch("OnHitObject")]
        public class BaseTool_OnHitObject_Patch
        {
            public void Postfix(BaseTool __instance, GameObject target)
            {
                if (__instance.GetType() == typeof(BuilderTool))
                {
                    if (target.GetComponent<Pickupable>()?.GetTechType() == TechType.Wreck)
                    {
                        if (__instance.electronic)
                        {
                            Inventory.main.AddItem(AddItemsToInventory(baseBuilderV2TechType, 1));
                        }
                        else
                        {
                            ErrorMessage.AddMessage("This tool requires a battery.");
                        }
                    }
                    else
                    {
                        if (__instance.electronic)
                        {
                            Inventory.main.AddItem(AddItemsToInventory(baseBuilderV3TechType, UnityEngine.Random.Range(1, 4)));
                        }
                        else
                        {
                            Inventory.main.AddItem(AddItemsToInventory(TechType.Builder, 1));
                        }
                    }
                }
            }

            private static Pickupable.AdditionalPickingSettings AddItemsToInventory(TechType techType, int count)
            {
                return new Pickupable.AdditionalPickingSettings
                {
                    forPDA = true,
                    destroyOnPickup = true,
                    pickupSound = null,
                    playPickupSound = true,
                    slotFlags = InventorySlotType.General,
                    allowCopy = false,
                    amount = count,
                    techType = techType
                };
            }
        }

        [HarmonyPatch(typeof(Builder))]
        [HarmonyPatch("GetPrefabsForTechType")]
        public class Builder_GetPrefabsForTechType_Patch
        {
            public static void Postfix(TechType techType, ref int __result)
            {
                if (techType == baseBuilderV2TechType || techType == baseBuilderV3TechType)
                {
                    __result = 1;
                }
            }
        }

        [HarmonyPatch(typeof(Builder))]
        public class Builder_GetConstructable_Patch
        {
            public static void Postfix(TechType techType, ref Constructable __result)
            {
                if (techType == baseBuilderV2TechType || techType == baseBuilderV3TechType)
                {
                    __result = new Constructable(TechType.Builder, new string[] { "MultiTools", "Advanced Base Building" }, "Constructable/Deployables/BaseBuilder", true, null);
                }
            }
        }

        [HarmonyPatch(typeof(CrafterLogic))]
        public class CrafterLogic_GetFabricatorTab_Patch
        {
            public static void Postfix(CrafterLogic __instance, CraftData.DataType dataType, ref int __result)
            {
                if (dataType == CraftData.DataType.Tools && __instance.name == "Fabricator" && __instance.techType == TechType.Fabricator)
                {
                    __result = __instance.AddTab("Advanced Tools", baseBuilderV2TechType, baseBuilderV3TechType);
                }
            }
        }
    }
}
