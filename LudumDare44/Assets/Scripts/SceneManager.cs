using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class JamSceneManager {

    public static readonly Dictionary<JamScene, int> JAM_SCENE_TO_BUILD_INDEX = new Dictionary<JamScene, int>() {
        { JamScene.MainMenu, 0 },
        { JamScene.Area,     1 },
    };

    private static bool _isLoadingScene = false;
    private static int _sceneBuildIndexToLoad = -1;

    public static void ReloadSceneWithDelay(float delay) {
        PlayerController.instance.StartCoroutine(reloadSceneAfterDelay(delay));
    }

    private static IEnumerator reloadSceneAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadScene(JamScene jamScene) {
        LoadScene(JAM_SCENE_TO_BUILD_INDEX[jamScene]);
    }

    public static void LoadScene(int sceneBuildIndex) {
        if (_isLoadingScene) {
            Debug.Log("[JamSceneManager] Could not load scene " + sceneBuildIndex + " because there was already a scene being loaded");
            return;
        }

        _isLoadingScene = true;
        _sceneBuildIndexToLoad = sceneBuildIndex;
        ScreenFaderController.FadeOut(screenFaderController_OnFadedOut);
    }

    private static void screenFaderController_OnFadedOut() {
        _isLoadingScene = false;
        SceneManager.LoadScene(_sceneBuildIndexToLoad);
    }

}

public enum JamScene {
    MainMenu,
    Area,
}
