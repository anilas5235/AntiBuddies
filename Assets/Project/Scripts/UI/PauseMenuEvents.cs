using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class PauseMenuEvents : AbstractUIEvents
    {
        private Button _resumeButton;
        private Button _returnButton;

        protected override void UpdateUiRefs(VisualElement root)
        {
            _resumeButton = root.Q<Button>("Resume");
            _returnButton = root.Q<Button>("Return");
        }

        protected override void SubscribeEvents()
        {
            _resumeButton.clicked += OnResumeButtonClick;
            _returnButton.clicked += OnReturnButtonClick;
            _resumeButton.Focus();
        }

        protected override void UnsubscribeEvents()
        {
            _resumeButton.clicked -= OnResumeButtonClick;
            _returnButton.clicked -= OnReturnButtonClick;
        }

        private void OnResumeButtonClick()
        {
            UIManager.Instance.TogglePauseMenu();
        }

        private void OnReturnButtonClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}