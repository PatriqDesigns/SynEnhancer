using WindowsInput.Native;

namespace SynEnhancer
{
    public abstract class Action
    {
        protected VirtualKeyCode[] KeyCombination;

        protected Action()
        {
            KeyCombination = new VirtualKeyCode[0];
        }

        public void Execute()
        {
            Input.Keyboard.PressKeyCombination(KeyCombination);
        }
    }
}