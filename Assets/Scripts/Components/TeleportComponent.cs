using UnityEngine;

namespace Scripts.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destPosition;

        public void Teleport(GameObject target)
        {
            target.transform.position = _destPosition.position;
        }
    }
}