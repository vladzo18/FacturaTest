using FacturaGame.Scripts.GameRoot;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace FacturaGame.Editor
{
    [InitializeOnLoad]
    public class BootSceneAutoLoader
    {
        private static string BootScenePath = "Assets/FacturaGame/Scenes/Boot_Scene.unity";
        
        static BootSceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }
        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (EditorSceneManager.GetActiveScene().name != SceneNames.BOOT)
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(BootScenePath);
                        EditorApplication.isPlaying = true;
                    }
                    else
                    {
                        EditorApplication.isPlaying = false;
                    }
                }
            }
        }
    }
}