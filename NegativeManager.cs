using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

public class NegativeManager : MonoBehaviour
{
    public IdleGame game;

    public Canvas Realm3Main;
    public Canvas Realm3Upgrades;
    public Canvas Realm3Leak;

    public Text antiClick;
    public Text StatusText;

    public Text antiText;
    public Text antiPerSecText;
    public Text LeakStatusText;
    public Text LeakText;
    public Text LeakRepairCost;

    public BigDouble antiTemp;
    public BigDouble leakBoost => 1.5 + (.05 * game.data.realm3UpgradeLevel3);
    public BigDouble power = 5;

    public float[] realm3CostMult;
    public BigDouble[] realm3UpgradeBaseCosts;
    public BigDouble[] realm3UpgradeCosts;
    public BigDouble[] realm3UpgradeLevels;

    private string[] upgradeNames;

    public Text[] upgradeText;
    public Image[] upgradeFill;
    public Image[] upgradeFillSmooth;

    private void Start()
    {
        realm3CostMult = new float[] { 1.5f, 1.5f, 1.75f, 1.75f};
        realm3UpgradeBaseCosts = new BigDouble[] { 2, 5, 1e6, 10};
        realm3UpgradeCosts = new BigDouble[4];
        realm3UpgradeLevels = new BigDouble[4];
        upgradeNames = new[] { "Click Power +1", "Particle Converter +1 AP/s", "Leak Boost +5%", "Particle Gateway +5 AP/s"};
    }

    public void Update()
    {
        var data = game.data;
        var time = TimeSpan.FromSeconds(data.leakCooldown);

        data.repairCost = 1e3 + (.25 * data.AntiParticles);

        if (data.leakCooldown > 0)
            data.leakCooldown -= Time.deltaTime;
        else
            data.leakCooldown = 0;

        if(data.isLeaking)
            data.AntiParticles += ((data.realm3UpgradeLevel2 + (power * data.realm3UpgradeLevel4)) * leakBoost) * Time.deltaTime;
        else
            data.AntiParticles += (data.realm3UpgradeLevel2 + (power * data.realm3UpgradeLevel4)) * Time.deltaTime;

        ArrayManager();
        UI();

        void UI()
        {
            if (Realm3Upgrades.gameObject.activeSelf)
            {
                game.SmoothNumber(ref antiTemp, data.AntiParticles);

                for (var i = 0; i < 4; i++)
                {
                    upgradeText[i].text = $"({realm3UpgradeLevels[i]}) {upgradeNames[i]}\nCost: {Methods.NotationMethod(realm3UpgradeCosts[i], "F2")}";
                    Methods.BigDoubleFill(data.AntiParticles, realm3UpgradeCosts[i], ref upgradeFill[i]);
                    Methods.BigDoubleFill(antiTemp, realm3UpgradeCosts[i], ref upgradeFillSmooth[i]);
                }

            }

            if (Realm3Leak.gameObject.activeSelf)
            {
                if (data.isLeaking == false)
                    LeakText.text = data.leakCooldown > 0 ? $"{time.ToString(@"hh\:mm\:ss")}" : "Leak Ready";
                else if (data.isLeaking == true)
                    LeakText.text = $"Leak Active Repair in ({time.ToString(@"hh\:mm\:ss")})";

                LeakRepairCost.text = $"Repair Cost: {Methods.NotationMethod(data.repairCost, "F2")}";
            }

            if(Realm3Main.gameObject.activeSelf)
            {
                StatusText.text = $"Leak Power:{Methods.NotationMethod(leakBoost, "F2")}x\nTotal Repair Time: 2 Hours";
                antiClick.text = data.isLeaking == false ? $"Click +{Methods.NotationMethod(1 + (1 * data.realm3UpgradeLevel1), "F2")} Anti Particles" : $"Click +{Methods.NotationMethod(1 + (1 * data.realm3UpgradeLevel1 * leakBoost), "F2")} Anti Particles";
            }


            antiText.text = $"Anti Particles: {Methods.NotationMethod(data.AntiParticles, "F2")}";
            antiPerSecText.text = data.isLeaking == false ? $"{Methods.NotationMethod(data.realm3UpgradeLevel2 + (power * data.realm3UpgradeLevel4), "F0")} Anti Particles/s" : $"{Methods.NotationMethod((data.realm3UpgradeLevel2 + (power * data.realm3UpgradeLevel4)) * leakBoost, "F2")} Anti Particles/s" ;
            if (data.isLeaking == false)
                LeakStatusText.text = data.leakCooldown > 0 ? $"Restablizing: {time.ToString(@"hh\:mm\:ss")}" : "Leak Status: Ready";
            else if (data.isLeaking == true)
                LeakStatusText.text = $"Leak Status: Leaking\n Time till repaired: ({time.ToString(@"hh\:mm\:ss")})";

        }
        if (data.leakCooldown == 0 && data.isLeaking == true)
            LeakFinished();
    }

