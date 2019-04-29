using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class HealthBarController : MonoBehaviour {

    private const string DISPLAY_REWARD_TRIGGER = "Show";

    [SerializeField] private TMPro.TextMeshProUGUI[] _weaponDamageText;

    [Space]

    [SerializeField] private Image _healthFillBar;
    [SerializeField] private TMPro.TextMeshProUGUI _healthText;
    [SerializeField] private TMPro.TextMeshProUGUI _gainedHealthText;

    private Animator _animator;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        LevelRewardsController.onPlayerWon += ShowHPGained;
    }

    private void OnDestroy() {
        LevelRewardsController.onPlayerWon -= ShowHPGained;
    }

    private void Update() {
        for (int i = 0; i < _weaponDamageText.Length; i++) {
            _weaponDamageText[i].text = GameManager.gameData.GetCurrentDamage(i).ToString();
        }

        int currHealth = GameManager.gameData.PlayerCurrentHealth;
        int maxHealth = GameManager.gameData.GetPlayerMaxHealth();
        _healthFillBar.fillAmount = Mathf.Clamp01((float) currHealth / maxHealth);
        _healthText.text = string.Format("[{0}/{1}]", currHealth, maxHealth);
    }

    public void ShowHPGained() {
        _gainedHealthText.text = string.Format("+{0} MAX HP", DataManager.GetMaxHPRewardForScene(GameManager.gameData.LoadedJamScene));
        _animator.SetTrigger(DISPLAY_REWARD_TRIGGER);
    }

}
