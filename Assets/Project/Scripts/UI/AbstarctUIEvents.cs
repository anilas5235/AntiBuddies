using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.UI
{
    /// <summary>
    /// Abstract base class for UI event handling components.
    /// Provides a template for subscribing and unsubscribing UI events and updating UI references.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public abstract class AbstractUIEvents : MonoBehaviour
    {
        /// <summary>
        /// Reference to the attached UIDocument component.
        /// </summary>
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

        /// <summary>
        /// Subscribes to relevant UI events.
        /// </summary>
        protected abstract void SubscribeEvents();

        /// <summary>
        /// Updates references to UI elements from the root visual element.
        /// </summary>
        /// <param name="root">The root visual element of the UI.</param>
        protected abstract void UpdateUiRefs(VisualElement root);

        protected virtual void OnDisable()
        {
            UnsubscribeEvents();
        }

        /// <summary>
        /// Unsubscribes from UI events.
        /// </summary>
        protected abstract void UnsubscribeEvents();
    }
}