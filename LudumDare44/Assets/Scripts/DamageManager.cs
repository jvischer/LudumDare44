using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour {

    private static DamageManager _damageManager;

    [SerializeField] private DamageTextController _damageTextControllerPrefab;

    private List<DamageTextController> _availableTextControllers;

    private void Awake() {
        if (_damageManager != null) {
            Destroy(gameObject);
            return;
        }
        _damageManager = this;
        _availableTextControllers = new List<DamageTextController>();
    }

    private DamageTextController CreateNewAvailableTextController() {
        DamageTextController newDamageController = Instantiate(_damageTextControllerPrefab);
        newDamageController.transform.SetParent(transform, false);
        Vector3 position = newDamageController.transform.position;
        position.z = transform.position.z;
        newDamageController.transform.position = position;
        return newDamageController;
    }

    private void ReturnTextControllerToPool(DamageTextController damageTextController) {
        _availableTextControllers.Add(damageTextController);
    }

    public static void DisplayDamageAt(int damage, Vector2 worldPos) {
        Debug.Log(damage + " " + worldPos);
        if (_damageManager == null) {
            Debug.LogError("[DamageManager] Damage manager was null. Could not show damage at position.");
            return;
        }

        DamageTextController chosenTextController = null;
        if (_damageManager._availableTextControllers.Count == 0) {
            chosenTextController = _damageManager.CreateNewAvailableTextController();
        } else {
            int chosenIndex = _damageManager._availableTextControllers.Count - 1;
            chosenTextController = _damageManager._availableTextControllers[chosenIndex];
            _damageManager._availableTextControllers.RemoveAt(chosenIndex);
        }

        chosenTextController.DisplayAt(damage.ToString(), worldPos, _damageManager.ReturnTextControllerToPool);
    }

}
