using System.Collections.Generic;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.UI
{
    /// <summary>
    /// Manages UI elements such as pause and end run menus, and controls UI state and input.
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        /// <summary>
        /// Reference to the pause menu GameObject.
        /// </summary>
        [SerializeField] private GameObject pauseMenu;

        /// <summary>
        /// Reference to the end run menu GameObject.
        /// </summary>
        [SerializeField] private GameObject endRun;

        /// <summary>
        /// Indicates if any UI is currently active.
        /// </summary>
        private bool _uiActive;

        /// <summary>
        /// List of currently active UI GameObjects.
        /// </summary>
        private readonly List<GameObject> _activeUIs = new();

        private void OnEnable()
        {
            UpdateUIState();
        }

        /// <summary>
        /// Handles the cancel input action (e.g., Escape key) to toggle the pause menu.
        /// </summary>
        /// <param name="inputValue">The input value from the input system.</param>
        public void OnCancel(InputValue inputValue)
        {
            if (inputValue.isPressed)
            {
                TogglePauseMenu();
            }
        }

        /// <summary>
        /// Shows the end run menu and adds it to the list of active UIs.
        /// </summary>
        public void ShowEndRunMenu()
        {
            if (!endRun)
            {
                Debug.LogWarning("End run menu is not assigned in the UIManager.");
                return;
            }

            endRun.SetActive(true);
            AddActiveUI(endRun);
        }

        /// <summary>
        /// Toggles the pause menu's visibility and updates the active UI list accordingly.
        /// </summary>
        public void TogglePauseMenu()
        {
            if (!pauseMenu)
            {
                Debug.LogWarning("Pause menu is not assigned in the UIManager.");
                return;
            }

            bool isActive = !pauseMenu.activeSelf;
            pauseMenu.SetActive(isActive);
            if (isActive)
            {
                AddActiveUI(pauseMenu);
            }
            else
            {
                RemoveActiveUI(pauseMenu);
            }
        }

        /// <summary>
        /// Adds a UI GameObject to the list of active UIs and updates the UI state.
        /// </summary>
        /// <param name="element">The UI GameObject to add.</param>
        private void AddActiveUI(GameObject element)
        {
            if (!element || _activeUIs.Contains(element)) return;
            _activeUIs.Add(element);
            UpdateUIState();
        }

        /// <summary>
        /// Removes a UI GameObject from the list of active UIs and updates the UI state.
        /// </summary>
        /// <param name="element">The UI GameObject to remove.</param>
        private void RemoveActiveUI(GameObject element)
        {
            if (!element || !_activeUIs.Contains(element)) return;
            _activeUIs.Remove(element);
            UpdateUIState();
        }

        /// <summary>
        /// Updates the UI state, including cursor visibility and time scale, based on whether any UI is active.
        /// </summary>
        private void UpdateUIState()
        {
            _uiActive = _activeUIs.Count > 0;
            // If any UI is active, show the cursor and pause the game; otherwise, hide the cursor and resume.
            if (_uiActive)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
        }
    }
}