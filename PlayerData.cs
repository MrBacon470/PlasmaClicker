using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

[Serializable]
public class PlayerData
{
    public bool offlineProgressCheck;
    #region Realm 1
    public BigDouble plasma;
    public BigDouble plasmaClickValue;
    public BigDouble plasmaCollected;
    public BigDouble darkMatter;
    public BigDouble darkMatterToGet;
    public BigDouble plasmaPerSecond;

    public BigDouble clickUpgrade1Level;
    public BigDouble clickUpgrade2Level;
    public BigDouble clickUpgrade3Level;
    public BigDouble clickUpgrade4Level;
    public BigDouble clickUpgrade5Level;
    public BigDouble clickUpgrade6Level;
    public BigDouble clickUpgrade7Level;
    public BigDouble clickUpgrade8Level;
    public BigDouble clickUpgrade9Level;
    public BigDouble clickUpgrade10Level;

    public BigDouble productionUpgrade1Level;
    public BigDouble productionUpgrade2Level;
    public BigDouble productionUpgrade3Level;
    public BigDouble productionUpgrade4Level;
    public BigDouble productionUpgrade5Level;
    public BigDouble productionUpgrade6Level;
    public BigDouble productionUpgrade7Level;
    public BigDouble productionUpgrade8Level;
    public BigDouble productionUpgrade9Level;
    public BigDouble productionUpgrade10Level;

    public BigDouble productionUpgrade1Power;
    public BigDouble productionUpgrade2Power;
    public BigDouble productionUpgrade3Power;
    public BigDouble productionUpgrade4Power;
    public BigDouble productionUpgrade5Power;
    public BigDouble productionUpgrade6Power;
    public BigDouble productionUpgrade7Power;
    public BigDouble productionUpgrade8Power;
    public BigDouble productionUpgrade9Power;
    public BigDouble productionUpgrade10Power;


    public BigDouble achLevel1;
    public BigDouble achLevel2;
    public BigDouble achLevel3;
    public BigDouble achLevel4;
    public BigDouble achLevel5;
    public BigDouble achLevel6;
    public BigDouble achLevel7;
    public BigDouble achLevel8;
    #endregion


    public BigDouble crystalShards;
    public float[] eventCooldown = new float[7];
    public int eventActiveID;

    #region Prestige
    public int prestigeUlevel1;
    public int prestigeUlevel2;
    public int prestigeUlevel3;
    #endregion

    #region Automators
    public int autoLevel1;
    public int autoLevel2;
    public int autoLevel3;
    public int autoLevel4;
    public int autoLevel5;
    public int autoLevel6;
    public int autoLevel7;
    public int autoLevel8;
    public int autoLevel9;
    public int autoLevel10;
    public int autoLevel11;
    public int autoLevel12;
    public int autoLevel13;
    public int autoLevel14;
    public int autoLevel15;
    public int autoLevel16;
    public int autoLevel17;
    public int autoLevel18;
    public int autoLevel19;
    public int autoLevel20;
    #endregion

    #region Rebirth
    public BigDouble darkEnergy;
    #endregion

    #region Settings
    public short notationType;
    #endregion

    public float gamespeedtimer;
    public bool gamespeedactive;

    #region Realm 2
    public BigDouble quarks;
    public BigDouble positrons;
    public BigDouble realmUpgradeLevel1;
    public BigDouble realmUpgradeLevel2;
    public BigDouble realmUpgradeLevel3;

    #endregion
    #region DailyRewards
    public bool dailyRewardReady;
    public int currentDay;
    public DateTime UTCtime;
    #endregion
    #region Realm 3
    public BigDouble AntiParticles;
    public BigDouble realm3UpgradeLevel1;
    public BigDouble realm3UpgradeLevel2;
    public BigDouble realm3UpgradeLevel3;
    public BigDouble realm3UpgradeLevel4;
    #region Leaks
    public float leakCooldown;
    public BigDouble repairCost;
    public bool isLeaking;
    #endregion
    #endregion

    #region Heat Death
    public bool isheatdeathactive;
    public BigDouble clickUpgrade1LevelH;
    public BigDouble clickUpgrade2LevelH;
    public BigDouble productionUpgrade1LevelH;
    public BigDouble productionUpgrade2LevelH;

    public BigDouble realityShards;
    public BigDouble realityCrystals;
    #endregion


    public PlayerData()
    {
        offlineProgressCheck = false;
        plasma = 0;
        plasmaCollected = 0;
        plasmaClickValue = 1;
        darkMatter = 0;
        darkMatterToGet = 0;
        plasmaPerSecond = 0;

        clickUpgrade1Level = 0;
        clickUpgrade2Level = 0;
        clickUpgrade3Level = 0;
        clickUpgrade4Level = 0;
        clickUpgrade5Level = 0;
        clickUpgrade6Level = 0;
        clickUpgrade7Level = 0;
        clickUpgrade8Level = 0;
        clickUpgrade9Level = 0;
        clickUpgrade10Level = 0;

        productionUpgrade1Level = 0;
        productionUpgrade2Level = 0;
        productionUpgrade3Level = 0;
        productionUpgrade4Level = 0;
        productionUpgrade5Level = 0;
        productionUpgrade6Level = 0;
        productionUpgrade7Level = 0;
        productionUpgrade8Level = 0;
        productionUpgrade9Level = 0;
        productionUpgrade10Level = 0;

        prestigeUlevel1 = 0;
        prestigeUlevel2 = 0;
        prestigeUlevel3 = 0;

        achLevel1 = 0;
        achLevel2 = 0;
        achLevel3 = 0;
        achLevel4 = 0;
        achLevel5 = 0;
        achLevel6 = 0;
        achLevel7 = 0;
        achLevel8 = 0;
        eventActiveID = 0;

        crystalShards = 0;
        for (int i = 0; i < eventCooldown.Length - 1; i++)
            eventCooldown[i] = 0;

        darkEnergy = 0;

        #region Autos
        autoLevel1 = 0;
        autoLevel2 = 0;
        autoLevel3 = 0;
        autoLevel4 = 0;
        #endregion
        #region Settings
        notationType = 0;
        #endregion     
        quarks = 1;
        positrons = 0;

        realmUpgradeLevel1 = 0;
        realmUpgradeLevel2 = 0;
        realmUpgradeLevel3 = 0;

        currentDay = 0;
        UTCtime = DateTime.UtcNow;
        dailyRewardReady = false;

        AntiParticles = 0;
        realm3UpgradeLevel1 = 0;
        realm3UpgradeLevel2 = 0;
        realm3UpgradeLevel3 = 0;
        realm3UpgradeLevel4 = 0;
        leakCooldown = 0;
        repairCost = 0;
        isLeaking = false;

        gamespeedtimer = 0;
        gamespeedactive = false;

        isheatdeathactive = false;

        clickUpgrade1LevelH = 0;
        clickUpgrade2LevelH = 0;
        productionUpgrade1LevelH = 0;
        productionUpgrade2LevelH = 0;

        realityShards = 0;
        realityCrystals = 0;
    }


}
