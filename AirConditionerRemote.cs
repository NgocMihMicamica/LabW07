using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Dieu khien dieu hoa, co danh sach phong tuong thich
    public class AirConditionerRemote : TeachingDevice
    {
        public IReadOnlySet<string> CompatibleRooms { get; }

        public AirConditionerRemote(string id, string name, string storageLocation, IEnumerable<string> compatibleRooms)
            : base(id, name, storageLocation)
        {
            CompatibleRooms = new HashSet<string>(compatibleRooms);
        }

        public bool IsCompatibleWith(string roomId)
        {
            return CompatibleRooms.Contains(roomId);
        }

        public override bool IsReadyForUse()
        {
            // dieu hoa chi can Available la san sang (kiem tra phong o buoc loc)
            return Status == DeviceStatus.Available;
        }

        public override double GetDefaultPriorityScore()
        {
            // diem mac dinh 100; viec loc phong tuong thich lam o dieu kien loc
            return 100;
        }
    }
}