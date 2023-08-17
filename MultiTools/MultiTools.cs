using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MultiTools
{
    public static class MultiTools
    {
        public static void Load()
        {
            // Tutaj można dodać kod do inicjalizacji pluginu
        }

        public static void Unload()
        {
            // Tutaj można dodać kod do wyłączenia pluginu
        }

        public static void Patch()
        {
            // Dodanie nowych urządzeń do fabrykatora
            TechTypeBuilder.AddTechType("BaseBuilderToolV2", "Base Builder V2", "A device used to build bases.", (int)TechGroup.Resources, (int)TechCategory.Tools);
            CraftDataHandler.SetTechData(TechType.BaseBuilderToolV2, new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.Titanium, 2),
                    new Ingredient(TechType.Lubricant, 1),
                    new Ingredient(TechType.Silicone, 1),
                }
            });

            TechTypeBuilder.AddTechType("BaseBuilderToolV3", "Base Builder V3", "An advanced device used to build bases.", (int)TechGroup.Resources, (int)TechCategory.Tools);
            CraftDataHandler.SetTechData(TechType.BaseBuilderToolV3, new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.Titanium, 2),
                    new Ingredient(TechType.WiringKit, 1),
                    new Ingredient(TechType.Silicone, 1),
                    new Ingredient(TechType.Diamond, 1),
                }
            });

            // Dodanie nowych metod do obsługi urządzeń
            CraftDataHandler.SetItemSize(TechType.BaseBuilderToolV2, new Vector2int(2, 2));
            CraftDataHandler.SetItemSize(TechType.BaseBuilderToolV3, new Vector2int(2, 2));
            CraftDataHandler.SetEquipmentType(TechType.BaseBuilderToolV2, EquipmentType.Hand);
            CraftDataHandler.SetEquipmentType(TechType.BaseBuilderToolV3, EquipmentType.Hand);

            // Dodanie metod do obsługi upuszczania materiałów
            MethodInfo dropItemAtMethodV2 = typeof(BaseBuilderV2).GetMethod("DropItemAt", BindingFlags.Instance | BindingFlags.NonPublic);
            CraftDataHandler.AddToGroup(TechGroup.Resources, TechCategory.Tools, TechType.BaseBuilderToolV2);
            CraftDataHandler.SetHarvestType(TechType.BaseBuilderToolV2, HarvestType.Normal);
            CraftDataHandler.SetInteractDistance(TechType.BaseBuilderToolV2, 5f);
            CraftDataHandler.SetEquipmentAction(TechType.BaseBuilderToolV2, new EquipmentAction(dropItemAtMethodV2, new LiveMixinData(0f), new UseFirstAidData(1f), EquipmentActions.Default));
            CraftDataHandler.AddToSubCategory(TechCategory.Tools, TechType.BaseBuilderToolV2);

            MethodInfo dropItemAtMethodV3 = typeof(BaseBuilderV3).GetMethod("DropItemAt", BindingFlags.Instance | BindingFlags.NonPublic);
            CraftDataHandler.AddToGroup(TechGroup.Resources, TechCategory.Tools, TechType.BaseBuilderToolV3);
            CraftDataHandler.SetHarvestType(TechType.BaseBuilderToolV3, HarvestType.Normal);
            CraftDataHandler.SetInteractDistance(TechType.BaseBuilderToolV3, 5f);
            CraftDataHandler.SetEquipmentAction(TechType.BaseBuilderTool
            CraftDataHandler.SetItemSize(TechType.BaseBuilderToolV2, new Vector2int(2, 2));
            CraftDataHandler.SetItemSize(TechType.BaseBuilderToolV3, new Vector2int(3, 3));

            CraftDataHandler.SetEquipmentType(TechType.BaseBuilderToolV2, EquipmentType.Hand);
            CraftDataHandler.SetEquipmentType(TechType.BaseBuilderToolV3, EquipmentType.Hand);

            CraftDataHandler.AddToGroup(TechGroup.Resources, TechCategory.Tools, TechType.BaseBuilderToolV2);
            CraftDataHandler.AddToGroup(TechGroup.Resources, TechCategory.Tools, TechType.BaseBuilderToolV3);

            var ab = Assembly.GetExecutingAssembly();
            var abName = ab.GetName().Name;
            var abVersion = ab.GetName().Version;
            var abDescription = ab.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

            Debug.Log($"{abName} v{abVersion} - {abDescription} - loaded.");
        }
    }

    public class BaseBuilderToolV2 : ToolAction
    {
        public override void OnToolUseAnim(GUIHand hand)
        {
            // Kod do obsługi urządzenia "Base Builder V2"
            Inventory.main.container.RemoveItem(TechType.Titanium, 2);
            Inventory.main.container.RemoveItem(TechType.Lubricant, 1);
            Inventory.main.container.RemoveItem(TechType.Silicone, 1);
            Debug.Log("Base Builder V2 used.");
        }
    }

    public class BaseBuilderToolV3 : ToolAction
    {
        public override void OnToolUseAnim(GUIHand hand)
        {
            // Kod do obsługi urządzenia "Base Builder V3"
            Inventory.main.container.RemoveItem(TechType.Titanium, 2);
            Inventory.main.container.RemoveItem(TechType.WiringKit, 1);
            Inventory.main.container.RemoveItem(TechType.Silicone, 1);
            Inventory.main.container.RemoveItem(TechType.Diamond, 1);
            Debug.Log("Base Builder V3 used.");
        }
    }
}