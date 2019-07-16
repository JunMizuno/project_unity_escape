using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using DG.Tweening;

/// <summary>
/// アプリ起動時の実行メソッド群
/// @todo. 起動時に自動的にDontDestoroyOnLoadが生成される？？
/// </summary>
public class RuntimeInitialize
{
    // @memo. 起動時、最初のシーンが読み込まれる前に実行
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        // FPS設定
        Application.targetFrameRate = ApplicationSettings.TARGET_FPS;

        // DOTween初期化
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = true;

        //Debug.Log("RuntimeInitializeOnLoadMethod Initialize completed".WithColorTag(Color.red));
    }

    // @memo. 起動時、最初のシーンが読み込まれた後に実行
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AfterSceneLoad()
    {
        // @memo. ここでDontDestroyOnLoadのオブジェクトを生成
        //Debug.Log("RuntimeInitializeOnLoadMethod AfterSceneLoad completed".WithColorTag(Color.red));
    }
}
