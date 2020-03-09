using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        public GameObject playerPrefab;

        public float maxHealth;
        public float currentHealth;
    }
}
