using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class MarsManager : MonoBehaviour
{
    public IdleGame game;
    public Text quarksText;
    public Text realmClickText;
    public Text realmSoftCapText;
    public Text realmQuarkPerSecText;
    public Text positronText;
    public Text prestigeText;

    public Canvas upgradesGroup;
    public Canvas realm2Group;
    public Canvas prestigeGroup;

    public BigDouble taxesExponentFactor => Pow(Log10(game.data.quarks + 1) + 1, 0.1);
    public BigDouble taxesExponentFactorClick => Pow(Log10(game.data.quarks + 1) + 1, 1);
    public BigDouble realmTaxes => game.data.quarks;
    public BigDouble positronBoost => (game.data.positrons / 100) + 1;

    private BigDouble quarksTemp;
    private BigDouble quarksPerSec => Pow(game.data.quarks * (realm2UpgradeLevels[2] * 0.01), 1.01 / taxesExponentFactor);
    private BigDouble positronsToGet;

    //Save
    public float[] realmCostMult;
    public BigDouble[] realmUpgradeBaseCosts;
    public BigDouble[] realm2UpgradeCosts;
    public BigDouble[] realm2UpgradeLevels;

    private string[] upgradeNames;

    public Text[] upgradeText;
    public Image[] upgradeFill;
    public Image[] upgradeFillSmooth;


    private void Start()
    {
        realmCostMult = new float[] { 2.5f, 5, 5};
        realmUpgradeBaseCosts = new BigDouble[] { 2, 100, 100};
        realm2UpgradeCosts = new BigDouble[3];
        realm2UpgradeLevels = new BigDouble[3];
        upgradeNames = new[] { "Click Power +0.01x", "Click Power +0.05x", "Gain +0.01 of your quarks per second"};
    }



    public void Update()
    {
        var data = game.data;
        ArrayManager();
        UI();

        positronsToGet = Log10(data.quarks + 1);

        data.quarks += quarksPerSec * Time.deltaTime;

        void UI()
        {
            if (upgradesGroup.gameObject.activeSelf)
            {
                game.SmoothNumber(ref quarksTemp, data.quarks);
                
                for (var i = 0; i < 3; i++)
                {
                    upgradeText[i].text = $"({realm2UpgradeLevels[i]}) {upgradeNames[i]}\nCost: {Methods.NotationMethod(realm2UpgradeCosts[i], "F2")}";
                    Methods.BigDoubleFill(data.quarks, realm2UpgradeCosts[i], ref upgradeFill[i]);
                    Methods.BigDoubleFill(quarksTemp, realm2UpgradeCosts[i], ref upgradeFillSmooth[i]);
                }

            }

            if(prestigeGroup.gameObject.activeSelf)
            {
                prestigeText.text = $"+{Methods.NotationMethod(positronsToGet, "F0")} Positrons";
            }

            if (!game.realm.Realm2.gameObject.activeSelf) return;
            quarksText.text = $"Quarks:{Methods.NotationMethod(data.quarks, "F2")}";
            positronText.text = $"Positrons: {Methods.NotationMethod(data.positrons, "F0")}\n{Methods.NotationMethod(positronBoost, "F2")}x boost";
            realmClickText.text = $"Click\n+{Pow(totalClickPower, 1 / taxesExponentFactorClick):F2}x Quarks";
            realmQuarkPerSecText.text = $"{Methods.NotationMethod(quarksPerSec, "F2")} Quarks/s";
            realmSoftCapText.text = $"Hadron Particle Tax:\n{Methods.NotationMethod(taxesMultPerSecond(), "F2")} less Quarks/s\n {Methods.NotationMethod(taxesMultPerClick(), "F2")}x less Quarks per Click";
        }

    }

    BigDouble taxesMultPerSecond()
    {
        if (realm2UpgradeLevels[2] == 0) return 1;
        return (game.data.quarks * (realm2UpgradeLevels[2] * 0.01)) / taxesExponentFactor;
    }

    BigDouble taxesMultPerClick()
    {
        return totalClickPower / Pow(totalClickPower, 1 / taxesExponentFactorClick);
    }

    public void Click()
    {
        var data = game.data;
        data.quarks *= Pow(totalClickPower, 1 / taxesExponentFactorClick);
    }

    public BigDouble clickPower1 => 1.01 + (0.01 * realm2UpgradeLevels[0]);
    public BigDouble clickPower2 => 1.01 + (0.05 * realm2UpgradeLevels[1]);
    public BigDouble totalClickPower => clickPower1 + clickPower2;

    public void BuyUpgrade(int index)
    {
        if(game.data.quarks >= realm2UpgradeCosts[index])
        {
            game.data.quarks -= realm2UpgradeCosts[index];
            realm2UpgradeLevels[index]++;
        }
        NonArrayManager();
        
    }

    public void BuyMax()
    {
        Methods.BuyMax(ref game.data.quarks, realmUpgradeBaseCosts[0], realmCostMult[0], ref realm2UpgradeLevels[0]);
        Methods.BuyMax(ref game.data.quarks, realmUpgradeBaseCosts[1], realmCostMult[1], ref realm2UpgradeLevels[1]);
        Methods.BuyMax(ref game.data.quarks, realmUpgradeBaseCosts[2], realmCostMult[2], ref realm2UpgradeLevels[2]);
        NonArrayManager();
        
    }

    private void ArrayManager()
    {
        realm2UpgradeLevels[0] = game.data.realmUpgradeLevel1;
        realm2UpgradeLevels[1] = game.data.realmUpgradeLevel2;
        realm2UpgradeLevels[2] = game.data.realmUpgradeLevel3;

        realm2UpgradeCosts[0] = realmUpgradeBaseCosts[0] * Pow(realmCostMult[0], realm2UpgradeLevels[0]);
        realm2UpgradeCosts[1] = realmUpgradeBaseCosts[1] * Pow(realmCostMult[1], realm2UpgradeLevels[1]);
        realm2UpgradeCosts[2] = realmUpgradeBaseCosts[2] * Pow(realmCostMult[2], realm2UpgradeLevels[2]);
    }

    private void NonArrayManager()
    {
        game.data.realmUpgradeLevel1 = realm2UpgradeLevels[0];
        game.data.realmUpgradeLevel2 = realm2UpgradeLevels[1];
        game.data.realmUpgradeLevel3 = realm2UpgradeLevels[2];
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
                realm2Group.gameObject.SetActive(true);
                break;
            case "prestige":
                prestigeGroup.gameObject.SetActive(true);
                break;
        }
    }

    void DisableAll()
    {
        upgradesGroup.gameObject.SetActive(false);
        realm2Group.gameObject.SetActive(false);
        prestigeGroup.gameObject.SetActive(false);
    }

    public void PrestigeRealm()
    {
        var data = game.data;
        if (data.quarks < 1e10) return;
        DisableAll();
        realm2Group.gameObject.SetActive(true);

        data.positrons = positronsToGet;

        data.quarks = 1;
        data.realmUpgradeLevel1 = 0;
        data.realmUpgradeLevel2 = 0;
        data.realmUpgradeLevel3 = 0;

        data.achLevel6 = 0;
    }
}
