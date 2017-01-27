using WindowsInput;
using WindowsInput.Native;

namespace SynEnhancer
{
    public static class Input
    {
        public static readonly InputSimulator InputSimulator = new InputSimulator();


        public static class Keyboard
        {
            private static readonly IKeyboardSimulator KeyboardSimulator = InputSimulator.Keyboard;

            public static void HoldKey(VirtualKeyCode virtualKey)
            {
                KeyboardSimulator.KeyDown(virtualKey);
            }

            public static void PressKey(VirtualKeyCode virtualKey)
            {
                KeyboardSimulator.KeyPress(virtualKey);
            }

            public static void PressKeyCombination(VirtualKeyCode[] virtualKeys)
            {
                foreach (var virtualKey in virtualKeys)
                {
                    HoldKey(virtualKey);
                }
                foreach (var virtualKey in virtualKeys)
                {
                    ReleaseKey(virtualKey);
                }
            }

            public static void ReleaseKey(VirtualKeyCode virtualKey)
            {
                KeyboardSimulator.KeyUp(virtualKey);
            }
        }
    }
}