using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRewardsController : MonoBehaviour {

    public static Action onPlayerWon;

    private EnemyController[] _enemyControllers;
    private bool _hasWon;

    private void Start() {
        _enemyControllers = GameObject.FindObjectsOfType<EnemyController>();
        _hasWon = false;
    }

    private void Update() {
        if (_hasWon) {
            return;
        }

        bool isThereAnAliveEnemy = false;
        for (int i = 0; i < _enemyControllers.Length; i++) {
            if (_enemyControllers[i] != null && !_enemyControllers[i].isDead) {
                isThereAnAliveEnemy = true;
                break;
            }
        }
        if (!isThereAnAliveEnemy) {
            _hasWon = true;
            DataManager.AddPlayerMaxHealth(DataManager.GetMaxHPRewardForScene(GameManager.gameData.LoadedJamScene));
            JamSceneManager.LoadSceneWithDelay(JamScene.MainMenu, 2.0F);

            if (onPlayerWon != null) {
                onPlayerWon.Invoke();
            }
        }
    }

}
