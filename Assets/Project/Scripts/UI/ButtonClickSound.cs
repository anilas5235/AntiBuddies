using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class ButtonClickSound : AbstractUIEvents
    {
        [SerializeField] private AudioSource audioSource;
        private List<Button> _buttons;

        protected override void UpdateUiRefs(VisualElement root)
        {
            _buttons = root.Query<Button>().ToList();
        }

        protected override void SubscribeEvents()
        {
            if (_buttons == null) return;
            foreach (Button button in _buttons)
            {
                button.clicked += OnButtonClick;
            }
        }

        protected override void UnsubscribeEvents()
        {
            if (_buttons == null) return;
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