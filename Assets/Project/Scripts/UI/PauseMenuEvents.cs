using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    /// <summary>
    /// Handles pause menu button events such as resume and return to main menu.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public class PauseMenuEvents : AbstractUIEvents
    {
        /// <summary>
        /// Reference to the Resume button.
        /// </summary>
        private Button _resumeButton;

        /// <summary>
        /// Reference to the Return button.
        /// </summary>
        private Button _returnButton;

        /// <inheritdoc/>
        protected override void UpdateUiRefs(VisualElement root)
        {
            _resumeButton = root.Q<Button>("Resume");
            _returnButton = root.Q<Button>("Return");
        }

        /// <inheritdoc/>
        protected override void SubscribeEvents()
        {
            _resumeButton.clicked += OnResumeButtonClick;
            _returnButton.clicked += OnReturnButtonClick;
            _resumeButton.Focus();
        }

        /// <inheritdoc/>
        protected override void UnsubscribeEvents()
        {
            _resumeButton.clicked -= OnResumeButtonClick;
            _returnButton.clicked -= OnReturnButtonClick;
        }

        /// <summary>
        /// Handles the Resume button click event. Toggles the pause menu.
        /// </summary>
        private void OnResumeButtonClick()
        {
            UIManager.Instance.TogglePauseMenu();
        }

        /// <summary>
        /// Handles the Return button click event. Loads the main menu scene.
        /// </summary>
        private void OnReturnButtonClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}