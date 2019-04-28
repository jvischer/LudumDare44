using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonController : MonoBehaviour {

    [SerializeField] private TMPro.TextMeshProUGUI _levelLabel;

    [Space]

    [SerializeField] private TMPro.TextMeshProUGUI _maxHPRewardLabel;

    [Space]

    [SerializeField] private TMPro.TextMeshProUGUI _enemyHPLabel;
    [SerializeField] private TMPro.TextMeshProUGUI _enemyDmgLabel;

    [Space]

    [SerializeField] private int _world;
    [SerializeField] private int _level;

    private JamScene _jamScene;

    private void Awake() {
        _jamScene = JamSceneManager.ConvertWorldLevelToJamScene(_world, _level);
        int maxHPReward = DataManager.GetMaxHPRewardForScene(_jamScene);
        int enemyHP = DataManager.GetEnemyHPForScene(_jamScene);
        int enemyDmg = DataManager.GetEnemyDmgForScene(_jamScene);

        _levelLabel.text = string.Format("{0}-{1}", _world, _level);

        _maxHPRewardLabel.text = string.Format("Max HP: +{0}", maxHPReward);

        _enemyHPLabel.text = string.Format("HP: {0}", enemyHP);
        _enemyDmgLabel.text = string.Format("DMG: {0}", enemyDmg);
    }

    public void PlayLevel() {
        Debug.Log("Entering level " + _world + "-" + _level + " w/ scene " + _jamScene);
        JamSceneManager.LoadScene(_jamScene);
    }

}
