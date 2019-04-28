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
        { JamScene.MainMenu,       0 },
        { JamScene.OneDashOne,     1 },
        { JamScene.OneDashTwo,     1 },
        { JamScene.OneDashThree,   1 },
        { JamScene.TwoDashOne,     2 },
        { JamScene.TwoDashTwo,     2 },
        { JamScene.TwoDashThree,   2 },
        { JamScene.ThreeDashOne,   3 },
        { JamScene.ThreeDashTwo,   3 },
        { JamScene.ThreeDashThree, 3 },
    };

    private static bool _isLoadingScene = false;
    private static int _sceneBuildIndexToLoad = -1;

    public static void LoadSceneWithDelay(JamScene jamScene, float delay) {
        PlayerController.instance.StartCoroutine(loadSceneAfterDelay(jamScene, delay));
    }

    private static IEnumerator loadSceneAfterDelay(JamScene jamScene, float delay) {
        yield return new WaitForSeconds(delay);
        LoadScene(jamScene);
    }

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

    public static JamScene ConvertWorldLevelToJamScene(int world, int level) {
        if (world == 1 && level == 1) {
            return JamScene.OneDashOne;
        } else if (world == 1 && level == 2) {
            return JamScene.OneDashTwo;
        } else if (world == 1 && level == 3) {
            return JamScene.OneDashThree;
        } else if (world == 2 && level == 1) {
            return JamScene.TwoDashOne;
        } else if (world == 2 && level == 2) {
            return JamScene.TwoDashTwo;
        } else if (world == 2 && level == 3) {
            return JamScene.TwoDashThree;
        } else if (world == 3 && level == 1) {
            return JamScene.ThreeDashOne;
        } else if (world == 3 && level == 2) {
            return JamScene.ThreeDashTwo;
        } else if (world == 3 && level == 3) {
            return JamScene.ThreeDashThree;
        }
        return JamScene.Invalid;
    }

}

public enum JamScene {
    Invalid,
    MainMenu,
    OneDashOne,
    OneDashTwo,
    OneDashThree,
    TwoDashOne,
    TwoDashTwo,
    TwoDashThree,
    ThreeDashOne,
    ThreeDashTwo,
    ThreeDashThree,
}
