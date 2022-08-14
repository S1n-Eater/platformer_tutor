using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _actiont;

        public void Interact()
        {
            _actiont?.Invoke();
        }
    }
}