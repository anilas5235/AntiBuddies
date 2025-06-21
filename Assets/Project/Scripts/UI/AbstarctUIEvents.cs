using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class AbstractUIEvents : MonoBehaviour
    {
        private UIDocument _uiDocument;
        
        protected virtual void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
        }
        
        protected virtual void OnEnable()
        {
            UpdateUiRefs(_uiDocument.rootVisualElement);
            SubscribeEvents();
        }

        protected abstract void SubscribeEvents();
        protected abstract void UpdateUiRefs(VisualElement root);

        protected virtual void OnDisable()
        {
            UnsubscribeEvents();
        }
        protected abstract void UnsubscribeEvents();
    }
}