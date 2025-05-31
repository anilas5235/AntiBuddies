using UnityEngine;

namespace Project.Scripts.Spawning
{
    public static class SpawnUtils
    {
        public static Vector2 RandomPointInCircle(Vector2 center, float radius)
        {
            return Random.insideUnitCircle * radius + center;
        }
        
        public static Vector2 RandomPointInSquare(Vector2 center, float size)
        {
            return RandomPointInRect(center, new Vector2(size, size));
        }
        
        public static Vector2 RandomPointInRect(Vector2 center, Vector2 size)
        {
            return center + new Vector2(
                Random.Range(-size.x / 2f, size.x / 2f),
                Random.Range(-size.y / 2f, size.y / 2f)
            );
        }
    }
}