using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

public class IdleGame : MonoBehaviour
{
    public PlayerData data;
    public EventManager events;
    public PrestigeManager prestige;
    public RebirthManager rebirth;
    public OfflineManager offline;
    public AutomatorManager auto;
    public AchievementManager achievement;
    public UpgradesManager upgrades;
    public RealmManager realm;
    public SaveSystem nonStaticSaveSystem;
    public MarsManager mars;
    public NegativeManager negative;
    
    public Button Singularity;

    public Text plasmaText;
    public Text clickValueText;

    public Text plasmaPerSecText;

    //prestige system


    public BigDouble plasmaTemp;

    public Canvas mainMenuGroup;
    public Canvas upgradesGroup;
    public Canvas settingsGroup;
    public Canvas achievementsGroup;
    public Canvas eventsGroup;
    public Canvas prestigeGroup;
    public Canvas singularityGroup;
    public Canvas realmsGroup;
    public Canvas autoGroup;

    public void Start()
    {
        auto.StartAutomator();
        events.StartEvents();
        prestige.StartPrestige();
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, false);
        Application.runInBackground = true;

        realm.Realm1.gameObject.SetActive(true);
        mainMenuGroup.gameObject.SetActive(true);
        upgradesGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        achievementsGroup.gameObject.SetActive(false);
        eventsGroup.gameObject.SetActive(false);
        prestigeGroup.gameObject.SetActive(false);
        singularityGroup.gameObject.SetActive(false);
        realmsGroup.gameObject.SetActive(false);
        autoGroup.gameObject.SetActive(false);
        realm.Realm2.gameObject.SetActive(false);
        data = SaveSystem.SaveExists("PlayerData") ? SaveSystem.LoadPlayer<PlayerData>("PlayerData") : new PlayerData();
        offline.LoadOfflineProduction();

        TotalPlasmaPerSecond();
        Methods.NotationSettings = data.notationType;
    }

    public void Update()
    {
        
        achievement.RunAchievements();
        prestige.Run();
        rebirth.Run();
        auto.Run();
        upgrades.RunUpgradesUI();
        upgrades.RunUpgrades();

        //progress bars


        if (data.darkMatter >= 1000)
            Singularity.interactable = true;
        else if (data.darkMatter <= 1000)
            Singularity.interactable = false;


        data.darkMatterToGet = 150 * Sqrt(data.plasma / 1e7);

        if (mainMenuGroup.gameObject.activeSelf)
        {

            clickValueText.text = "Click\n+" + Methods.NotationMethod(TotalClickValue(), y: "F0") + " Plasma";
        }

        plasmaPerSecText.text = Methods.NotationMethod(TotalPlasmaPerSecond(), "F0") + " plasma/s";
        plasmaText.text = "Plasma: " + Methods.NotationMethod(data.plasma, y: "F0");

        data.plasma += TotalPlasmaPerSecond() * Time.deltaTime;
        data.plasmaCollected += TotalPlasmaPerSecond() * Time.deltaTime;

    saveTimer += Time.deltaTime;

        if (!(saveTimer >= 15)) return;
        Save();
        data.offlineProgressCheck = true;
        saveTimer = 0;
    }

    public async void Save()
    {
        await nonStaticSaveSystem.AwaitGetUTCTIme();
        SaveSystem.SavePlayer(data, "PlayerData");
    }

    public float saveTimer;

    public void SmoothNumber(ref BigDouble smooth, BigDouble actual)
    {
        if (smooth > actual & actual == 0)
            smooth -= (smooth - actual) / 10 + 0.1 * Time.deltaTime;
        else if (Floor(smooth) < actual)
            smooth += (actual - smooth) / 10 + 0.1 * Time.deltaTime;
        else if (Floor(smooth) > actual)
            smooth -= (smooth - actual) / 10 + 0.1 * Time.deltaTime;
        else
        {
            smooth = actual;
        }
    }

    public BigDouble TotalBoost()
    {
        var temp = prestige.TotalDarkMatterBoost();
        temp *= events.shardsBoost;
        if(data.isLeaking)
            temp *= negative.leakBoost;
        return temp;
    }

    public BigDouble TotalPlasmaPerSecond()
    {
        BigDouble temp = 0;
        temp += data.productionUpgrade1Power * data.productionUpgrade1Level;
        temp += data.productionUpgrade2Power * data.productionUpgrade2Level;
        temp += data.productionUpgrade3Power * data.productionUpgrade3Level;
        temp += data.productionUpgrade4Power * data.productionUpgrade4Level;
        temp += data.productionUpgrade5Power * data.productionUpgrade5Level;
        temp += data.productionUpgrade6Power * data.productionUpgrade6Level;
        temp += data.productionUpgrade7Power * data.productionUpgrade7Level;
        temp += data.productionUpgrade8Power * data.productionUpgrade8Level;
        temp += data.productionUpgrade9Power * data.productionUpgrade9Level;
        temp += data.productionUpgrade10Power * data.productionUpgrade10Level;
        temp *= TotalBoost();
        temp *= Pow(1.1, prestige.prestigeULevels[1]);
        temp *= realm.RRBoost;
        return temp;
    }

    private BigDouble TotalClickValue()
    {
        var temp = data.plasmaClickValue;
        temp *= TotalBoost();
        temp *= Pow(1.5, prestige.prestigeULevels[0]);
        return temp;
    }

    // Buttons
    public void Click()
    {
        data.plasma += TotalClickValue();
        data.plasmaCollected += TotalClickValue();
    }



    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
            
            case "upgrades":
                upgradesGroup.gameObject.SetActive(true);
                break;
            case "main":
                mainMenuGroup.gameObject.SetActive(true);
                break;
            case "gsettings":
                settingsGroup.gameObject.SetActive(true);
                break;
            case "bsettings":
                mainMenuGroup.gameObject.SetActive(true);
                break;
            case "achievements":
                achievementsGroup.gameObject.SetActive(true);
                break;
            case "events":
                eventsGroup.gameObject.SetActive(true);
                break;
            case "prestige":
                prestigeGroup.gameObject.SetActive(true);
                break;
            case "singularity":
                singularityGroup.gameObject.SetActive(true);
                break;
            case "realms":
                realmsGroup.gameObject.SetActive(true);
                break;
            case "automation":
                autoGroup.gameObject.SetActive(true);
                break;
            case "realmsback":
                mainMenuGroup.gameObject.SetActive(true);
                break;
            
        }
    }

    void DisableAll()
    {
        mainMenuGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        upgradesGroup.gameObject.SetActive(false);
        achievementsGroup.gameObject.SetActive(false);
        eventsGroup.gameObject.SetActive(false);
        prestigeGroup.gameObject.SetActive(false);
        singularityGroup.gameObject.SetActive(false);
        autoGroup.gameObject.SetActive(false);
        realmsGroup.gameObject.SetActive(false);
    }

   public void FullReset()
    {
        data = new PlayerData();
        ChangeTabs("main");
    }

}
public class Methods : MonoBehaviour
{
    public static int NotationSettings;

