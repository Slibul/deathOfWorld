using BepInEx;
using BepInEx.Configuration;
using DeadlyOfWorld;
using HarmonyLib;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public enum DarkEnum
    {
        All = 1,
        Common =2,
        Rare = 3,
        Uniq = 4,
        Legend = 5,
        Percent = 6
    }


    public static ConfigEntry<DarkEnum> Step;
    public static ConfigEntry<int> isSetP;
    public static ConfigEntry<bool> isNoDarker;
    public static ConfigEntry<double> Death;
    public static ConfigEntry<double> Death1;
    public static ConfigEntry<double> Death2;
    public static ConfigEntry<double> Death3;
    public static ConfigEntry<double> Deal;
    public static ConfigEntry<bool> TrueAllDark;
    private void Awake()
    {
        Step = Config.Bind<DarkEnum>("General", "나오는 정도", DarkEnum.All, "All : 전부\nCommon : 일반템이 나오는정도\nRare : 레어템이 나오는정도\nUniq :유니크템이 나오는정도\nLegend : 레전드템이 뜨는정도\nPercent : 본인이 원하는만큼 설정합니다.");
        isSetP = Config.Bind<int>("General", "확율", 0, "Percent적용시\n0~100까지의확율로 검은적이 나오게 설정할수있습니다.");
        isNoDarker = Config.Bind<bool>("General", "검은적 정상화", false, "검은적이 나오는것을 되돌립니다.");
        Death = Config.Bind<double>("General", "모험가/보스체력 조정", 1.0, "보스와 모험가의 체력을 곱합니다.");
        Death1 = Config.Bind<double>("General", "일반몹체력 조정", 1.0, "일반몹의 체력을 곱합니다.");
        Death2 = Config.Bind<double>("General", "모험가/보스데미지 조정", 1.0, "보스와 모험가의 데미지을 곱합니다.");
        Death3 = Config.Bind<double>("General", "일반몹 데미지 조정", 1.0, "보스와 모험가의 데미지을 곱합니다.");
        Deal = Config.Bind<double>("General", "데미지 조정", 0.1, "데미지를 조정합니다. (0.1 당 10%)");
        TrueAllDark = Config.Bind<bool>("General", "진짜 모든 검은적", false, "지옥을 알고싶다면 이것을 켜보시오.");
        Harmony.CreateAndPatchAll(typeof(DeadlyPath));
        Logger.LogInfo($"Mod {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void Update()
    {
        Config.Reload();
    }
}
