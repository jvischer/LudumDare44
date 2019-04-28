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
        return JsonUtility.FromJson<GameData>(serializedGameData);
    }

    public static void SaveGameData(GameData gameData) {
        string serializedGameData = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(GAME_DATA_KEY, serializedGameData);
        PlayerPrefs.Save();
    }
    
    public static void AddPlayerHealth(int amount) {
        int maxHealth = GameManager.gameData.GetPlayerHealth();
        if (GameManager.gameData.PlayerCurrentHealth + amount > maxHealth) {
            GameManager.gameData.PlayerCurrentHealth = maxHealth;
        } else {
            GameManager.gameData.PlayerCurrentHealth += amount;
        }
    }

}
