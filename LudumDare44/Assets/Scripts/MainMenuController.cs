using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    private void Update() {
        if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.K) &&
            (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))) {
            DataManager.SaveGameData(GameData.GetDefault());
            JamSceneManager.ReloadSceneWithDelay(0);
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Insert)) {
            GameManager.gameData.PlayerWeaponLvl = 2;
            GameManager.gameData.PlayerDamageLvl = 11;
            GameManager.gameData.PlayerMaxHealth = 777;
            DataManager.SaveGameData(GameManager.gameData);
            JamSceneManager.ReloadSceneWithDelay(0);
        }
    }

    public void ExitGame() {
        Application.Quit();
    }

}
