using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class ButtonClickSound : MonoBehaviour

    {
        [SerializeField] private AudioSource audioSource;
        private UIDocument _uiDocument;
        private List<Button> _buttons;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _buttons = _uiDocument.rootVisualElement.Query<Button>().ToList();
        }

        private void OnEnable()
        {
            foreach (Button button in _buttons)
            {
                button.clicked += OnButtonClick;
            }
        }

        private void OnDisable()
        {
            foreach (Button button in _buttons)
            {
                button.clicked -= OnButtonClick;
            }
        }

        private void OnButtonClick()
        {
            audioSource.Play();
        }
    }
}