using BepInEx.Configuration;
using UnityEngine;

namespace MultitoolsMod
{
    public static class ModConfig
    {
        public static ConfigEntry<bool> V2Enabled;
        public static ConfigEntry<bool> V3Enabled;
        public static ConfigEntry<bool> BuildInSurvival;
        public static ConfigEntry<bool> BuildInFreedom;
        public static ConfigEntry<bool> BuildInCreative;
        public static ConfigEntry<bool> BuildInHardcore;

        public static void Init(ConfigFile config)
        {
            V2Enabled = config.Bind("BaseBuilderToolV2", "Enabled", true, "Enable/disable the Base Builder Tool V2");
            V3Enabled = config.Bind("BaseBuilderToolV3", "Enabled", true, "Enable/disable the Base Builder Tool V3");
            BuildInSurvival = config.Bind("Builder", "BuildInSurvival", true, "Allow building in Survival mode");
            BuildInFreedom = config.Bind("Builder", "BuildInFreedom", true, "Allow building in Freedom mode");
            BuildInCreative = config.Bind("Builder", "BuildInCreative", true, "Allow building in Creative mode");
            BuildInHardcore = config.Bind("Builder", "BuildInHardcore", true, "Allow building in Hardcore mode");
        }
    }
}

