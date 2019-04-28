using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    [SerializeField] private Image _healthFillBar;
    [SerializeField] private TMPro.TextMeshProUGUI _healthText;

    private void Update() {
        int currHealth = GameManager.gameData.PlayerCurrentHealth;
        int maxHealth = GameManager.gameData.GetPlayerHealth();
        _healthFillBar.fillAmount = Mathf.Clamp01((float) currHealth / maxHealth);
        _healthText.text = string.Format("[{0}/{1}]", currHealth, maxHealth);
    }

}
