namespace SynEnhancer
{
    public interface IMoveEvent
    {
        bool FingerPresent(Touchpad touchpad, Packet packet, MoveState globalMoveState);
        bool FingerNotPresent(Touchpad touchpad, Packet packet, MoveState globalMoveState);
    }
}