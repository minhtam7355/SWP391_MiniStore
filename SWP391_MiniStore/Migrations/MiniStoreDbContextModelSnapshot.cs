﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SWP391_MiniStore.Data;

#nullable disable

namespace SWP391_MiniStore.Migrations
{
    [DbContext(typeof(MiniStoreDbContext))]
    partial class MiniStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.Attendance", b =>
                {
                    b.Property<Guid>("AttendanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FkStaffID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AttendanceID");

                    b.HasIndex("FkStaffID");

                    b.ToTable("tblAttendances", (string)null);
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.LeaveApplication", b =>
                {
                    b.Property<Guid>("LeaveApplicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FkStaffID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LeaveApprovalReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("LeaveApprovalStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LeaveEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeaveReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LeaveStart")
                        .HasColumnType("datetime2");

                    b.HasKey("LeaveApplicationID");

                    b.HasIndex("FkStaffID");

                    b.ToTable("tblLeaveApplications", (string)null);
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.Payroll", b =>
                {
                    b.Property<Guid>("PayrollID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FkStaffID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("GrossPay")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("NetPay")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime?>("PayDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Payslip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PayrollID");

                    b.HasIndex("FkStaffID");

                    b.ToTable("tblPayrolls", (string)null);
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.ShiftAssignment", b =>
                {
                    b.Property<Guid>("AssignmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FkShiftID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FkStaffID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AssignmentID");

                    b.HasIndex("FkShiftID");

                    b.HasIndex("FkStaffID");

                    b.ToTable("tblShiftAssignments", (string)null);
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.StoreStaff", b =>
                {
                    b.Property<Guid>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("HourlyRate")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("StaffStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffID");

                    b.ToTable("tblStoreStaff", (string)null);
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.WorkShift", b =>
                {
                    b.Property<Guid>("ShiftID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("EndTime")
                        .HasColumnType("time");

                    b.Property<bool?>("Holiday")
                        .HasColumnType("bit");

                    b.Property<decimal?>("SalaryCoef")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("StaffRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("StartTime")
                        .HasColumnType("time");

                    b.Property<int?>("WorkHours")
                        .HasColumnType("int");

                    b.HasKey("ShiftID");

                    b.ToTable("tblWorkShifts", (string)null);
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.Attendance", b =>
                {
                    b.HasOne("SWP391_MiniStore.Models.Domain.StoreStaff", "Staff")
                        .WithMany("Attendances")
                        .HasForeignKey("FkStaffID");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.LeaveApplication", b =>
                {
                    b.HasOne("SWP391_MiniStore.Models.Domain.StoreStaff", "Staff")
                        .WithMany("LeaveApplications")
                        .HasForeignKey("FkStaffID");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.Payroll", b =>
                {
                    b.HasOne("SWP391_MiniStore.Models.Domain.StoreStaff", "Staff")
                        .WithMany("Payrolls")
                        .HasForeignKey("FkStaffID");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.ShiftAssignment", b =>
                {
                    b.HasOne("SWP391_MiniStore.Models.Domain.WorkShift", "Shift")
                        .WithMany("ShiftAssignments")
                        .HasForeignKey("FkShiftID");

                    b.HasOne("SWP391_MiniStore.Models.Domain.StoreStaff", "Staff")
                        .WithMany("ShiftAssignments")
                        .HasForeignKey("FkStaffID");

                    b.Navigation("Shift");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.StoreStaff", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("LeaveApplications");

                    b.Navigation("Payrolls");

                    b.Navigation("ShiftAssignments");
                });

            modelBuilder.Entity("SWP391_MiniStore.Models.Domain.WorkShift", b =>
                {
                    b.Navigation("ShiftAssignments");
                });
#pragma warning restore 612, 618
        }
    }
}