using System;

namespace QuanLyThietBi
{
    // Micro khong day, co pin va chat luong tin hieu
    public class WirelessMicrophone : TeachingDevice
    {
        public int BatteryLevel { get; }
        public int SignalQuality { get; }

        public WirelessMicrophone(string id, string name, string storageLocation, int batteryLevel, int signalQuality)
            : base(id, name, storageLocation)
        {
            if (batteryLevel < 0 || batteryLevel > 100)
                throw new ArgumentException("BatteryLevel phai tu 0 den 100");
            if (signalQuality < 0 || signalQuality > 100)
                throw new ArgumentException("SignalQuality phai tu 0 den 100");

            BatteryLevel = batteryLevel;
            SignalQuality = signalQuality;
        }

        public override bool IsReadyForUse()
        {
            // san sang khi Available, pin >= 30% va tin hieu >= 40%
            return Status == DeviceStatus.Available && BatteryLevel >= 30 && SignalQuality >= 40;
        }

        public override double GetDefaultPriorityScore()
        {
            // diem = trung binh pin va tin hieu
            return (BatteryLevel + SignalQuality) / 2.0;
        }
    }
}