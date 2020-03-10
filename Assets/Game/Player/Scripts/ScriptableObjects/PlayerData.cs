using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        public GameObject playerPrefab;

        [SerializeField]
        private float maxHealth;
        public float MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        [SerializeField]
        private float currentHealth;
        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }
    }
}