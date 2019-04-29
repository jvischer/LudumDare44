using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WeaponPurchaseButton : MonoBehaviour {

    [SerializeField] private int _weaponId;

    private Button _button;
    private int _cost;

    private void Awake() {
        _button = gameObject.GetComponent<Button>();
    }

    private void Start() {
        _cost = DataManager.GetCostForWeaponLvl(_weaponId + 1);
        Refresh();
    }

    private void Refresh() {
        _button.interactable = !IsOwned;
    }

    public void TryPurchase() {
        if (IsOwned) {
            return;
        }

        if (_weaponId == GameManager.gameData.PlayerWeaponLvl && DataManager.TryRemovePlayerMaxHealth(_cost)) {
            DataManager.AddWeaponLvl();
            Refresh();
        }
    }

    private bool IsOwned {
        get {
            return _weaponId < GameManager.gameData.PlayerWeaponLvl;
        }
    }

}
