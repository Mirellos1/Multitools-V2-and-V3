using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace MultitoolsMod.Configuration
{
    [Menu("Multitools", "Builder V3 Config")]
    public class BaseBuilderToolV3Config : ConfigFile
    {
        [Slider("Max Distance", 0, 100)]
        public float MaxDistance = 10f;

        [Toggle("Enable Precision Placement")]
        public bool EnablePrecisionPlacement = false;

        [Toggle("Enable Instant Build")]
        public bool EnableInstantBuild = false;

        [Toggle("Enable Fast Build")]
        public bool EnableFastBuild = false;

        [Toggle("Enable Vertical Snap")]
        public bool EnableVerticalSnap = false;

        [Toggle("Enable Horizontal Snap")]
        public bool EnableHorizontalSnap = false;

        [Slider("Horizontal Snap Degrees", 0, 360)]
        public float HorizontalSnapDegrees = 45f;

        [Toggle("Enable Random Build Angle")]
        public bool EnableRandomBuildAngle = false;

        [Slider("Max Random Angle", 0, 360)]
        public float MaxRandomAngle = 10f;

        [Toggle("Enable Quick Switch")]
        public bool EnableQuickSwitch = false;

        [Toggle("Enable Rotate Button")]
        public bool EnableRotateButton = false;

        [Toggle("Enable Demolish Button")]
        public bool EnableDemolishButton = false;
    }
}
