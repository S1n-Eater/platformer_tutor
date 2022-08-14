using UnityEngine;

namespace Scripts.Components
{
    public class HealComponent : MonoBehaviour
    {
        [SerializeField] private int _heal;

        public void ApplyHeal(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            
            

            if (healthComponent != null)
            {
                healthComponent.ApplyHeal(_heal);
            }
        }

        public void TurnOnTrigger()
        {
            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }
}