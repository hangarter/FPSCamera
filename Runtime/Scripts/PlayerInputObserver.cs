using UnityEngine;
using UnityEngine.InputSystem;

namespace fpscamera
{
    public class PlayerInputObserver : MonoBehaviour
    {
        public PlayerInputEvent OnLookEvent;
        public PlayerInputEvent OnMoveEvent;

        private void OnLook(InputValue value)
        {
            OnLookEvent.TriggerInputEvent(value);
        }

        private void OnMove(InputValue value)
        {
            OnMoveEvent.TriggerInputEvent(value);
        }

    }
}