using AIChara;
using HarmonyLib;

namespace KKABMX.Core
{
    public partial class KKABMX_Core
    {
        private static class Hooks
        {
            public static void Init()
            {
                Harmony.CreateAndPatchAll(typeof(Hooks), GUID);
            }

            [HarmonyPostfix]
            [HarmonyPatch(typeof(ChaControl), nameof(ChaControl.SetShapeBodyValue))]
            [HarmonyPatch(typeof(ChaControl), nameof(ChaControl.SetShapeFaceValue))]
            public static void ChangeValuePost(ChaControl __instance)
            {
                if (__instance != null)
                {
                    var controller = __instance.GetComponent<BoneController>();
                    if (controller != null)
                        controller.NeedsBaselineUpdate = true;
                }
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(ChaControl), nameof(ChaControl.ChangeHeadAsync), new System.Type[] { typeof(int), typeof(bool), typeof(bool) })]
            public static void ChangeHeadPre(ChaControl __instance)
            {
                if (__instance)
                {
                    var controller = __instance.GetComponent<BoneController>();
                    if (controller)
                    {
                        controller.HeadReset();
                    }
                }
            }
        }
    }
}
