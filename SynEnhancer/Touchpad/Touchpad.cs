using System;
using SYNCTRLLib;

namespace SynEnhancer
{
    public class Touchpad
    {
        private readonly SynDeviceCtrl _device;
        private readonly SynPacketCtrl _packet;
        private bool _aquired;

        public delegate void PacketHandler(Touchpad touchpad, Packet packet);

        public event PacketHandler OnPacket;

        public Touchpad()
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
                Console.Error.WriteLine("Couldn't find the device.");
                return;
            }

            // Select and activate the device.
            _device.Select(deviceHandle);
            _device.Activate();
            // Add the custom packet handler.
            _device.OnPacket += OnSynPacket;
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
                //_device.Acquire((int) SynAcquisitionFlags.SF_AcquireAll);
            }
            else if (!acquire && _aquired)
            {
                _aquired = false;
                //_device.Unacquire();
            }
        }

        public long GetMinX()
        {
            return _device.GetLongProperty(SynDeviceProperty.SP_XLoSensor);
        }

        public long GetMaxX()
        {
            return _device.GetLongProperty(SynDeviceProperty.SP_XHiSensor);
        }

        public long GetMinY()
        {
            return _device.GetLongProperty(SynDeviceProperty.SP_YLoSensor);
        }

        public long GetMaxY()
        {
            return _device.GetLongProperty(SynDeviceProperty.SP_YHiSensor);
        }
    }
}