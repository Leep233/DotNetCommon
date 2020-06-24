using DotNetFramework.Common.Events;
using System;
using System.IO.Ports;

namespace DotNetFramework.Common.Components
{
    public class SimpleSerialPort
    {

        private readonly static object locker = new object();

        public FrameworkEvent<byte[]> OnReceivedEvent;

        public FrameworkEvent Disconnected;

        public SerialPort CurrentPort { get; private set; }

        public bool IsOpen { get; private set; }

        private byte[] _buffer = new byte[1024];

        private SimpleSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            OnReceivedEvent = new FrameworkEvent<byte[]>();

            Disconnected = new FrameworkEvent();

            CurrentPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

            IsOpen = false;
        }

        public static SimpleSerialPort CreateSerialPortEX(string portName, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            return new SimpleSerialPort(portName, baudRate, parity, dataBits, stopBits);
        }

        public void Open(int timeout = 5000)
        {
            if (CurrentPort == null || CurrentPort.IsOpen || IsOpen)
                throw new Exception("The Object isn't Create or Object is Open");

            CurrentPort.ReadTimeout = CurrentPort.WriteTimeout = timeout;

            CurrentPort.DataReceived += OnDataReceivced;

            CurrentPort.ErrorReceived += OnErrorReceived;

            CurrentPort.PinChanged += OnPinChanged;

            CurrentPort.Open();

            this.IsOpen = true;
        }

        private void OnPinChanged(object sender, SerialPinChangedEventArgs e)
        {
            
        }

        private void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
           
        }
        
        private void OnDataReceivced(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;

            if (serialPort == null)
                return;

            byte[] data = null;

            lock (locker)
            {
                try
                {
                    int length = serialPort.Read(_buffer, 0, _buffer.Length);

                    data = new byte[length];

                    Buffer.BlockCopy(_buffer, 0, data, 0, length);
                }
                catch (Exception ex)
                {
                    Close();
                }
            }
         
            OnReceivedEvent?.Invoke(data);
        }

        public void Send(byte[] content)
        {
            if (CurrentPort == null || !CurrentPort.IsOpen || !IsOpen)
                return;

            CurrentPort.Write(content, 0, content.Length);
        }

        public void Send(string content)
        {
            if (CurrentPort == null || !CurrentPort.IsOpen || !IsOpen)
                return;

            CurrentPort.Write(content);
        }

        public void Close()
        {
            if (!IsOpen)
                return;

            Disconnected?.Invoke();

            CurrentPort?.Close();
        }

    }
}
