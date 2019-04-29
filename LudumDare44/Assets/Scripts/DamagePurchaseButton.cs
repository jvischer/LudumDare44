using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DamagePurchaseButton : MonoBehaviour {

    [SerializeField] private int _damageId;
    [SerializeField] private TMPro.TextMeshProUGUI _descText;

    private Button _button;
    private int _cost;

    private void Awake() {
        _button = gameObject.GetComponent<Button>();
    }

    private void Start() {
        _cost = DataManager.GetCostForDamageLvl(_damageId + 1);
        _descText.text = string.Format("<size=14>+1</size>" + '\n'+ "[{0} HP]", _cost);
        Refresh();
    }

    private void Refresh() {
        _button.interactable = !IsOwned;
    }

    public void TryPurchase() {
        if (IsOwned) {
            return;
        }

        if (_damageId == GameManager.gameData.PlayerDamageLvl && DataManager.TryRemovePlayerMaxHealth(_cost)) {
            DataManager.AddDamageLvl();
            Refresh();
        }
    }

    private bool IsOwned {
        get {
            return _damageId < GameManager.gameData.PlayerDamageLvl;
        }
    }

}
