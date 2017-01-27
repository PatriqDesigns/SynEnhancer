using WindowsInput.Native;

namespace SynEnhancer
{
    public class WorkspaceManager : Action
    {
        public WorkspaceManager(bool up)
        {
            if (up)
            {
                KeyCombination = new[]
                {
                    VirtualKeyCode.LWIN,
                    VirtualKeyCode.TAB,
                };
            }
            else
            {
                KeyCombination = new[]
                {
                    VirtualKeyCode.ESCAPE,
                };
            }
        }
    }
}