    public static void BigDoubleFill(BigDouble x, BigDouble y, ref Image fill)
    {
        float z;
        var a = x / y;
        if (a < 0.001)
            z = 0;
        else if (a > 10)
            z = 1;
        else
            z = (float)a.ToDouble();
        fill.fillAmount = z;
    }

    public static string NotationMethod(BigDouble x, string y)
    {
        if (x <= 1000) return x.ToString(y);
        switch (NotationSettings)
        {
            case 0:
                {
                    var exponent = Floor(Log10(Abs(x)));
                    var mantissa = x / Pow(10, exponent);
                    return mantissa.ToString("F2") + "e" + exponent;
                }
            case 1:
                {
                    var exponent = 3 * Floor(Floor(Log10(x)) / 3);
                    var mantissa = x / Pow(10, exponent);
                    return mantissa.ToString("F2") + "e" + exponent;
                }
            case 2:
                {

                    var exponent = Floor(Log10(x));
                    var thirdExponent = 3 * Floor(exponent / 3);
                    var mantissa = x / Pow(10, thirdExponent);

                    if (x <= 1000) return x.ToString("F2"); // Normal Number
                    if (x >= 1e308) return mantissa.ToString("F2") + "e" + thirdExponent; // over 1e78 eng notation
                    return (x / Pow(10, thirdExponent)).ToString("F2") + prefixes[thirdExponent.ToDouble()]; //word notation

                }
        }
        return "";
    }
    private static readonly Dictionary<double,string> prefixes = new Dictionary<double, string>()
    {
        {3, " Thousand" },
        {6, " Million" },
        {9, " Billion" },
        {12, " Trillion" },
        {15, " Quadrillion" },
        {18, " Quintillion" },
        {21, " Sextillion" },
        {24, " Septillion" },
        {27, " Octillion" },
        {30, " Nonillion" },
        {33, " Decillion" },
        {36, " Undecillion" },
        {39, " Duodecillion" },
        {42, " Tredecillion" },
        {45, " Quattuordecillion" },
        {48, " Quinquadecillion" },
        {51, " Sexdecillion" },
        {54, " Septendecillion" },
        {57, " Octodecillion" },
        {60, " Novendecillion" },
        {63, " Vigintillion" },
        {66, " Unvigintillion" },
        {69, " Duovigintillion" },
        {72, " Trevigintillion" },
        {75, " Quattuorvigintillion" },
        {78, " Quinvigintillion" },
        {81, " Sexvigintillion" },
        {84, " Septenvigintillion" },
        {87, " Octovigintillion" },
        {90, " Nonvigintillion" },
        {93, " Trigintillion" },
        {96, " Untrigintillion" },
        {99, " Duotrigintillion" },
        {102, " Tretrigintillion" },
        {105, " Quattuortrigintillion"},
        {108, " Quintrigintillion" },
        {111, " Sextrigintillion" },
        {114, " Septentrigintillion" },
        {117, " Octotrigintillion" },
        {120, " Novemtrigintillion" },
        {123, " Quadragintillion"  },
        {126, " Unquadragintillion" },
        {129, " Duoquadragintillion" },
        {132, " Trequadragintillion" },
        {135, " Quattuorquadragintillion" },
        {138, " Quinquadragintillion" },
        {141, " Sexquadragintillion" },
        {144, " Septenquadragintillion" },
        {147, " Octoquadragintillion" },
        {150, " Novemquadragintillion" },
        {153, " Quinquagintillion" },
        {156, " Unquinquagintillion" },
        {159, " Duoquinquagintillion" },
        {162, " Trequinquagintillion" },
        {165, " Quattuorquinquagintillion" },
        {168, " Quinquinquagintillion" },
        {171, " Sexquinquagintillion" },
        {174, " Septenquinquagintillion" },
        {177, " Octoquinquagintillion" },
        {180, " Novemquinquagintillion" },
        {183, " Sexangintillion" },
        {186, " Unsexangintillion" },
        {189, " Duosexangintillion" },
        {192, " Tresexangintillion" },
        {195, " Quattuorsexangintillion" },
        {198, " Quinsexangintillion" },
        {201, " Sexsexangintillion" },
        {204, " Septensexangintillion" },
        {207, " Octosexangintillion" },
        {210, " Novemsexangintillion" },
        {213, " Septuagintillion" },
        {216, " Unseptuagintillion" },
        {219, " Duoseptuagintillion" },
        {222, " Treseptuagintillion" },
        {225, " Quattuorseptuagintillion" },
        {228, " Quinseptuagintillion" },
        {231, " Sexseptuagintliion" },
        {234, " Septenseptuagintllion" },
        {237, " Octoseptuagintillion" },
        {240, " Novemseptuagintillion" },
        {243, " Octogintllion" },
        {246, " Unoctogintillion" },
        {249, " Duooctogintillion" },
        {252, " Treoctogintillion" },
        {255, " Quattuoroctogintillion" },
        {258, " Quinoctogintillion" },
        {261, " Sexoctogintillion" },
        {264, " Septenoctogintillion" },
        {267, " Octooctogintillion" },
        {270, " Novemoctogintillion" },
        {273, " Nonangintillion" },
        {276, " Unnonangintillion" },
        {279, " Duononangintillion" },
        {282, " Trenonagintillion" },
        {285, " Quattuornonangintillion" },
        {288, " Quinnonangintillion" },
        {291, " Sexnonangintillion," },
        {294, " Septennonangintillion" },
        {297, " Octononagintillion" },
        {300, " Novemnonagintillion" },
        {303, " Centillion" },
        {306, " Infinity" }
    };

    public static void BuyMax(ref BigDouble c, BigDouble b, float r, ref BigDouble k)
    {
        var n = Floor(Log((c * (r - 1) / (b * Pow(r, k))) + 1, r));

        var cost = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (c >= cost)
        {
            k += n;
            c -= cost;
        }

    }

    public static void BuyMaxLimit(ref BigDouble c, BigDouble b, float r, ref BigDouble k, int limit)
    {
        var n = Floor(Log((c * (r - 1) / (b * Pow(r, k))) + 1, r));
        if (n + k > limit) n -= k;
        var cost = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (c < cost) return;
        k += n;
        c -= cost;
    }
}