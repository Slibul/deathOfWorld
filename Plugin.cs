using BepInEx;
using BepInEx.Configuration;
using DeadlyOfWorld;
using HarmonyLib;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static ConfigEntry<int> Step;
    public static ConfigEntry<int> isSetP;
    public static ConfigEntry<bool> isNoDarker;
    public static ConfigEntry<bool> isParamet;
    private void Awake()
    {
        Step = Config.Bind<int>("General", "난이도", 1, "난이도 : 1\t1.전원검은적\n난이도 : 11\t11.일반적으로 많이나옵니다.\n난이도 : 12\t12.레어하게 많이나옵니다.\n난이도 : 13\t13.유니크하게 나옵니다.\n난이도 : 14\t14.레전드확율로 나옵니다.");
        Step = Config.Bind<int>("General", "확율", 0, "0~100까지의확율로 검은적이 나오게 설정할수있습니다.");
        isNoDarker = Config.Bind<bool>("General", "검은적 정상화", false, "검은적이 나오는것을 되돌립니다.");
        isParamet = Config.Bind<bool>("General", "검은적 확율화", false, "검은적이 나오는것을 직접설정하게 바꿉니다.");
        Harmony.CreateAndPatchAll(typeof(DeadlyPath));
        Logger.LogInfo($"Mod {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void Update()
    {
        Config.Reload();
    }
}
