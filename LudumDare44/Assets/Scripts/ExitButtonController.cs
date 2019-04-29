using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : MonoBehaviour {

#if UNITY_WEBGL
    private void Start() {
        // Hides the button from WebGL builds since it does nothing anyway
        gameObject.SetActive(false);
    }
#endif

}
