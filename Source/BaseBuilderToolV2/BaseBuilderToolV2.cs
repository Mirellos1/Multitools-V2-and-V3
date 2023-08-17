    public static class BaseBuilderToolV2_Patch
    {
        [HarmonyPatch(typeof(HoverPreview))]
        [HarmonyPatch("OnHover")]
        public static class HoverPreview_OnHover_Patch
        {
            public static void Prefix(HoverPreview __instance)
            {
                if (__instance.name.Contains("HabitatBuilder") && Inventory.main != null)
                {
                    PDA pda = Inventory.main.GetComponentInParent<PDA>();
                    if (pda != null)
                    {
                        if (pda.isInUse && __instance.gameObject.GetComponentInParent<Constructable>() == null)
                        {
                            __instance.gameObject.AddComponent<Constructable>();
                            __instance.gameObject.AddComponent<BaseBuilderToolV3>();
                        }
                    }
                }
            }
        }
        
        public class BaseBuilderToolV2 : MonoBehaviour
        {
            public static bool isPlacing = false;
            public static bool inBaseMode = false;
            public static bool inDeconstructMode = false;

            private static bool readyToDeploy;
            private static bool haveRequiredMaterials;
            private static BaseBuilderV2.Mode buildMode;
            private static BaseBuilderV2.Buildable buildable;
            private static TechType[] allowedPlaceModes = new TechType[]
            {
                TechType.BaseFoundation, TechType.BaseFloor, TechType.BaseWall, TechType.BaseCeiling, TechType.BaseTube
            };
            
            private static List<TechType> deconstructableTechTypes = new List<TechType>
            {
                TechType.BaseFoundation, TechType.BaseFloor, TechType.BaseWall, TechType.BaseCeiling, TechType.BaseTube,
                TechType.Hatch, TechType.Moonpool, TechType.ScannerRoom, TechType.GlassDome, TechType.MultipurposeRoom,
                TechType.Reinforcement, TechType.ThermalPlant, TechType.PowerTransmitter, TechType.PowerGeneratorBioReactor,
                TechType.NuclearReactor, TechType.WaterFiltrationMachine, TechType.LargeRoom, TechType.ExteriorWallWindow,
                TechType.PipeSurface, TechType.PipeWallMounted, TechType.PipeLadder, TechType.PipeJunction,
                TechType.PipeHorizontal, TechType.PipeVertical, TechType.PipeValve, TechType.FiltrationMachine,
                TechType.Ladder, TechType.PrecursorDungeonWall, TechType.MarineSnow, TechType.Shelf, TechType.PropulsionCannon,
                TechType.CyclopsHullArmorModule, TechType.CyclopsDecoyTube, TechType.CyclopsFireSuppressionModule,
                TechType.CyclopsSonarModule, TechType.CyclopsSeamothRepairModule, TechType.CyclopsShieldModule,
                TechType.CyclopsThermalReactorModule, TechType.CyclopsPowerUpgradeModule, TechType.CyclopsBridgeUpgradeModule,
                TechType.CyclopsSpeedUpgradeModule, TechType.CyclopsSilentRunningUpgradeModule, TechType.CyclopsHullModule1,
                TechType.CyclopsHullModule2, TechType.CyclopsHullModule3, TechType.CyclopsEnergyEfficiencyModule,
                TechType.CyclopsSonarModule, TechType.CyclopsDecoyModule, TechType.CyclopsSeamothModule, TechType.ExosuitDrillArmModule,
                TechType.ExosuitPropulsionArmModule, TechType.ExosuitGrapplingArmModule, TechType.Exosuit
        private void CheckForValidTargets()
        {
            if (!isInRadius)
            {
                target = null;
                return;
            }

            // Filter out invalid targets and get closest one
            float closestDist = maxDistance;
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                Target t = targets[i];
                if (!IsValidTarget(t, out float dist))
                {
                    targets.RemoveAt(i);
                }
                else if (dist < closestDist)
                {
                    closestDist = dist;
                    target = t;
                }
            }

            if (target == null && targets.Count > 0)
            {
                target = targets[0];
            }
        }

        private bool IsValidTarget(Target t, out float dist)
        {
            if (!t)
            {
                dist = 0f;
                return false;
            }

            Vector3 tPos = t.transform.position;
            if (!t.gameObject.activeSelf || Vector3.Distance(transform.position, tPos) > maxDistance)
            {
                dist = 0f;
                return false;
            }

            dist = Vector3.Distance(transform.position, tPos);
            return true;
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxDistance);
        }
    }
}

