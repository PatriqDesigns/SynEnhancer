using System;
using System.Threading;
using Microsoft.Win32;
using SYNCTRLLib;

namespace SynEnhancer
{
    public class Touchpad
    {
        // Touchpad variables.
        private SynDeviceCtrl _device;

        private SynPacketCtrl _packet;
        private bool _aquired;

        // The power mode changed handler.
        private readonly PowerModeChangedEventHandler _powerModeChangedEventHandler;

        public delegate void OnPacketEventHandler(Touchpad touchpad, Packet packet);

        public event OnPacketEventHandler OnPacket;

        public Touchpad()
        {
            // Initialize touchpad.
            if(!InitializeSynapticsTouchpad()) return;

            // Re-initialize on Resume.
            // Since during power change events (sleep/resume), something happens
            // with the touchpad where events are no longer sent.
            // To handle this we trash out the last device, and re-initialize it again, registrying new events.
            // Also it seems like if we do it right away it doesn't work, therefore the Sleep(100).
            _powerModeChangedEventHandler = (sender, args) =>
            {
                if (!args.Mode.Equals(PowerModes.Resume)) return;
                Thread.Sleep(100);
                InitializeSynapticsTouchpad();
            };
            SystemEvents.PowerModeChanged += _powerModeChangedEventHandler;
        }

        private bool InitializeSynapticsTouchpad()
        {
            _device = new SynDeviceCtrl();
            _packet = new SynPacketCtrl();

            // Initialize and activate the API.
            ISynAPICtrl api = new SynAPICtrl();
            api.Initialize();
            api.Activate();

            // Find the device.
            var deviceHandle = api.FindDevice(SynConnectionType.SE_ConnectionAny, SynDeviceType.SE_DeviceTouchPad, -1);
            if (deviceHandle == -1)
            {
                Console.WriteLine("Couldn't find the device.");
                return false;
            }

            // Select and activate the device.
            _device.Select(deviceHandle);
            _device.Activate();

            // Register our custom event.
            _device.OnPacket += OnSynPacket;
            return true;
        }

        private void OnSynPacket()
        {
            // Load the newly arrived packet.
            _device.LoadPacket(_packet);
            // And send it to the touchpad listeners.
            var packet = new Packet(_packet);
            OnPacket?.Invoke(this, packet);
        }

        public bool IsAquired()
        {
            return _aquired;
        }

        public void Acquire(bool acquire)
        {
            if (acquire && !_aquired)
            {
                _aquired = true;
                _device.Acquire((int) SynAcquisitionFlags.SF_AcquireAll);
            }
            else if (!acquire && _aquired)
            {
                _aquired = false;
                _device.Unacquire();
            }
        }

        public long GetSensorLowX()
        {
            return _device.GetLongProperty(SynDeviceProperty.SP_XLoSensor);
        }

        public long GetSensorHighX()
        {
            return _device.GetLongProperty(SynDeviceProperty.SP_XHiSensor);
        }

        public long GetSensorLowY()
        {
            return _device.GetLongProperty(SynDeviceProperty.SP_YLoSensor);
        }

        public long GetSensorHighY()
        {
            return _device.GetLongProperty(SynDeviceProperty.SP_YHiSensor);
        }

        ~Touchpad()
        {
            SystemEvents.PowerModeChanged -= _powerModeChangedEventHandler;
        }
    }
}