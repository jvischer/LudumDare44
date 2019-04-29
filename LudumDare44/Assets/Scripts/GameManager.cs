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
    
    public static GameData gameData {
        get {
            if (_gameData == null) {
                _gameData = DataManager.LoadGameData();
            }
            return _gameData;
        }
    }

}
