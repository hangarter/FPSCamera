using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace fpscamera
{
    [CreateAssetMenu(fileName = "OnPlayerInputEvent", menuName = "Player Input/Player Input Event", order = 1)]
    public class PlayerInputEvent : ScriptableObject
    {
        public delegate void PlayerInputAction(InputValue inputValue);

        public event PlayerInputAction OnPlayerInput;

        public void TriggerInputEvent(InputValue inputValue)
        {
            OnPlayerInput?.Invoke(inputValue);
        }
    }

}