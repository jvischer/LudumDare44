using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameData _gameData;

    private void Awake() {
        _gameData = DataManager.LoadGameData();
    }

    private void OnApplicationPause() {
        DataManager.SaveGameData(_gameData);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            gameData.PlayerWeaponLvl = 0;
            DataManager.SaveGameData(gameData);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            gameData.PlayerWeaponLvl = 1;
            DataManager.SaveGameData(gameData);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            gameData.PlayerWeaponLvl = 2;
            DataManager.SaveGameData(gameData);
        }
    }

    public static GameData gameData {
        get {
            if (_gameData == null) {
                _gameData = DataManager.LoadGameData();
            }
            return _gameData;
        }
    }

}
