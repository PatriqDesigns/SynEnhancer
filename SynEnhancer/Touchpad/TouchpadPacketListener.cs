namespace SynEnhancer
{
    public class TouchpadPacketListener
    {
        private readonly MoveEventHandler _eventHandler;

        public TouchpadPacketListener()
        {
            _eventHandler = new MoveEventHandler();
        }

        public void OnPacket(Touchpad touchpad, Packet packet)
        {
            _eventHandler.Handle(touchpad, packet);
        }
    }
}