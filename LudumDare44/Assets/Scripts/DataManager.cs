using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class DataManager {

    private const string GAME_DATA_KEY = "GD";

    public static GameData LoadGameData() {
        string serializedGameData = PlayerPrefs.GetString(GAME_DATA_KEY, string.Empty);
        // If there was no saved game data return the default
        if (serializedGameData.Length == 0) {
            return GameData.GetDefault();
        }
        GameData gameData = JsonUtility.FromJson<GameData>(serializedGameData);
        gameData.TryRespawn();
        return gameData;
    }

    public static void SaveGameData(GameData gameData) {
        string serializedGameData = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(GAME_DATA_KEY, serializedGameData);
        PlayerPrefs.Save();
    }

    public static void UpdateLoadedJamScene(JamScene jamScene) {
        GameManager.gameData.LoadedJamScene = jamScene;
        SaveGameData(GameManager.gameData);
    }

    public static bool TakeDamage(int amount) {
        GameManager.gameData.PlayerCurrentHealth -= amount;
        if (GameManager.gameData.PlayerCurrentHealth <= 0) {
            GameManager.gameData.PlayerCurrentHealth = 0;
            return true;
        }
        return false;
    }

    public static void AddPlayerHealth(int amount) {
        int maxHealth = GameManager.gameData.GetPlayerHealth();
        if (GameManager.gameData.PlayerCurrentHealth + amount > maxHealth) {
            GameManager.gameData.PlayerCurrentHealth = maxHealth;
        } else {
            GameManager.gameData.PlayerCurrentHealth += amount;
        }
    }

    public static int GetMaxHPRewardForScene(JamScene jamScene) {
        switch (jamScene) {
            case JamScene.OneDashOne:
                return 1;
            case JamScene.OneDashTwo:
                return 2;
            case JamScene.OneDashThree:
                return 3;
            case JamScene.TwoDashOne:
                return 5;
            case JamScene.TwoDashTwo:
                return 7;
            case JamScene.TwoDashThree:
                return 9;
            case JamScene.ThreeDashOne:
                return 12;
            case JamScene.ThreeDashTwo:
                return 15;
            case JamScene.ThreeDashThree:
                return 20;
        }
        return 0;
    }

    public static int GetEnemyHPForScene(JamScene jamScene) {
        switch (jamScene) {
            case JamScene.OneDashOne:
                return 2;
            case JamScene.OneDashTwo:
                return 4;
            case JamScene.OneDashThree:
                return 6;
            case JamScene.TwoDashOne:
                return 9;
            case JamScene.TwoDashTwo:
                return 12;
            case JamScene.TwoDashThree:
                return 15;
            case JamScene.ThreeDashOne:
                return 20;
            case JamScene.ThreeDashTwo:
                return 25;
            case JamScene.ThreeDashThree:
                return 30;
        }
        return 10;
    }

    public static int GetEnemyDmgForScene(JamScene jamScene) {
        switch (jamScene) {
            case JamScene.OneDashOne:
                return 1;
            case JamScene.OneDashTwo:
                return 2;
            case JamScene.OneDashThree:
                return 3;
            case JamScene.TwoDashOne:
                return 5;
            case JamScene.TwoDashTwo:
                return 7;
            case JamScene.TwoDashThree:
                return 9;
            case JamScene.ThreeDashOne:
                return 12;
            case JamScene.ThreeDashTwo:
                return 15;
            case JamScene.ThreeDashThree:
                return 18;
        }
        return 1;
    }

}
