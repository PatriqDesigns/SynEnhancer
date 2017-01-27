namespace SynEnhancer
{
    public class MoveState
    {
        private long _initialX;
        private long _initialY;
        private long _initialTimestamp;
        private long _maxZ;

        public void Update(Packet packet)
        {
            if (packet.GetCurrentNumberOfFingers() > 0)
            {
                if (_initialTimestamp == 0)
                {
                    _initialX = packet.GetX();
                    _initialY = packet.GetY();
                    _initialTimestamp = packet.GetTimestamp();
                }
                _maxZ = (packet.GetZ() > _maxZ) ? packet.GetZ() : _maxZ;
            }
        }

        public void UpdateEnd(Packet packet)
        {
        }

        public void Clear()
        {
            _initialX = 0;
            _initialY = 0;
            _initialTimestamp = 0;
            _maxZ = 0;
        }

        public long GetInitialX()
        {
            return _initialX;
        }

        public long GetInitialY()
        {
            return _initialY;
        }

        public long GetInitialTimestamp()
        {
            return _initialTimestamp;
        }

        public long GetMaxZ()
        {
            return _maxZ;
        }
    }
}