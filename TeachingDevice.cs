using System;

namespace QuanLyThietBi
{
    // Lop truu tuong dai dien cho thiet bi giang day
    public abstract class TeachingDevice : IIdentifiable
    {
        public string Id { get; }
        public string Name { get; }
        public string StorageLocation { get; }
        public DeviceStatus Status { get; protected set; }

        public TeachingDevice(string id, string name, string storageLocation)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id khong duoc rong");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name khong duoc rong");
            if (string.IsNullOrWhiteSpace(storageLocation))
                throw new ArgumentException("StorageLocation khong duoc rong");

            Id = id;
            Name = name;
            StorageLocation = storageLocation;
            Status = DeviceStatus.Available;
        }

        public abstract bool IsReadyForUse();
        public abstract double GetDefaultPriorityScore();

        public void Borrow()
        {
            if (!IsReadyForUse())
                throw new DeviceNotReadyException(Id, Status);

            Status = DeviceStatus.Borrowed;
        }

        public void Return()
        {
            // FIX lan 2: chi cho tra khi dang o trang thai Borrowed
            if (Status != DeviceStatus.Borrowed)
                throw new InvalidOperationException("Thiet bi " + Id + " khong o trang thai Borrowed");

            Status = DeviceStatus.Available;
        }
    }
}
