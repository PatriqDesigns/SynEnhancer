using WindowsInput.Native;

namespace SynEnhancer
{
    public class SwitchWorkspace : Action
    {
        public SwitchWorkspace(bool left)
        {
            KeyCombination = new[]
            {
                VirtualKeyCode.LWIN,
                VirtualKeyCode.LCONTROL,
                (left ? VirtualKeyCode.LEFT : VirtualKeyCode.RIGHT),
            };
        }
    }
}