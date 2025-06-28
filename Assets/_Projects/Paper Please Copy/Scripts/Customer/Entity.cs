using UnityEngine;

namespace com.Kuwiku
{
    public enum EntityType
    {
        Customer,
        Terrorist
    }

    public class Entity : MonoBehaviour
    {
        [SerializeField] protected int health;
        [SerializeField] protected EntityType type;

        protected virtual void Die()
        {
            Debug.Log($"Entity {gameObject.name} is Dying!");
        }

        public bool IsAlive() { return health > 0; }
        public EntityType GetEntityType() { return type; }
    }
}
