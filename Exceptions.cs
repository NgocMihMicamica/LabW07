using System;

namespace QuanLyThietBi
{
    // Them doi tuong co Id da ton tai
    public class DuplicateIdException : Exception
    {
        public string DuplicateId { get; }

        public DuplicateIdException(string duplicateId)
            : base("Da ton tai doi tuong co Id: " + duplicateId)
        {
            DuplicateId = duplicateId;
        }
    }

    // Khong tim thay doi tuong phu hop
    public class NoSuitableItemException : Exception
    {
        public NoSuitableItemException()
            : base("Khong tim thay doi tuong phu hop")
        {
        }
    }

    // Thiet bi chua san sang
    public class DeviceNotReadyException : Exception
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }

        public DeviceNotReadyException(string deviceId, DeviceStatus currentStatus)
            : base("Thiet bi " + deviceId + " chua san sang (trang thai: " + currentStatus + ")")
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }
    }
}