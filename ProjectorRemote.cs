using System;

namespace QuanLyThietBi
{
    // Dieu khien may chieu, co muc pin
    public class ProjectorRemote : TeachingDevice
    {
        public int BatteryLevel { get; }

        public ProjectorRemote(string id, string name, string storageLocation, int batteryLevel)
            : base(id, name, storageLocation)
        {
            if (batteryLevel < 0 || batteryLevel > 100)
                throw new ArgumentException("BatteryLevel phai tu 0 den 100");

            BatteryLevel = batteryLevel;
        }

        public override bool IsReadyForUse()
        {
            // san sang khi Available va pin >= 20%
            return Status == DeviceStatus.Available && BatteryLevel >= 20;
        }

        public override double GetDefaultPriorityScore()
        {
            // diem uu tien bang muc pin
            return BatteryLevel;
        }
    }
}