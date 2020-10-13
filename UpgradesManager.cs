using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class UpgradesManager : MonoBehaviour
{
    public IdleGame game;
    public PrestigeManager prestige;
    public Image[] clickUpgradeBar;
    public Image[] clickUpgradeBarSmooth;
    public Image[] proUpgradeBarSmooth;
    public Image[] proUpgradeBar;

    public GameObject[] clickUpgrade = new GameObject[10];
    public GameObject[] productionUpgrade = new GameObject[10];
    
    public Text[] clickUpgradeMaxText = new Text[10];
    public Text[] productionUpgradeMaxText = new Text[10];
    public Text[] clickUpgradeText = new Text[10];
    public Text[] productionUpgradeText = new Text[10];

    public BigDouble[] clickUpgradeCost;
    public BigDouble[] productionUpgradeCost;
    public BigDouble[] clickUpgradePower;
    public BigDouble[] clickUpgradeLevels;
    public BigDouble[] productionUpgradeLevels;
    public BigDouble[] clickUpgradeCostMults;
    public BigDouble[] productionUpgradeCostMults;
    public BigDouble[] clickUpgradeBaseCosts;
    public BigDouble[] productionUpgradeBaseCosts;
    public BigDouble[] clickUpgradeUnlockCosts;
    public BigDouble[] productionUpgradeUnlockCosts;
    public BigDouble[] productionUpgradePower;

    

    private void Start()
    {
        clickUpgradeUnlockCosts = new BigDouble[] { 5, 50, 250, 500, 500e3, 500e4, 500e6, 500e7, 5000e10, 500e20 };
        productionUpgradeUnlockCosts = new BigDouble[] { 50, 250, 500, 500e3, 500e4, 500e5, 500e6, 500e7, 500e10, 500e20 };
        clickUpgradeCostMults = new BigDouble[] { 1.5, 1.75, 2, 2.25, 2.50, 2.75, 3, 3.25, 3.50, 3.75 };
        productionUpgradeCostMults = new BigDouble[] { 1.5, 1.75, 2, 2.25, 2.50, 2.75, 3, 3.25, 3.50, 3.75 };
        clickUpgradePower = new BigDouble[]{ 1, 5, 10, 50, 100, 250, 500, 1e3, 1e4, 1e6 };
        productionUpgradePower = new BigDouble[] { 1, 5, 25, 100, 250, 500, 1e3, 1e9, 1e10, 1e15 };
        clickUpgradeBaseCosts = new BigDouble[] { 10, 100, 500, 1e3, 1e6, 1e8, 1e12, 1e14, 1e20, 1e40 };
        productionUpgradeBaseCosts = new BigDouble[] { 100, 500, 1e3, 1e6, 1e8, 1e10, 1e12, 1e14, 1e20, 1e40 };
        clickUpgradeCost = new BigDouble[10];
        productionUpgradeCost = new BigDouble[10];
        clickUpgradeLevels = new BigDouble[10];
        productionUpgradeLevels = new BigDouble[10];
    }

    public void RunUpgrades()
    {
        var data = game.data;
        ArrayManager();
        clickUpgradeCost[0] = clickUpgradeBaseCosts[0] * Pow(clickUpgradeCostMults[0], data.clickUpgrade1Level);
        clickUpgradeCost[1] = clickUpgradeBaseCosts[1] * Pow(clickUpgradeCostMults[1], data.clickUpgrade2Level);
        clickUpgradeCost[2] = clickUpgradeBaseCosts[2] * Pow(clickUpgradeCostMults[2], data.clickUpgrade3Level);
        clickUpgradeCost[3] = clickUpgradeBaseCosts[3] * Pow(clickUpgradeCostMults[3], data.clickUpgrade4Level);
        clickUpgradeCost[4] = clickUpgradeBaseCosts[4] * Pow(clickUpgradeCostMults[4], data.clickUpgrade5Level);
        clickUpgradeCost[5] = clickUpgradeBaseCosts[5] * Pow(clickUpgradeCostMults[5], data.clickUpgrade6Level);
        clickUpgradeCost[6] = clickUpgradeBaseCosts[6] * Pow(clickUpgradeCostMults[6], data.clickUpgrade2Level);
        clickUpgradeCost[7] = clickUpgradeBaseCosts[7] * Pow(clickUpgradeCostMults[7], data.clickUpgrade8Level);
        clickUpgradeCost[8] = clickUpgradeBaseCosts[8] * Pow(clickUpgradeCostMults[8], data.clickUpgrade9Level);
        clickUpgradeCost[9] = clickUpgradeBaseCosts[9] * Pow(clickUpgradeCostMults[9], data.clickUpgrade10Level);

        productionUpgradeCost[0] = productionUpgradeBaseCosts[0] * Pow(productionUpgradeCostMults[0], data.productionUpgrade1Level);
        productionUpgradeCost[1] = productionUpgradeBaseCosts[1] * Pow(productionUpgradeCostMults[1], data.productionUpgrade2Level);
        productionUpgradeCost[2] = productionUpgradeBaseCosts[2] * Pow(productionUpgradeCostMults[2], data.productionUpgrade3Level);
        productionUpgradeCost[3] = productionUpgradeBaseCosts[3] * Pow(productionUpgradeCostMults[3], data.productionUpgrade4Level);
        productionUpgradeCost[4] = productionUpgradeBaseCosts[4] * Pow(productionUpgradeCostMults[4], data.productionUpgrade5Level);
        productionUpgradeCost[5] = productionUpgradeBaseCosts[5] * Pow(productionUpgradeCostMults[5], data.productionUpgrade6Level);
        productionUpgradeCost[6] = productionUpgradeBaseCosts[6] * Pow(productionUpgradeCostMults[6], data.productionUpgrade7Level);
        productionUpgradeCost[7] = productionUpgradeBaseCosts[7] * Pow(productionUpgradeCostMults[7], data.productionUpgrade8Level);
        productionUpgradeCost[8] = productionUpgradeBaseCosts[8] * Pow(productionUpgradeCostMults[8], data.productionUpgrade9Level);
        productionUpgradeCost[9] = productionUpgradeBaseCosts[9] * Pow(productionUpgradeCostMults[9], data.productionUpgrade10Level);

        data.productionUpgrade1Power = productionUpgradePower[0];
        data.productionUpgrade2Power = productionUpgradePower[1];
        data.productionUpgrade3Power = productionUpgradePower[2];
        data.productionUpgrade4Power = productionUpgradePower[3];
        data.productionUpgrade5Power = productionUpgradePower[4];
        data.productionUpgrade6Power = productionUpgradePower[5];
        data.productionUpgrade7Power = productionUpgradePower[6];
        data.productionUpgrade8Power = productionUpgradePower[7];
        data.productionUpgrade9Power = productionUpgradePower[8];
        data.productionUpgrade10Power = productionUpgradePower[9];
    }

    public void RunUpgradesUI()
    {
        var data = game.data;
        if (game.upgradesGroup.gameObject.activeSelf)
        {

            for (var i = 0; i < 10; i++)
            {
                clickUpgradeText[i].text = $"Click Upgrade {i + 1}\nCost: {GetUpgradeCost(i, clickUpgradeCost)} plasma\nPower: +{clickUpgradePower[i]}\nLevel: {GetUpgradeLevel(i, clickUpgradeLevels)}";
                clickUpgradeMaxText[i].text = $"Buy Max ({BuyClickUpgradeMaxCount(i)})";
                productionUpgradeMaxText[i].text = $"Buy Max ({BuyProductionUpgradeMaxCount(i)})";
                clickUpgrade[i].gameObject.SetActive(data.plasmaCollected >= clickUpgradeUnlockCosts[i]);
                productionUpgrade[i].gameObject.SetActive(data.plasmaCollected >= productionUpgradeUnlockCosts[i]);
                Methods.BigDoubleFill(data.plasma, clickUpgradeCost[i], ref clickUpgradeBar[i]);
                Methods.BigDoubleFill(game.plasmaTemp, clickUpgradeCost[i], ref clickUpgradeBarSmooth[i]);
                Methods.BigDoubleFill(data.plasma, productionUpgradeCost[i], ref proUpgradeBar[i]);
                Methods.BigDoubleFill(game.plasmaTemp, productionUpgradeCost[i], ref proUpgradeBarSmooth[i]);
            }
            

            productionUpgradeText[0].text = $"Plasma Harvester\nCost: {GetUpgradeCost(0, productionUpgradeCost)} plasma\nPower: {Methods.NotationMethod(productionUpgradePower[0] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")} Plasma/s\nLevel: {GetUpgradeLevel(0, productionUpgradeLevels)}";
            productionUpgradeText[1].text = $"Power Cell\nCost   {GetUpgradeCost(1, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[1] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(1, productionUpgradeLevels)}";
            productionUpgradeText[2].text = $"Plasma Transporter\nCost   {GetUpgradeCost(2, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[2] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(2, productionUpgradeLevels)}";
            productionUpgradeText[3].text = $"Plasma Refiner\nCost   {GetUpgradeCost(3, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[3] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(3, productionUpgradeLevels)}";
            productionUpgradeText[4].text = $"Plasma Synthesizer\nCost   {GetUpgradeCost(4, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[4] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(4, productionUpgradeLevels)}";
            productionUpgradeText[5].text = $"Plasma Portal\nCost   {GetUpgradeCost(5, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[5] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(5, productionUpgradeLevels)}";
            productionUpgradeText[6].text = $"Plasma Temple\nCost   {GetUpgradeCost(6, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[6] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(6, productionUpgradeLevels)}";
            productionUpgradeText[7].text = $"Plasma Forge\nCost   {GetUpgradeCost(7, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[7] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(7, productionUpgradeLevels)}";
            productionUpgradeText[8].text = $"Plasma Reactor\nCost   {GetUpgradeCost(8, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[8] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(8, productionUpgradeLevels)}";
            productionUpgradeText[9].text = $"Plasma Gateway\nCost   {GetUpgradeCost(1, productionUpgradeCost)}   plasma\nPower:  {Methods.NotationMethod(productionUpgradePower[9] * game.TotalBoost() * Pow(1.5, prestige.prestigeULevels[1]) + (Log(Sqrt(game.data.quarks) + 1, 20) + 1), "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(9, productionUpgradeLevels)}";

            game.SmoothNumber(ref game.plasmaTemp, data.plasma);
        }

        string GetUpgradeCost(int index, BigDouble[] upgrade)
            {
                return Methods.NotationMethod(upgrade[index], "F2");
            }

            string GetUpgradeLevel(int index, BigDouble[] upgradeLevel)
            {
                return Methods.NotationMethod(upgradeLevel[index], "F2");
            }

    }



    private void ArrayManager()
    {
        var data = game.data;
        clickUpgradeLevels[0] = data.clickUpgrade1Level;
        clickUpgradeLevels[1] = data.clickUpgrade2Level;
        clickUpgradeLevels[2] = data.clickUpgrade3Level;
        clickUpgradeLevels[3] = data.clickUpgrade4Level;
        clickUpgradeLevels[4] = data.clickUpgrade5Level;
        clickUpgradeLevels[5] = data.clickUpgrade6Level;
        clickUpgradeLevels[6] = data.clickUpgrade7Level;
        clickUpgradeLevels[7] = data.clickUpgrade8Level;
        clickUpgradeLevels[8] = data.clickUpgrade9Level;
        clickUpgradeLevels[9] = data.clickUpgrade10Level;

        productionUpgradeLevels[0] = data.productionUpgrade1Level;
        productionUpgradeLevels[1] = data.productionUpgrade2Level;
        productionUpgradeLevels[2] = data.productionUpgrade3Level;
        productionUpgradeLevels[3] = data.productionUpgrade4Level;
        productionUpgradeLevels[4] = data.productionUpgrade5Level;
        productionUpgradeLevels[5] = data.productionUpgrade6Level;
        productionUpgradeLevels[6] = data.productionUpgrade7Level;
        productionUpgradeLevels[7] = data.productionUpgrade8Level;
        productionUpgradeLevels[8] = data.productionUpgrade9Level;
        productionUpgradeLevels[9] = data.productionUpgrade10Level;
    }

    private void NonArrayManager()
    {
        var data = game.data;
        data.clickUpgrade1Level = clickUpgradeLevels[0];
        data.clickUpgrade2Level = clickUpgradeLevels[1];
        data.clickUpgrade3Level = clickUpgradeLevels[2];
        data.clickUpgrade4Level = clickUpgradeLevels[3];
        data.clickUpgrade5Level = clickUpgradeLevels[4];
        data.clickUpgrade6Level = clickUpgradeLevels[5];
        data.clickUpgrade7Level = clickUpgradeLevels[6];
        data.clickUpgrade8Level = clickUpgradeLevels[7];
        data.clickUpgrade9Level = clickUpgradeLevels[8];
        data.clickUpgrade10Level = clickUpgradeLevels[9];

        data.productionUpgrade1Level = productionUpgradeLevels[0];
        data.productionUpgrade2Level = productionUpgradeLevels[1];
        data.productionUpgrade3Level = productionUpgradeLevels[2];
        data.productionUpgrade4Level = productionUpgradeLevels[3];
        data.productionUpgrade5Level = productionUpgradeLevels[4];
        data.productionUpgrade6Level = productionUpgradeLevels[5];
        data.productionUpgrade7Level = productionUpgradeLevels[6];
        data.productionUpgrade8Level = productionUpgradeLevels[7];
        data.productionUpgrade9Level = productionUpgradeLevels[8];
        data.productionUpgrade10Level = productionUpgradeLevels[9];
    }

    public void BuyClickUpgrade(int index)
    {
        var data = game.data;
        if (data.plasma >= clickUpgradeCost[index])
        {
            clickUpgradeLevels[index]++;
            data.plasma -= clickUpgradeCost[index];
            data.plasmaClickValue += clickUpgradePower[index];
        }
        NonArrayManager();
    }
    public void BuyClickUpgradeMax(int index)
    {
        var data = game.data;
        var b = clickUpgradeBaseCosts[index];
        var c = data.plasma;
        var r = clickUpgradeCostMults[index];
        var k = clickUpgradeLevels[index];
        var n = Floor(Log((c * (r - 1) / (b * Pow(r, k))) + 1, r));

        var cost2 = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (data.plasma >= cost2)
        {
            clickUpgradeLevels[index] += n;
            data.plasma -= cost2;
            data.plasmaClickValue += n * clickUpgradePower[index];
        }
        NonArrayManager();
    }

