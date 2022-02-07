using UnityEngine;
using UnityEngine.InputSystem;

namespace fpscamera
{
    public class PlayerInputObserver : MonoBehaviour
    {
        public PlayerInputEvent OnLookEvent;

        private void OnLook(InputValue value)
        {
            OnLookEvent.TriggerInputEvent(value);
        }

        private void OnMove(InputValue value)
        {
            Debug.Log("On Move");
            //onMove.TriggerInputEvent(inputAction);         
        }

    }
}