using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    public class RunEndEvents : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private Button _restartButton;
        private Button _menuButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _restartButton = _uiDocument.rootVisualElement.Q<Button>("Restart");
            _menuButton = _uiDocument.rootVisualElement.Q<Button>("Menu");
        }

        private void OnEnable()
        {
            _restartButton.clicked += OnRestartButtonClick;
            _menuButton.clicked += OnMenuButtonClick;
            _restartButton.Focus(); // Set focus on the restart button
        }

        private void OnDisable()
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
            // Return to the main menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}
