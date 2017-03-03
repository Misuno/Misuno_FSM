using System;
using UnityEngine.UI;

namespace Misuno
{
    public class StateTransitionButtonPressed : StateTransition
    {
        private Button button;
        private bool pressed;

        public StateTransitionButtonPressed(State from, State to, Button button)
            : base(from, to)
        {
            this.button = button;
            button.onClick.AddListener(ClickCallback);
        }

        public override bool Check()
        {
            return pressed;
        }

        public override void Reset()
        {
            pressed = false;
        }

        private void ClickCallback()
        {
            pressed = true;
        }

        public override void Clear()
        {
            button.onClick.RemoveListener(ClickCallback);
        }
    }
}

