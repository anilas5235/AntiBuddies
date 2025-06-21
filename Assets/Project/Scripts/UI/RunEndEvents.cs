using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    public class RunEndEvents : AbstractUIEvents
    {
        private Button _restartButton;
        private Button _menuButton;

        protected override void UpdateUiRefs(VisualElement root)
        {
            _restartButton = root.Q<Button>("Restart");
            _menuButton = root.Q<Button>("Menu");
        }

        protected override void SubscribeEvents()
        {
            _restartButton.clicked += OnRestartButtonClick;
            _menuButton.clicked += OnMenuButtonClick;
            _restartButton.Focus();
        }

        protected override void UnsubscribeEvents()
        {
            _restartButton.clicked -= OnRestartButtonClick;
            _menuButton.clicked -= OnMenuButtonClick;
        }

        private void OnRestartButtonClick()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void OnMenuButtonClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