public void BuyProductionUpgradeMax(int index)
{
        var data = game.data;
        var b1 = productionUpgradeBaseCosts[index];
    var c1 = data.plasma;
    var r1 = productionUpgradeCostMults[index];
    var k1 = productionUpgradeLevels[index];
    var n1 = Floor(Log((c1 * (r1 - 1) / (b1 * Pow(r1, k1))) + 1, r1));

    var cost2 = b1 * (Pow(r1, k1) * (Pow(r1, n1) - 1) / (r1 - 1));

    if (data.plasma >= cost2)
    {
        productionUpgradeLevels[index] += n1;
        data.plasma -= cost2;
    }
    NonArrayManager();
}

public void BuyProductionUpgrade(int index)
    {
        var data = game.data;
        if (data.plasma >= productionUpgradeCost[index])
        {
            productionUpgradeLevels[index]++;
            data.plasma -= productionUpgradeCost[index];
        }
        NonArrayManager();
    }
        //maxcounts
    public BigDouble BuyClickUpgradeMaxCount(int index)
    {
        var data = game.data;
        var b = clickUpgradeBaseCosts[index];
        var c = data.plasma;
        var r = clickUpgradeCostMults[index];
        var k = clickUpgradeLevels[index];
        var n = Floor(Log((c * (r - 1) / (b * Pow(r, k))) + 1, r));
        return n;
    }

    public BigDouble BuyProductionUpgradeMaxCount(int index)
    {
        var data = game.data;
        var b = productionUpgradeBaseCosts[index];
        var c = data.plasma;
        var r = productionUpgradeCostMults[index];
        var k = productionUpgradeLevels[index];
        var n = Floor(Log((c * (r - 1) / (b * Pow(r, k))) + 1, r));
        return n;
    }

}
