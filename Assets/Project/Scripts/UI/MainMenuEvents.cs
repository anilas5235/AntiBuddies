using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenuEvents : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private Button _playButton;
        private Button _optionsButton;
        private Button _quitButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _playButton = _uiDocument.rootVisualElement.Q<Button>("Play");
            _optionsButton = _uiDocument.rootVisualElement.Q<Button>("Options");
            _quitButton = _uiDocument.rootVisualElement.Q<Button>("Quit");
        }

        private void OnEnable()
        {
            _playButton.clicked += OnPlayButtonClick;
            _optionsButton.clicked += OnOptionsButtonClick;
            _quitButton.clicked += OnExitButtonClick;
        }
        
        private void OnDisable()
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
            SceneManager.LoadScene("SampleScene");
        }
    }
}