    public void Click()
    {
        var data = game.data;
        if(data.isLeaking)
            data.AntiParticles += 1 + (1 * data.realm3UpgradeLevel1 * leakBoost);
        else
            data.AntiParticles += 1 + (1 * data.realm3UpgradeLevel1);
    }

    private void ArrayManager()
    {
        realm3UpgradeLevels[0] = game.data.realm3UpgradeLevel1;
        realm3UpgradeLevels[1] = game.data.realm3UpgradeLevel2;
        realm3UpgradeLevels[2] = game.data.realm3UpgradeLevel3;
        realm3UpgradeLevels[3] = game.data.realm3UpgradeLevel4;

        realm3UpgradeCosts[0] = realm3UpgradeBaseCosts[0] * Pow(realm3CostMult[0], realm3UpgradeLevels[0]);
        realm3UpgradeCosts[1] = realm3UpgradeBaseCosts[1] * Pow(realm3CostMult[1], realm3UpgradeLevels[1]);
        realm3UpgradeCosts[2] = realm3UpgradeBaseCosts[2] * Pow(realm3CostMult[2], realm3UpgradeLevels[2]);
        realm3UpgradeCosts[3] = realm3UpgradeBaseCosts[3] * Pow(realm3CostMult[3], realm3UpgradeLevels[3]);
    }

    private void NonArrayManager()
    {
        game.data.realm3UpgradeLevel1 = realm3UpgradeLevels[0];
        game.data.realm3UpgradeLevel2 = realm3UpgradeLevels[1];
        game.data.realm3UpgradeLevel3 = realm3UpgradeLevels[2];
        game.data.realm3UpgradeLevel4 = realm3UpgradeLevels[3];
    }

    public void StartLeak()
    {
        var data = game.data;

        if(data.leakCooldown <= 0)
        {
            if (data.AntiParticles < data.repairCost) return;
            data.AntiParticles -= data.repairCost;
            data.leakCooldown = 600;
            data.isLeaking = true;
        }
        else
        {
            Debug.Log("Cooldown is Greater than 1 returning out of method");
            return;
        }
        
    }

    public void LeakFinished()
    {
        var data = game.data;

        data.isLeaking = false;
        data.leakCooldown = 7200;
    }

    public void BuyUpgrade(int index)
    {
        if (game.data.AntiParticles >= realm3UpgradeCosts[index])
        {
            game.data.AntiParticles -= realm3UpgradeCosts[index];
            realm3UpgradeLevels[index]++;
        }
        NonArrayManager();

    }

    public void BuyMax()
    {
        Methods.BuyMax(ref game.data.AntiParticles, realm3UpgradeBaseCosts[0], realm3CostMult[0], ref realm3UpgradeLevels[0]);
        Methods.BuyMax(ref game.data.AntiParticles, realm3UpgradeBaseCosts[1], realm3CostMult[1], ref realm3UpgradeLevels[1]);
        Methods.BuyMax(ref game.data.AntiParticles, realm3UpgradeBaseCosts[2], realm3CostMult[2], ref realm3UpgradeLevels[2]);
        Methods.BuyMax(ref game.data.AntiParticles, realm3UpgradeBaseCosts[3], realm3CostMult[3], ref realm3UpgradeLevels[3]);
        NonArrayManager();

    }


    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
            case "upgrades":
                Realm3Upgrades.gameObject.SetActive(true);
                game.mainMenuGroup.gameObject.SetActive(true);
                break;
            case "leak":
                Realm3Leak.gameObject.SetActive(true);
                break;
            case "main":
                Realm3Main.gameObject.SetActive(true);
                break;

        }
    }

    void DisableAll()
    {
        Realm3Main.gameObject.SetActive(false);
        Realm3Leak.gameObject.SetActive(false);
        Realm3Upgrades.gameObject.SetActive(false);
        game.realmsGroup.gameObject.SetActive(false);
    }
}
