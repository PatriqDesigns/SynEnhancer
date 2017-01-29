using System;

namespace SynEnhancer
{
    public class ThreeFingerScroll : IMoveEvent
    {
        // Number of fingers that will activate this scroll event
        private const int Fingers = 3;

        // The percentage of distance travaled with the three fingers that will trigger the X axis swipe.
        private const float XTriggerFactor = 0.1f;

        // The percentage of distance travaled with the three fingers that will trigger the Y axis swipe.
        private const float YTriggerFactor = 0.2f;

        // Time cooldown between actions. Way to prevent action bursting.
        private const long ActionCooldown = 150;

        // Private move state. Will much work like the public one they give us, but we can can change this one.
        private readonly MoveState _moveState;

        // Last action timestamp.
        private long _lastActionTimestamp;

        // If there was motion or not. If there wasn't, FingerPresent didn't do nothing, and therefore shouldn't the FingerNotPresent.
        private bool _wasEvent;

        public ThreeFingerScroll()
        {
            _moveState = new MoveState();
        }

        public bool FingerPresent(Touchpad touchpad, Packet packet, MoveState globalMoveState)
        {
            if (packet.GetTimestamp() - _lastActionTimestamp < ActionCooldown) return false;
            if (packet.GetCurrentNumberOfFingers() != Fingers) return false;
            if (!packet.IsFingerInMotion()) return false;

            // Try to aquire the touchpad.
            touchpad.Acquire(true);
            if (!touchpad.IsAquired()) return false;

            // There was motion, so start counting the move state, and let us know we did some work.
            _wasEvent = true;
            _moveState.Update(packet);

            // Check if the X axis was triggered.
            var xDistance = packet.GetX() - _moveState.GetInitialX();
            var xFactor = Math.Abs(xDistance / (1f * (touchpad.GetSensorHighX() - touchpad.GetSensorLowX())));
            var wasXTriggered = xFactor > XTriggerFactor;

            // Check if the Y axis was triggered.
            var yDistance = packet.GetY() - _moveState.GetInitialY();
            var yFactor = Math.Abs(yDistance / (1f * (touchpad.GetSensorHighY() - touchpad.GetSensorLowY())));
            var wasYTriggered = yFactor > YTriggerFactor;

            // If any of them was triggered, start counting again.
            if (wasXTriggered || wasYTriggered) _moveState.Clear();

            // If both were choose the one with the highest distance travaled.
            if (wasXTriggered && wasYTriggered)
            {
                wasXTriggered = Math.Abs(xDistance) > Math.Abs(yDistance);
                wasYTriggered = !wasXTriggered;
            }

            // Check every possible action.
            if (wasXTriggered)
            {
                var left = xDistance > 0;
                (new SwitchWorkspace(left)).Execute();
                // Since a action was executed, and we don't want to be firing actions like crazy, update the last action timestamp.
                _lastActionTimestamp = packet.GetTimestamp();
            }
            if (wasYTriggered)
            {
                var up = yDistance > 0;
                (new WorkspaceManager(up)).Execute();
                // Since a action was executed, and we don't want to be firing actions like crazy, update the last action timestamp.
                _lastActionTimestamp = packet.GetTimestamp();
            }

            // End the update.
            _moveState.UpdateEnd(packet);
            return true;
        }

        public bool FingerNotPresent(Touchpad touchpad, Packet packet, MoveState globalMoveState)
        {
            // Clear the move state.
            _moveState.Clear();
            if (!_wasEvent) return false;
            _wasEvent = false;
            touchpad.Acquire(false);
            return true;
        }
    }
}