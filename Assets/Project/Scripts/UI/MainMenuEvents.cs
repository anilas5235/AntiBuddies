using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    /// <summary>
    /// Handles main menu button events such as play, options, and quit.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public class MainMenuEvents : AbstractUIEvents
    {
        /// <summary>
        /// Reference to the Play button.
        /// </summary>
        private Button _playButton;
        /// <summary>
        /// Reference to the Options button.
        /// </summary>
        private Button _optionsButton;
        /// <summary>
        /// Reference to the Quit button.
        /// </summary>
        private Button _quitButton;

        /// <inheritdoc/>
        protected override void UpdateUiRefs(VisualElement root)
        {
            _playButton = root.Q<Button>("Play");
            _optionsButton = root.Q<Button>("Options");
            _quitButton = root.Q<Button>("Quit");
        }

        /// <inheritdoc/>
        protected override void SubscribeEvents()
        {
            _playButton.clicked += OnPlayButtonClick;
            _optionsButton.clicked += OnOptionsButtonClick;
            _quitButton.clicked += OnExitButtonClick;
            _playButton.Focus();
        }

        /// <inheritdoc/>
        protected override void UnsubscribeEvents()
        {
            _playButton.clicked -= OnPlayButtonClick;
            _optionsButton.clicked -= OnOptionsButtonClick;
            _quitButton.clicked -= OnExitButtonClick;
        }

        /// <summary>
        /// Handles the Quit button click event. Exits the application.
        /// </summary>
        private void OnExitButtonClick()
        {
            Application.Quit();
        }

        /// <summary>
        /// Handles the Options button click event. Throws not implemented exception.
        /// </summary>
        private void OnOptionsButtonClick()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Play button click event. Loads the game scene.
        /// </summary>
        private void OnPlayButtonClick()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}