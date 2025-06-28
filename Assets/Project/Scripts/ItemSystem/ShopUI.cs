using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.ResourceSystem;
using Project.Scripts.ShopSystem;
using Project.Scripts.UI;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.ItemSystem
{
    [RequireComponent(typeof(UIDocument)), DefaultExecutionOrder(0)]
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
        private bool _isShopOpen;

        public bool IsShopOpen => _isShopOpen;

        public event Action OnShopClosed;

        protected override void Awake()
        {
            base.Awake();
            _uiDocument = GetComponent<UIDocument>();
        }


        private void OnEnable()
        {
            // inventory view
            _inventoryView = _uiDocument.rootVisualElement.Q<VisualElement>("InventoryView");
            _buyButtons = _uiDocument.rootVisualElement.Query<Button>("Buy").ToList();
            for (int i = 0; i < _buyButtons.Count && i < Items.Length; i++)
            {
                int index = i;
                _buyButtons[i].clicked += () => Buy(index);
            }

            _rerollButton = _uiDocument.rootVisualElement.Q<Button>("RerollButton");
            _resumeButton = _uiDocument.rootVisualElement.Q<Button>("ResumeButton");
            if (_rerollButton != null)
                _rerollButton.clicked += () =>
                {
                    Shop.Instance.Reroll();
                    _rerollButton.text = $"Reroll ({Shop.Instance.RerollCost})";
                };
            if (_resumeButton != null)
                _resumeButton.clicked += UIManager.Instance.ToggleShop;

            // Initialize merger and merge button
            _merger = FindFirstObjectByType<Merger>();
            _mergeButton = _uiDocument.rootVisualElement.Q<Button>("MergeButton");
            if (_mergeButton != null)
            {
                _mergeButton.SetEnabled(false);
                _mergeButton.clicked += OnMergeClicked;
            }

            _selectedIndex1 = null;
            _selectedIndex2 = null;

            Hide();

            _resourceManager = GlobalVariables.Instance.ResourceManager;
            // initialize and subscribe to gold label
            _resourceManager.OnGoldChange += OnGoldChanged;
            OnGoldChanged();
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
                Item item = Items[index];
                if (item && Shop.Instance.BuyItem(item))
                {
                    SetItem(index, null);
                    RefreshInventory();
                }
            }
        }

        private void Show()
        {
            _isShopOpen = true;
            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            if (_rerollButton != null)
                _rerollButton.text = $"Reroll ({Shop.Instance.RerollCost})";
            Shop.Instance.GenerateShopItems();
            RefreshInventory();
        }

        private void Hide()
        {
            _isShopOpen = false;
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        public void ToggleShop()
        {
            if (_isShopOpen)
            {
                Hide();
                Shop.Instance.OnShopClose();
                OnShopClosed?.Invoke();
            }
            else
            {
                Show();
            }
        }

        private void OnGoldChanged() => Data.gold = _resourceManager.Gold;

        private void RefreshInventory()
        {
            if (_inventoryView == null) return;
            _inventoryView.Clear();
            Inventory inv = GlobalVariables.Instance.PlayerInventory;
            for (int i = 0; i < inv.MaxSize; i++)
            {
                VisualElement slot = new()
                {
                    style =
                    {
                        flexGrow = 1,
                        flexShrink = 0
                    }
                };
                slot.RegisterCallback<GeometryChangedEvent>(evt => slot.style.height = evt.newRect.width);
                slot.AddToClassList("inventorySlot");

                // Register click for selection
                int index = i;
                slot.RegisterCallback<MouseDownEvent>(evt =>
                {
                    OnInventorySlotClicked(index);
                    evt.StopPropagation();
                });

                // Style based on selection and recipe availability
                // Remove previous state classes
                slot.RemoveFromClassList("outline");
                slot.RemoveFromClassList("selected");

                bool isSelected = false;
                if (_selectedIndex1 == null && _selectedIndex2 == null)
                {
                    // No selection: outline items if they can be merged
                    if (i < inv.Items.Count && inv.Items[i] != null &&
                        _merger.Recipes.Any(r => r.IsPresent(inv.Items[i])))
                    {
                        for (int j = 0; j < inv.Items.Count; j++)
                        {
                            if (i == j) continue;
                            if (inv.Items[j] != null && _merger.Recipes.Any(r => r.IsValid(inv.Items[i], inv.Items[j])))
                            {
                                slot.AddToClassList("outline");
                                break;
                            }
                        }
                    }
                }
                else if (_selectedIndex1 != null && _selectedIndex2 == null)
                {
                    // One selected item: mark selected and outline valid merges
                    if (i == _selectedIndex1) {
                        slot.AddToClassList("selected");
                        isSelected = true;
                    }
                    else if (i < inv.Items.Count && inv.Items[i] != null &&
                             _merger.Recipes.Any(r => r.IsValid(inv.Items[_selectedIndex1.Value], inv.Items[i])))
                        slot.AddToClassList("outline");
                }
                else
                {
                    // Two or more: mark selected
                    if (i == _selectedIndex1 || i == _selectedIndex2) {
                        slot.AddToClassList("selected");
                        isSelected = true;
                    }
                }

                // Set background color based on rarity if not selected
                if (i < inv.Items.Count && inv.Items[i] != null && !isSelected)
                {
                    slot.style.backgroundColor = inv.Items[i].Color;
                } else {
                    slot.style.backgroundColor = StyleKeyword.Null;
                }

                if (i < inv.Items.Count && inv.Items[i] != null)
                {
                    VisualElement img = new();
                    img.style.backgroundImage = new StyleBackground(inv.Items[i].Icon);
                    img.AddToClassList("inventorySlotImage");
                    slot.Add(img);
                }

                _inventoryView.Add(slot);
            }
        }

        private void OnInventorySlotClicked(int index)
        {
            Inventory inv = GlobalVariables.Instance.PlayerInventory;
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
                IReadOnlyList<Item> invItems = GlobalVariables.Instance.PlayerInventory.Items;
                Item item1 = invItems[_selectedIndex1.Value];
                Item item2 = invItems[_selectedIndex2.Value];
                _merger.Merge(item1, item2);
                _selectedIndex1 = null;
                _selectedIndex2 = null;
                if (_mergeButton != null)
                    _mergeButton.SetEnabled(false);
                RefreshInventory();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Data.Clear();
        }
    }
}