using SYNCTRLLib;

namespace SynEnhancer
{
    public class Packet
    {
        private readonly ISynPacketCtrl _packet;

        public Packet(ISynPacketCtrl packet)
        {
            _packet = packet;
        }

        public long GetCurrentNumberOfFingers()
        {
            return GetExtraFingerState() & 7;
        }

        public long GetInitialNumberOfFingers()
        {
            return GetExtraFingerState() >> 8;
        }

        public long GetTimestamp()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_TimeStamp);
        }

        public long GetW()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_W);
        }

        public long GetX()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_X);
        }

        public long GetY()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_Y);
        }

        public long GetZ()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_Z);
        }


        public long GetDeltaX()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_XDelta);
        }

        public long GetDeltaY()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_YDelta);
        }

        public long GetDeltaZ()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_ZDelta);
        }

        public bool IsAnyPhysicalButtonPressed()
        {
            return HasButtonState(SynButtonFlags.SF_ButtonAnyPhysical);
        }

        public bool IsFingerPresent()
        {
            return HasFingerState(SynFingerFlags.SF_FingerPresent);
        }

        public bool IsFingerInMotion()
        {
            return HasFingerState(SynFingerFlags.SF_FingerMotion);
        }

        public long GetFingerIndex()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_FingerIndex);
        }

        private long GetExtraFingerState()
        {
            return _packet.GetLongProperty(SynPacketProperty.SP_ExtraFingerState);
        }

        private bool HasButtonState(SynButtonFlags buttonFlags)
        {
            long state = _packet.GetLongProperty(SynPacketProperty.SP_ButtonState);
            return (state & (int) buttonFlags) > 0;
        }

        private bool HasFingerState(SynFingerFlags fingerFlags)
        {
            long state = _packet.GetLongProperty(SynPacketProperty.SP_FingerState);
            return (state & (int) fingerFlags) > 0;
        }
    };
}