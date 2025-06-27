using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    /// <summary>
    /// Handles end-of-run UI events such as restarting the game or returning to the main menu.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class RunEndEvents : AbstractUIEvents
    {
        /// <summary>
        /// Reference to the Restart button.
        /// </summary>
        private Button _restartButton;

        /// <summary>
        /// Reference to the Menu button.
        /// </summary>
        private Button _menuButton;

        /// <inheritdoc/>
        protected override void UpdateUiRefs(VisualElement root)
        {
            _restartButton = root.Q<Button>("Restart");
            _menuButton = root.Q<Button>("Menu");
        }

        /// <inheritdoc/>
        protected override void SubscribeEvents()
        {
            _restartButton.clicked += OnRestartButtonClick;
            _menuButton.clicked += OnMenuButtonClick;
            _restartButton.Focus();
        }

        /// <inheritdoc/>
        protected override void UnsubscribeEvents()
        {
            _restartButton.clicked -= OnRestartButtonClick;
            _menuButton.clicked -= OnMenuButtonClick;
        }

        /// <summary>
        /// Handles the Restart button click event. Loads the game scene.
        /// </summary>
        private void OnRestartButtonClick()
        {
            SceneManager.LoadScene("GameScene");
        }

        /// <summary>
        /// Handles the Menu button click event. Loads the main menu scene.
        /// </summary>
        private void OnMenuButtonClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}