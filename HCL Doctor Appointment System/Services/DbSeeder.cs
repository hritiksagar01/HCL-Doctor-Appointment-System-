using Doctor_Appointment_System.Data;
using Doctor_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Services;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Specialties.AnyAsync()) return;

        // Specialties
        var specialties = new List<Specialty>
        {
            new() { SpecialtyId = 1, SpecialtyName = "Cardiology" },
            new() { SpecialtyId = 2, SpecialtyName = "Dermatology" },
            new() { SpecialtyId = 3, SpecialtyName = "Neurology" },
            new() { SpecialtyId = 4, SpecialtyName = "Pediatrics" },
            new() { SpecialtyId = 5, SpecialtyName = "Orthopedics" },
            new() { SpecialtyId = 6, SpecialtyName = "Psychiatry" },
            new() { SpecialtyId = 7, SpecialtyName = "General Medicine" }
        };
        context.Specialties.AddRange(specialties);
        await context.SaveChangesAsync();

        // Doctors
        var doctors = new List<Doctor>
        {
            new() { DoctorId = 101, DoctorName = "Dr. Julian Sterling", SpecialtyId = 1, Experience = 14, Fees = 1200, Mode = "Online", Email = "julian.sterling@stoiccare.com", ClinicAddress = "204 Riverfront Clinic, Indiranagar, Bengaluru" },
            new() { DoctorId = 102, DoctorName = "Dr. Meera Anand", SpecialtyId = 1, Experience = 11, Fees = 1100, Mode = "Offline", Email = "meera.anand@stoiccare.com", ClinicAddress = "12 Cedar Heart Centre, Koramangala, Bengaluru" },
            new() { DoctorId = 103, DoctorName = "Dr. Elena Rossi", SpecialtyId = 2, Experience = 8, Fees = 950, Mode = "Online", Email = "elena.rossi@stoiccare.com", ClinicAddress = "55 Skincare Avenue, Whitefield, Bengaluru" },
            new() { DoctorId = 104, DoctorName = "Dr. Aarav Menon", SpecialtyId = 2, Experience = 10, Fees = 1000, Mode = "Offline", Email = "aarav.menon@stoiccare.com", ClinicAddress = "88 Lakeside Clinic, HSR Layout, Bengaluru" },
            new() { DoctorId = 105, DoctorName = "Dr. Marcus Thorne", SpecialtyId = 3, Experience = 15, Fees = 1500, Mode = "Online", Email = "marcus.thorne@stoiccare.com", ClinicAddress = "14 Central Neuro Care, MG Road, Bengaluru" },
            new() { DoctorId = 106, DoctorName = "Dr. Nisha Kapoor", SpecialtyId = 3, Experience = 12, Fees = 1450, Mode = "Offline", Email = "nisha.kapoor@stoiccare.com", ClinicAddress = "62 North Neuro Clinic, Jayanagar, Bengaluru" },
            new() { DoctorId = 107, DoctorName = "Dr. Sarah Chen", SpecialtyId = 4, Experience = 9, Fees = 900, Mode = "Online", Email = "sarah.chen@stoiccare.com", ClinicAddress = "27 Family Health Point, Bellandur, Bengaluru" },
            new() { DoctorId = 108, DoctorName = "Dr. Rohan Iyer", SpecialtyId = 4, Experience = 13, Fees = 980, Mode = "Offline", Email = "rohan.iyer@stoiccare.com", ClinicAddress = "101 Child Wellness Centre, Banashankari, Bengaluru" },
            new() { DoctorId = 109, DoctorName = "Dr. James Wright", SpecialtyId = 5, Experience = 18, Fees = 1600, Mode = "Online", Email = "james.wright@stoiccare.com", ClinicAddress = "40 Bone & Joint Institute, Whitefield, Bengaluru" },
            new() { DoctorId = 110, DoctorName = "Dr. Kavya Krishnan", SpecialtyId = 5, Experience = 12, Fees = 1300, Mode = "Offline", Email = "kavya.krishnan@stoiccare.com", ClinicAddress = "40 Bone & Joint Institute, Whitefield, Bengaluru" },
            new() { DoctorId = 111, DoctorName = "Dr. Daniel Fox", SpecialtyId = 6, Experience = 20, Fees = 2000, Mode = "Online", Email = "daniel.fox@stoiccare.com", ClinicAddress = "70 MindCare Centre, Indiranagar, Bengaluru" },
            new() { DoctorId = 112, DoctorName = "Dr. Anita Desai", SpecialtyId = 6, Experience = 14, Fees = 1500, Mode = "Offline", Email = "anita.desai@stoiccare.com", ClinicAddress = "70 MindCare Centre, Indiranagar, Bengaluru" },
            new() { DoctorId = 113, DoctorName = "Dr. Samuel Okon", SpecialtyId = 7, Experience = 22, Fees = 800, Mode = "Online", Email = "samuel.okon@stoiccare.com", ClinicAddress = "22 Primary Health, Bellandur, Bengaluru" },
            new() { DoctorId = 114, DoctorName = "Dr. Vinay Sharma", SpecialtyId = 7, Experience = 15, Fees = 850, Mode = "Offline", Email = "vinay.sharma@stoiccare.com", ClinicAddress = "22 Primary Health, Bellandur, Bengaluru" },
            new() { DoctorId = 115, DoctorName = "Dr. Vikram Rao", SpecialtyId = 1, Experience = 16, Fees = 1350, Mode = "Offline", Email = "vikram.rao@stoiccare.com", ClinicAddress = "77 Prime Cardiac Centre, JP Nagar, Bengaluru" }
        };
        context.Doctors.AddRange(doctors);
        await context.SaveChangesAsync();

        // Slots
        var slots = new List<Slot>
        {
            new() { SlotId = 1, DoctorId = 101, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(9, 30), IsBooked = false },
            new() { SlotId = 2, DoctorId = 101, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(11, 30), IsBooked = true },
            new() { SlotId = 3, DoctorId = 101, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(15, 30), IsBooked = false },
            new() { SlotId = 4, DoctorId = 102, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(10, 30), IsBooked = false },
            new() { SlotId = 5, DoctorId = 102, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(13, 30), IsBooked = false },
            new() { SlotId = 6, DoctorId = 102, SlotDate = new DateOnly(2026, 4, 22), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(16, 30), IsBooked = true },
            new() { SlotId = 7, DoctorId = 103, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(9, 30), EndTime = new TimeOnly(10, 0), IsBooked = false },
            new() { SlotId = 8, DoctorId = 103, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(12, 30), IsBooked = false },
            new() { SlotId = 10, DoctorId = 104, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(11, 30), EndTime = new TimeOnly(12, 0), IsBooked = false },
            new() { SlotId = 11, DoctorId = 104, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(14, 30), IsBooked = false },
            new() { SlotId = 13, DoctorId = 105, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(8, 30), EndTime = new TimeOnly(9, 0), IsBooked = false },
            new() { SlotId = 14, DoctorId = 105, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(10, 30), EndTime = new TimeOnly(11, 0), IsBooked = true },
            new() { SlotId = 15, DoctorId = 105, SlotDate = new DateOnly(2026, 4, 22), StartTime = new TimeOnly(15, 30), EndTime = new TimeOnly(16, 0), IsBooked = false },
            new() { SlotId = 16, DoctorId = 106, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(9, 15), EndTime = new TimeOnly(9, 45), IsBooked = false },
            new() { SlotId = 17, DoctorId = 106, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(13, 15), EndTime = new TimeOnly(13, 45), IsBooked = false },
            new() { SlotId = 19, DoctorId = 107, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(10, 15), EndTime = new TimeOnly(10, 45), IsBooked = false },
            new() { SlotId = 20, DoctorId = 107, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(12, 45), EndTime = new TimeOnly(13, 15), IsBooked = true },
            new() { SlotId = 21, DoctorId = 107, SlotDate = new DateOnly(2026, 4, 22), StartTime = new TimeOnly(14, 15), EndTime = new TimeOnly(14, 45), IsBooked = false },
            new() { SlotId = 22, DoctorId = 108, SlotDate = new DateOnly(2026, 4, 22), StartTime = new TimeOnly(11, 15), EndTime = new TimeOnly(11, 45), IsBooked = false },
            new() { SlotId = 23, DoctorId = 108, SlotDate = new DateOnly(2026, 4, 23), StartTime = new TimeOnly(15, 15), EndTime = new TimeOnly(15, 45), IsBooked = false },
            new() { SlotId = 25, DoctorId = 109, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(8, 45), EndTime = new TimeOnly(9, 15), IsBooked = false },
            new() { SlotId = 26, DoctorId = 109, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(12, 15), EndTime = new TimeOnly(12, 45), IsBooked = true },
            new() { SlotId = 28, DoctorId = 110, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(9, 45), EndTime = new TimeOnly(10, 15), IsBooked = false },
            new() { SlotId = 29, DoctorId = 110, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(14, 45), EndTime = new TimeOnly(15, 15), IsBooked = true },
            new() { SlotId = 31, DoctorId = 111, SlotDate = new DateOnly(2026, 4, 25), StartTime = new TimeOnly(10, 45), EndTime = new TimeOnly(11, 15), IsBooked = false },
            new() { SlotId = 32, DoctorId = 111, SlotDate = new DateOnly(2026, 4, 26), StartTime = new TimeOnly(13, 30), EndTime = new TimeOnly(14, 0), IsBooked = true },
            new() { SlotId = 34, DoctorId = 112, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(11, 45), EndTime = new TimeOnly(12, 15), IsBooked = false },
            new() { SlotId = 35, DoctorId = 112, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(15, 45), EndTime = new TimeOnly(16, 15), IsBooked = false },
            new() { SlotId = 37, DoctorId = 113, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(10, 30), IsBooked = false },
            new() { SlotId = 38, DoctorId = 113, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(11, 30), IsBooked = false },
            new() { SlotId = 40, DoctorId = 114, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(9, 30), IsBooked = false },
            new() { SlotId = 41, DoctorId = 114, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(14, 30), IsBooked = false },
            new() { SlotId = 43, DoctorId = 115, SlotDate = new DateOnly(2026, 4, 20), StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(16, 30), IsBooked = false },
            new() { SlotId = 44, DoctorId = 115, SlotDate = new DateOnly(2026, 4, 21), StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(17, 30), IsBooked = true }
        };
        context.Slots.AddRange(slots);
        await context.SaveChangesAsync();

        // Demo user (password: demo12345)
        var demoUser = new User
        {
            UserId = 1,
            FullName = "Demo Patient",
            Email = "demo@stoiccare.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("demo12345"),
            Role = "Patient",
            CreatedAt = DateTime.UtcNow
        };
        context.Users.Add(demoUser);
        await context.SaveChangesAsync();

        // Sample appointments
        var appointments = new List<Appointment>
        {
            new()
            {
                AppointmentId = 2001, UserId = 1, DoctorId = 101, SlotId = 2,
                Mode = "Online", Status = "Confirmed",
                BookingDate = new DateTime(2026, 4, 12, 9, 15, 0, DateTimeKind.Utc),
                MeetingLink = "https://meet.stoiccare.app/dr-julian-sterling-2",
                ClinicAddress = null
            },
            new()
            {
                AppointmentId = 2002, UserId = 1, DoctorId = 108, SlotId = 22,
                Mode = "Offline", Status = "Confirmed",
                BookingDate = new DateTime(2026, 4, 14, 10, 0, 0, DateTimeKind.Utc),
                MeetingLink = null,
                ClinicAddress = "101 Child Wellness Centre, Banashankari, Bengaluru"
            },
            new()
            {
                AppointmentId = 2003, UserId = 1, DoctorId = 111, SlotId = 31,
                Mode = "Online", Status = "Confirmed",
                BookingDate = new DateTime(2026, 4, 15, 11, 20, 0, DateTimeKind.Utc),
                MeetingLink = "https://meet.stoiccare.app/dr-daniel-fox-31",
                ClinicAddress = null
            }
        };
        context.Appointments.AddRange(appointments);
        await context.SaveChangesAsync();
    }
}

