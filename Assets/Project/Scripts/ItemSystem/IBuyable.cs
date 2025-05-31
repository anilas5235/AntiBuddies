using UnityEngine;

namespace Project.Scripts.ItemSystem
{
    public interface IBuyable
    {
        int Cost { get; }
        string Description { get; }
        Sprite Icon { get; }
        Color Color { get;}
        void Buy();
    }
}