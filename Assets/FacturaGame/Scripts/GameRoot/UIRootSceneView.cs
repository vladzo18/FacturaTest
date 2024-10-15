using UnityEngine;

namespace FacturaGame.Scripts.GameRoot
{
    public class UIRootSceneView : MonoBehaviour
    {
        [SerializeField] private Transform _contentContainer;
        
        public void AttachSceneUI(GameObject sceneUI)
        {
            sceneUI.transform.SetParent(_contentContainer, false);
        }

        public void ClearSceneUI()
        {
            int childCount = _contentContainer.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(_contentContainer.GetChild(i).gameObject);
            }
        }
        
    }
}
