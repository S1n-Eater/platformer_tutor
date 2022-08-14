using UnityEngine;


namespace Scripts.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _ObjectToDestroy;

        public void DestroyObject()
        {
            Destroy(_ObjectToDestroy);
        }
    }
}