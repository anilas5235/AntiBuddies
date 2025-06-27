using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.ResourceSystem;
using Project.Scripts.ShopSystem;
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
        private Button _rerollButton;
        private Button _resumeButton;
        private ResourceManager _resourceManager;
        private Label _goldAmountLabel;
        private VisualElement _inventoryView;
        private Merger _merger;
        private Button _mergeButton;
        private int? _selectedIndex1;
        private int? _selectedIndex2;

        public event Action OnShopClosed;

        protected override void Awake()
        {
            base.Awake();
            _uiDocument = GetComponent<UIDocument>();
            _resourceManager = GlobalVariables.Instance.ResourceManager;

            // inventory view
            _inventoryView = _uiDocument.rootVisualElement.Q<VisualElement>("InventoryView");

            // initialize and subscribe to gold label
            _goldAmountLabel = _uiDocument.rootVisualElement.Q<Label>("GoldAmountLabel");
            OnGoldChanged();
            _resourceManager.OnGoldChange += OnGoldChanged;

            _buyButtons = _uiDocument.rootVisualElement.Query<Button>("Buy").ToList();
            for (int i = 0; i < _buyButtons.Count && i < Items.Length; i++)
            {
                int index = i;
                _buyButtons[i].clicked += () => Buy(index);
            }

            _rerollButton = _uiDocument.rootVisualElement.Q<Button>("RerollButton");
            _resumeButton = _uiDocument.rootVisualElement.Q<Button>("ResumeButton");
            if (_rerollButton != null)
                _rerollButton.clicked += () => {
                    ShopSystem.Shop.Instance.Reroll();
                    _rerollButton.text = $"Reroll ({ShopSystem.Shop.Instance.RerollCost})";
                };
            if (_resumeButton != null)
                _resumeButton.clicked += CloseShop;

            // Initialize merger and merge button
            _merger = UnityEngine.Object.FindFirstObjectByType<Merger>();
            _mergeButton = _uiDocument.rootVisualElement.Q<Button>("MergeButton");
            if (_mergeButton != null)
            {
                _mergeButton.SetEnabled(false);
                _mergeButton.clicked += OnMergeClicked;
            }

            _selectedIndex1 = null;
            _selectedIndex2 = null;

            Hide();
        }

        private void Start()
        {
            Data.Clear();
            for (int i = 0; i < Items.Length; i++)
                SetItem(i, Items[i]);
        }

        public void ClearItems() => Data.Clear();

        public void SetItem(int index, Item item)
        {
            if (index >= 0 && index < Items.Length)
            {
                Data.SetItem(index, item);
                Items[index] = item;
            }
        }

        private void Buy(int index)
        {
            if (index >= 0 && index < Items.Length)
            {
                var item = Items[index];
                if (item != null && ShopSystem.Shop.Instance.BuyItem(item))
                {
                    SetItem(index, null);
                    RefreshInventory();
                }
            }
        }

        public void Show()
        {
            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            if (_rerollButton != null)
                _rerollButton.text = $"Reroll ({ShopSystem.Shop.Instance.RerollCost})";
            ShopSystem.Shop.Instance.GenerateShopItems();
            RefreshInventory();
        }

        public void Hide() => _uiDocument.rootVisualElement.style.display = DisplayStyle.None;

        private void CloseShop()
        {
            Hide();
            OnShopClosed?.Invoke();
        }

        private void OnGoldChanged() => _goldAmountLabel.text = _resourceManager.Gold.ToString();

        private void RefreshInventory()
        {
            if (_inventoryView == null) return;
            _inventoryView.Clear();
            var inv = GlobalVariables.Instance.PlayerInventory;
            for (int i = 0; i < inv.MaxSize; i++)
            {
                var slot = new VisualElement();
                slot.style.flexGrow = 1;
                slot.style.flexShrink = 0;
                slot.RegisterCallback<GeometryChangedEvent>(evt => slot.style.height = evt.newRect.width);
                slot.AddToClassList("inventorySlot");

                // Register click for selection
                int index = i;
                slot.RegisterCallback<MouseDownEvent>(evt => { OnInventorySlotClicked(index); evt.StopPropagation(); });

                // Style based on selection and recipe availability
                // Remove previous state classes
                slot.RemoveFromClassList("outline");
                slot.RemoveFromClassList("selected");

                if (_selectedIndex1 == null && _selectedIndex2 == null)
                {
                    // No selection: outline items present in any recipe
                    if (i < inv.Items.Count && inv.Items[i] != null && _merger.Recipes.Any(r => r.IsValid(inv.Items[i], inv.Items[i])))
                    {
                        slot.AddToClassList("outline");
                    }
                }
                else if (_selectedIndex1 != null && _selectedIndex2 == null)
                {
                    // One selected
                    if (i == _selectedIndex1)
                        slot.AddToClassList("selected");
                    else if (i < inv.Items.Count && inv.Items[i] != null && _merger.Recipes.Any(r => r.IsValid(inv.Items[_selectedIndex1.Value], inv.Items[i])))
                        slot.AddToClassList("outline");
                }
                else
                {
                    // Two or more: mark selected
                    if (i == _selectedIndex1 || i == _selectedIndex2)
                        slot.AddToClassList("selected");
                }

                if (i < inv.Items.Count && inv.Items[i] != null)
                {
                    var img = new VisualElement();
                    img.style.backgroundImage = new StyleBackground(inv.Items[i].Icon);
                    img.AddToClassList("inventorySlotImage");
                    slot.Add(img);
                }
                _inventoryView.Add(slot);
            }
        }

        private void OnInventorySlotClicked(int index)
        {
            var inv = GlobalVariables.Instance.PlayerInventory;
            if (index < 0 || index >= inv.Items.Count) return;
            if (_selectedIndex1 == index)
                _selectedIndex1 = null;
            else if (_selectedIndex2 == index)
                _selectedIndex2 = null;
            else if (!_selectedIndex1.HasValue)
                _selectedIndex1 = index;
            else if (!_selectedIndex2.HasValue)
                _selectedIndex2 = index;
            // Enable merge only when two selected
            if (_mergeButton != null)
                _mergeButton.SetEnabled(_selectedIndex1.HasValue && _selectedIndex2.HasValue);
            RefreshInventory();
        }

        private void OnMergeClicked()
        {
            if (_selectedIndex1.HasValue && _selectedIndex2.HasValue)
            {
                var invItems = GlobalVariables.Instance.PlayerInventory.Items;
                var item1 = invItems[_selectedIndex1.Value];
                var item2 = invItems[_selectedIndex2.Value];
                _merger.Merge(item1, item2);
                _selectedIndex1 = null;
                _selectedIndex2 = null;
                if (_mergeButton != null)
                    _mergeButton.SetEnabled(false);
                RefreshInventory();
            }
        }
    }
}
