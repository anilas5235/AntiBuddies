using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenuEvents : AbstractUIEvents
    {
        private Button _playButton;
        private Button _optionsButton;
        private Button _quitButton;

        protected override void UpdateUiRefs(VisualElement root)
        {
            _playButton = root.Q<Button>("Play");
            _optionsButton = root.Q<Button>("Options");
            _quitButton = root.Q<Button>("Quit");
        }

        protected override void SubscribeEvents()
        {
            _playButton.clicked += OnPlayButtonClick;
            _optionsButton.clicked += OnOptionsButtonClick;
            _quitButton.clicked += OnExitButtonClick;
            _playButton.Focus();
        }

        protected override void UnsubscribeEvents()
        {
            _playButton.clicked -= OnPlayButtonClick;
            _optionsButton.clicked -= OnOptionsButtonClick;
            _quitButton.clicked -= OnExitButtonClick;
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
        }

        private void OnOptionsButtonClick()
        {
            throw new NotImplementedException();
        }

        private void OnPlayButtonClick()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}