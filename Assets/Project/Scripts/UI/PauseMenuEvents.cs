using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class PauseMenuEvents : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private Button _resumeButton;
        private Button _returnButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _resumeButton = _uiDocument.rootVisualElement.Q<Button>("Resume");
            _returnButton = _uiDocument.rootVisualElement.Q<Button>("Return");
        }

        private void OnEnable()
        {
            _resumeButton.clicked += OnResumeButtonClick;
            _returnButton.clicked += OnReturnButtonClick;
            Time.timeScale = 0f; // Pause the game
            _resumeButton.Focus(); // Set focus on the resume button
        }

        private void OnDisable()
        {
            _resumeButton.clicked -= OnResumeButtonClick;
            _returnButton.clicked -= OnReturnButtonClick;
            Time.timeScale = 1f; // Resume the game
        }

        private void OnResumeButtonClick()
        {
            UIManager.Instance.TogglePauseMenu();
        }

        private void OnReturnButtonClick()
        {
            // return to the main menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}