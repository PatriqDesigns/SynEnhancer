namespace SynEnhancer
{
    public class TouchpadPacketListener
    {
        private readonly MoveEventHandler _moveEventHandler;

        public TouchpadPacketListener()
        {
            _moveEventHandler = new MoveEventHandler();
        }

        public void OnPacket(Touchpad touchpad, Packet packet)
        {
            _moveEventHandler.Handle(touchpad, packet);
        }
    }
}