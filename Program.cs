using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== HE THONG QUAN LY THIET BI (LAB W07) ===");
            Console.WriteLine();

            // Tao du lieu nhu de bai yeu cau
            var p1 = new ProjectorRemote("P1", "Dieu khien may chieu 1", "Kho A", 90);
            var p2 = new ProjectorRemote("P2", "Dieu khien may chieu 2", "Kho A", 40);
            var p3 = new ProjectorRemote("P3", "Dieu khien may chieu 3", "Kho B", 15); // pin 15 < 20, khong san sang

            var a1 = new AirConditionerRemote("A1", "Dieu khien dieu hoa 1", "Kho A", new string[] { "A101", "A203" });
            var a2 = new AirConditionerRemote("A2", "Dieu khien dieu hoa 2", "Kho B", new string[] { "B202" });

            var m1 = new WirelessMicrophone("M1", "Micro 1", "Kho A", 80, 90);
            var m2 = new WirelessMicrophone("M2", "Micro 2", "Kho B", 50, 30); // tin hieu 30 < 40, khong san sang
            var m3 = new WirelessMicrophone("M3", "Micro 3", "Kho A", 60, 70);

            // Test 1: them thiet bi vao repository
            Console.WriteLine("[Test 1] Them thiet bi vao repository");
            var projectorRepo = new InMemoryRepository<ProjectorRemote>();
            var acRepo = new InMemoryRepository<AirConditionerRemote>();
            var micRepo = new InMemoryRepository<WirelessMicrophone>();

            projectorRepo.Add(p2); projectorRepo.Add(p1); projectorRepo.Add(p3);
            acRepo.Add(a1); acRepo.Add(a2);
            micRepo.Add(m1); micRepo.Add(m2); micRepo.Add(m3);
            Console.WriteLine("  Them xong: " + projectorRepo.GetAll().Count + " may chieu, " +
                acRepo.GetAll().Count + " dieu hoa, " + micRepo.GetAll().Count + " micro");

            // Test 2: them thiet bi trung ma
            Console.WriteLine("[Test 2] Them thiet bi trung ma");
            var dupRepo = new InMemoryRepository<ProjectorRemote>();
            dupRepo.Add(new ProjectorRemote("P1", "May chieu A", "Kho A", 50));
            try
            {
                dupRepo.Add(new ProjectorRemote("P1", "May chieu B", "Kho B", 60));
                Console.WriteLine("  KHONG BAT DUOC LOI (da them trung ma)");
            }
            catch (DuplicateIdException ex)
            {
                Console.WriteLine("  BAT DUNG LOI: " + ex.Message);
            }

            // Test 3: tim thiet bi theo ma
            Console.WriteLine("[Test 3] Tim thiet bi theo ma");
            ProjectorRemote found = projectorRepo.FindById("P2");
            Console.WriteLine("  Tim P2 -> " + (found != null ? found.Name : "khong thay"));

            // Test 4: loc thiet bi san sang
            Console.WriteLine("[Test 4] Loc thiet bi san sang");
            var readyProjectors = projectorRepo.FindAll(d => d.IsReadyForUse());
            Console.WriteLine("  So may chieu san sang: " + readyProjectors.Count);

            // Test 5: chon may chieu co diem cao nhat
            Console.WriteLine("[Test 5] Chon may chieu co pin cao nhat");
            var projStrategy = new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore());
            var projService = new AllocationService<ProjectorRemote>(projectorRepo, projStrategy);
            ProjectorRemote chosenProj = projService.Allocate(d => true);
            Console.WriteLine("  Chon duoc: " + chosenProj.Id + " (pin " + chosenProj.BatteryLevel + ")");

            // Test 6: chon dieu hoa tuong thich phong A203
            Console.WriteLine("[Test 6] Chon dieu hoa tuong thich phong A203");
            var acStrategy = new HighestPriorityStrategy<AirConditionerRemote>(d => d.GetDefaultPriorityScore());
            var acService = new AllocationService<AirConditionerRemote>(acRepo, acStrategy);
            AirConditionerRemote chosenAc = acService.Allocate(d => d.IsCompatibleWith("A203"));
            Console.WriteLine("  Chon duoc: " + chosenAc.Id);

            // Test 7: chon micro dau tien san sang
            Console.WriteLine("[Test 7] Chon micro dau tien san sang");
            var micStrategy = new FirstAvailableStrategy<WirelessMicrophone>();
            var micService = new AllocationService<WirelessMicrophone>(micRepo, micStrategy);
            WirelessMicrophone chosenMic = micService.Allocate(d => true);
            Console.WriteLine("  Chon duoc: " + chosenMic.Id);

            // Test 8: doi chien luoc luc chay
            Console.WriteLine("[Test 8] Doi chien luoc trong thoi gian chay");
            var demoRepo = new InMemoryRepository<ProjectorRemote>();
            demoRepo.Add(new ProjectorRemote("DP1", "Demo 1", "Kho D", 30));
            demoRepo.Add(new ProjectorRemote("DP2", "Demo 2", "Kho D", 90));
            var demoService = new AllocationService<ProjectorRemote>(demoRepo, new FirstAvailableStrategy<ProjectorRemote>());
            ProjectorRemote firstPick = demoService.Allocate(d => true);
            Console.WriteLine("  FirstAvailable chon: " + firstPick.Id + " (pin " + firstPick.BatteryLevel + ")");
            firstPick.Return();
            demoService.ChangeStrategy(new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore()));
            ProjectorRemote highPick = demoService.Allocate(d => true);
            Console.WriteLine("  HighestPriority chon: " + highPick.Id + " (pin " + highPick.BatteryLevel + ")");

            // Test 9: cap phat khi khong co ung vien
            Console.WriteLine("[Test 9] Cap phat khi khong co ung vien phu hop");
            var emptyRepo = new InMemoryRepository<WirelessMicrophone>();
            emptyRepo.Add(new WirelessMicrophone("E1", "Micro het pin", "Kho E", 10, 50));
            var emptyService = new AllocationService<WirelessMicrophone>(emptyRepo, new FirstAvailableStrategy<WirelessMicrophone>());
            try
            {
                emptyService.Allocate(d => true);
                Console.WriteLine("  KHONG BAT DUOC LOI");
            }
            catch (NoSuitableItemException ex)
            {
                Console.WriteLine("  BAT DUNG LOI: " + ex.Message);
            }

            // Test 10: thiet bi duoc chon chuyen sang Borrowed
            Console.WriteLine("[Test 10] Thiet bi duoc chon thanh Borrowed");
            Console.WriteLine("  Micro M1 sau khi cap phat co trang thai: " + micRepo.FindById("M1").Status);

            // Test 11: repository tong quat dung cho nhieu loai
            Console.WriteLine("[Test 11] Repository tong quat dung cho nhieu loai thiet bi");
            var generalRepo = new InMemoryRepository<TeachingDevice>();
            generalRepo.Add(new ProjectorRemote("G1", "May chieu tong quat", "Kho G", 50));
            generalRepo.Add(new WirelessMicrophone("G2", "Micro tong quat", "Kho G", 70, 80));
            generalRepo.Add(new AirConditionerRemote("G3", "Dieu hoa tong quat", "Kho G", new string[] { "A101" }));
            Console.WriteLine("  Mot repo chua duoc " + generalRepo.GetAll().Count + " thiet bi khac loai");

            Console.WriteLine();
            Console.WriteLine("Nhan phim bat ky de ket thuc...");
            Console.ReadKey();
        }
    }
}