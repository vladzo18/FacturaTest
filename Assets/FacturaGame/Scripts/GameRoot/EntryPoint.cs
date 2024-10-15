using System.Collections;
using FacturaGame.Scripts.Gameplay.Root;
using FacturaGame.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FacturaGame.Scripts.GameRoot
{
    public class EntryPoint : MonoBehaviour
    {
        
        private void Awake()
        {
            AutostartGame();
        }

        private void AutostartGame()
        {
            SetBaseApplicationSettings();
            LaunchGame();
        }

        private static void SetBaseApplicationSettings()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void LaunchGame()
        {
#if UNITY_EDITOR
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName != SceneNames.BOOT)
                return;
#endif
            CoroutineRunner.RunSelfStopCoroutine(LoadAndStartGameplay());
        }
        
        private IEnumerator LoadAndStartGameplay()
        {
            yield return SafeLoadScene(SceneNames.GAMEPLAY);

            GameplayEntryPoint entryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            entryPoint.Enter();
        }

        private IEnumerator SafeLoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(SceneNames.EMPTY);
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
