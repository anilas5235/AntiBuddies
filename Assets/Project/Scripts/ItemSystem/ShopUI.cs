using System;
using System.Collections.Generic;
using Project.Scripts.ResourceSystem;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.ItemSystem
{
    [RequireComponent(typeof(UIDocument))]
    public class ShopUI : Singleton<ShopUI>
    {
        [SerializeField] private ShopData Data;
        [SerializeField] private Item[] Items = new Item[4];
        
        private UIDocument _uiDocument;
        private List<Button> _buyButtons;

        protected override void Awake()
        {
            base.Awake();
            _uiDocument = GetComponent<UIDocument>();

            _buyButtons = _uiDocument.rootVisualElement.Query<Button>("Buy").ToList();
            for (int i = 0; i < _buyButtons.Count && i < Items.Length; i++)
            {
                int index = i; // Capture index for closure
                _buyButtons[i].clicked += () => Buy(index);
            }
        }

        private void Start()
        {
            Data.Clear();
            for (int i = 0; i < Items.Length; i++)
            {
                SetItem(i, Items[i]);
            }
        }

        public void ClearItems()
        {
            Data.Clear();
        }

        public void SetItem(int index, Item item)
        {
            if (IsValidIndex(index))
            {
                Data.SetItem(index, item);
            }
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < Items.Length;
        }

        private void Buy(int index)
        {
            if (IsValidIndex(index))
            {
                Data.BuyItem(index);
            }
        }
    }
}
