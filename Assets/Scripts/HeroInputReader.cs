using UnityEngine;
using UnityEngine.InputSystem;


namespace Scripts
{


    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        private HeroInputActions _inputActions;
        private void Awake()
        {
            _inputActions = new HeroInputActions();
            _inputActions.Hero.HorizontalMovement.performed += OnHorizontalMovement;
            _inputActions.Hero.HorizontalMovement.canceled += OnHorizontalMovement;

            _inputActions.Hero.SaySomething.performed += OnSaySomething;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        public void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();

            _hero.SetDirection(direction);
        }

        public void OnSaySomething(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.SaySomething();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Interact();
            }
        }
    }
}