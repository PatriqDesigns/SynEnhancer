using System.Collections.Generic;

namespace SynEnhancer
{
    public class MoveEventHandler
    {
        private readonly List<IMoveEvent> _moveEvents;
        private readonly MoveState _moveState;

        public MoveEventHandler()
        {
            _moveState = new MoveState();
            _moveEvents = new List<IMoveEvent>
            {
                new ThreeFingerScroll()
            };
        }

        public void Handle(Touchpad touchpad, Packet packet)
        {
            _moveState.Update(packet);
            if (packet.IsFingerPresent())
            {
                foreach (var moveEvent in _moveEvents)
                    moveEvent.FingerPresent(touchpad, packet, _moveState);
            }
            else
            {
                foreach (var moveEvent in _moveEvents)
                    moveEvent.FingerNotPresent(touchpad, packet, _moveState);
                _moveState.Clear();
            }
            _moveState.UpdateEnd(packet);
        }
    }
}