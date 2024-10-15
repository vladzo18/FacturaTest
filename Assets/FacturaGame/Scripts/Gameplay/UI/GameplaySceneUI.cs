using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FacturaGame.Scripts.Gameplay.UI
{
    public class GameplaySceneUI : MonoBehaviour
    {
        [SerializeField] private GameObject _loseWindow;
        [SerializeField] private GameObject _victoryWindow;
        [SerializeField] private List<Button> _restartButtons;

        public event Action OnRestartClick;
        
        private void Start()
        {
            foreach (var restartButton in _restartButtons)
            {
                restartButton.onClick.AddListener(() => OnRestartClick?.Invoke());
            }
        }

        private void OnDestroy()
        {
            foreach (var restartButton in _restartButtons)
            {
                restartButton.onClick.RemoveAllListeners();
            }
        }

        public void ShowLoseWindow()
        {
            _loseWindow.SetActive(true);
        }
        
        public void ShowVictoryWindow()
        {
            _victoryWindow.SetActive(true);
        }

        public void HideAll()
        {
            _loseWindow.SetActive(false);
            _victoryWindow.SetActive(false);
        }
    }
}