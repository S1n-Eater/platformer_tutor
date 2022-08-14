using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private int _maxHealth;
        public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;

            _onDamage?.Invoke();
            if(_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        public void ApplyHeal(int healValue)
        {
            if (_health < _maxHealth)
            {
                _health += healValue;
                Debug.Log("¬осстановление здоровь€");
            }
            else
            {
                Debug.Log("«апас здоровь€ достиг максимума, лечение невозможно");
            }

            _onHeal?.Invoke();
        }
    }
}