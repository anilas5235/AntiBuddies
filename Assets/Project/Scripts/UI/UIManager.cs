using System.Collections.Generic;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject pauseMenu;
        private bool _uiActive;

        private readonly List<GameObject> _activeUIs = new();

        private void OnEnable()
        {
            UpdateUIState();
        }

        public void OnCancel(InputValue inputValue)
        {
            if (inputValue.isPressed)
            {
                TogglePauseMenu();
            }
        }

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

        private void AddActiveUI(GameObject element)
        {
            if (!element || _activeUIs.Contains(element)) return;
            _activeUIs.Add(element);
            UpdateUIState();
        }

        private void RemoveActiveUI(GameObject element)
        {
            if (!element || !_activeUIs.Contains(element)) return;
            _activeUIs.Remove(element);
            UpdateUIState();
        }

        private void UpdateUIState()
        {
            _uiActive = _activeUIs.Count > 0;
            if (_uiActive)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}