using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Models.Domain;

namespace SWP391_MiniStore.Data
{
    public class MiniStoreDbContext : DbContext
    {
        public MiniStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            // Configure other options, such as the database provider and connection string
        }

        // Staff Management
        public DbSet<StoreStaff> StoreStaff { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
        public DbSet<ShiftAssignment> ShiftAssignments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }

        // E-Commerce 
        public DbSet<Order> Orders { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreStaff>().ToTable("tblStoreStaff");
            modelBuilder.Entity<WorkShift>().ToTable("tblWorkShifts");
            modelBuilder.Entity<ShiftAssignment>().ToTable("tblShiftAssignments");
            modelBuilder.Entity<Attendance>().ToTable("tblAttendances");
            modelBuilder.Entity<LeaveApplication>().ToTable("tblLeaveApplications");
            modelBuilder.Entity<Payroll>().ToTable("tblPayrolls");

            modelBuilder.Entity<Order>().ToTable("tblOrders");
            modelBuilder.Entity<Voucher>().ToTable("tblVouchers");
            modelBuilder.Entity<OrderDetail>().ToTable("tblOrderDetails");
            modelBuilder.Entity<Product>().ToTable("tblProducts");
            modelBuilder.Entity<Category>().ToTable("tblCategories");
            modelBuilder.Entity<Customer>().ToTable("tblCustomers");
            modelBuilder.Entity<Feedback>().ToTable("tblFeedbacks");

            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.FkOrderID, od.FkProductID });

            // Staff Management
            modelBuilder.Entity<ShiftAssignment>()
                .HasOne(sa => sa.Staff)
                .WithMany(s => s.ShiftAssignments)
                .HasForeignKey(sa => sa.FkStaffID);

            modelBuilder.Entity<ShiftAssignment>()
                .HasOne(sa => sa.Shift)
                .WithMany(s => s.ShiftAssignments)
                .HasForeignKey(sa => sa.FkShiftID);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Staff)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.FkStaffID);

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(la => la.Staff)
                .WithMany(s => s.LeaveApplications)
                .HasForeignKey(la => la.FkStaffID);
            
            modelBuilder.Entity<LeaveApplication>()
                .HasOne(la => la.Manager)
                .WithMany(m => m.ProcessedLeaveApplications)
                .HasForeignKey(la => la.FkManagerID)
                .IsRequired(false);

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Staff)
                .WithMany(s => s.Payrolls)
                .HasForeignKey(p => p.FkStaffID);

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Manager)
                .WithMany(m => m.SentPayrolls)
                .HasForeignKey(p => p.FkManagerID);

            // E-Commerce 
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.FkCustomerID);
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Staff)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.FkStaffID)
                .IsRequired(false);
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Voucher)
                .WithMany(v => v.Orders)
                .HasForeignKey(o => o.FkVoucherID)
                .IsRequired(false);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.FkOrderID);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.FkProductID);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.FkCategoryID);
            
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.FkCustomerID);
            
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Product)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.FkProductID);

            // Specify precision and scale
            modelBuilder.Entity<StoreStaff>()
                .Property(ss => ss.HourlyRate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<WorkShift>()
                .Property(ws => ws.SalaryCoef)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payroll>()
                .Property(p => p.GrossPay)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payroll>()
                .Property(p => p.NetPay)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTotalBeforeVoucher)
                .HasPrecision(10, 2);
            
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTotalAfterVoucher)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Voucher>()
                .Property(v => v.DiscountPercentage)
                .HasPrecision(5, 2);
            
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.OrderedProductPrice)
                .HasPrecision(10, 2);
            
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.SubTotal)
                .HasPrecision(10, 2);
            
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Balance)
                .HasPrecision(10, 2);
            
            // INSERT DATA FOR STAFF MANAGEMENT
            // Insert rows for Sales Shift
            var salesShift1 = new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("06:00"), EndTime = TimeSpan.Parse("12:00"), WorkHours = 6, SalaryCoef = 1 };
            var salesShift2 = new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("12:00"), EndTime = TimeSpan.Parse("18:00"), WorkHours = 6, SalaryCoef = 1 };
            var salesShift3 = new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("18:00"), EndTime = TimeSpan.Parse("06:00"), WorkHours = 12, SalaryCoef = (decimal)1.5 };
            var salesShift4 = new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = false, DayOfWeek = "Sunday", StartTime = TimeSpan.Parse("18:00"), EndTime = TimeSpan.Parse("06:00"), WorkHours = 12, SalaryCoef = 2 };
            var salesShift5 = new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = true, DayOfWeek = null, StartTime = TimeSpan.Parse("18:00"), EndTime = TimeSpan.Parse("06:00"), WorkHours = 12, SalaryCoef = 3 };

            modelBuilder.Entity<WorkShift>().HasData(
                salesShift1,
                salesShift2,
                salesShift3,
                salesShift4,
                salesShift5
            );

            // Insert rows for Guard Shift
            var guardShift1 = new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Guard", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("06:00"), EndTime = TimeSpan.Parse("18:00"), WorkHours = 12, SalaryCoef = 1 };
            var guardShift2 = new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Guard", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("18:00"), EndTime = TimeSpan.Parse("06:00"), WorkHours = 12, SalaryCoef = (decimal)1.5 };

            modelBuilder.Entity<WorkShift>().HasData(
                guardShift1,
                guardShift2
            );

            // Insert rows for Staff
            var manager1 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "manager1", Email = "manager1@gmail.com", PhoneNumber = "1234567890", Password = "Password1#", Dob = new DateTime(1990, 1, 13), Address = "Address 1", StaffRole = "Manager", HourlyRate = new Random().Next(50, 71), StaffStatus = true };
            var manager2 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "manager2", Email = "manager2@gmail.com", PhoneNumber = "9876543210", Password = "Password2#", Dob = new DateTime(1990, 2, 14), Address = "Address 2", StaffRole = "Manager", HourlyRate = new Random().Next(50, 71), StaffStatus = true };
            var manager3 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "manager3", Email = "manager3@gmail.com", PhoneNumber = "1111111111", Password = "Password3#", Dob = new DateTime(1990, 3, 15), Address = "Address 3", StaffRole = "Manager", HourlyRate = new Random().Next(50, 71), StaffStatus = true };

            var sales1 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "sales1", Email = "sales1@gmail.com", PhoneNumber = "2222222222", Password = "Password4#", Dob = new DateTime(1990, 4, 16), Address = "Address 4", StaffRole = "Sales", HourlyRate = new Random().Next(30, 51), StaffStatus = true };
            var sales2 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "sales2", Email = "sales2@gmail.com", PhoneNumber = "3333333333", Password = "Password5#", Dob = new DateTime(1990, 5, 17), Address = "Address 5", StaffRole = "Sales", HourlyRate = new Random().Next(30, 51), StaffStatus = true };
            var sales3 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "sales3", Email = "sales3@gmail.com", PhoneNumber = "4444444444", Password = "Password6#", Dob = new DateTime(1990, 6, 18), Address = "Address 6", StaffRole = "Sales", HourlyRate = new Random().Next(30, 51), StaffStatus = true };

            var guard1 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "guard1", Email = "guard1@gmail.com", PhoneNumber = "5555555555", Password = "Password7#", Dob = new DateTime(1990, 7, 19), Address = "Address 7", StaffRole = "Guard", HourlyRate = new Random().Next(15, 31), StaffStatus = true };
            var guard2 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "guard2", Email = "guard2@gmail.com", PhoneNumber = "6666666666", Password = "Password8#", Dob = new DateTime(1990, 8, 20), Address = "Address 8", StaffRole = "Guard", HourlyRate = new Random().Next(15, 31), StaffStatus = true };
            var guard3 = new StoreStaff { StaffID = Guid.NewGuid(), Username = "guard3", Email = "guard3@gmail.com", PhoneNumber = "7777777777", Password = "Password9#", Dob = new DateTime(1990, 9, 21), Address = "Address 9", StaffRole = "Guard", HourlyRate = new Random().Next(15, 31), StaffStatus = true };

            modelBuilder.Entity<StoreStaff>().HasData(
                // Manager
                manager1,
                manager2,
                manager3,

                // Sales
                sales1,
                sales2,
                sales3,

                // Guard
                guard1,
                guard2,
                guard3
            );

            // Insert rows for ShiftAssignment
            modelBuilder.Entity<ShiftAssignment>().HasData(
                // Sales Shift Assignments
                new ShiftAssignment { AssignmentID = Guid.NewGuid(), FkStaffID = sales1.StaffID, FkShiftID = salesShift1.ShiftID, Date = new DateTime(2023, 6, 12), DayOfWeek = new DateTime(2023, 6, 12).DayOfWeek.ToString() },
                new ShiftAssignment { AssignmentID = Guid.NewGuid(), FkStaffID = sales2.StaffID, FkShiftID = salesShift2.ShiftID, Date = new DateTime(2023, 6, 13), DayOfWeek = new DateTime(2023, 6, 13).DayOfWeek.ToString() },
                new ShiftAssignment { AssignmentID = Guid.NewGuid(), FkStaffID = sales3.StaffID, FkShiftID = salesShift3.ShiftID, Date = new DateTime(2023, 6, 14), DayOfWeek = new DateTime(2023, 6, 14).DayOfWeek.ToString() },

                // Guard Shift Assignments
                new ShiftAssignment { AssignmentID = Guid.NewGuid(), FkStaffID = guard1.StaffID, FkShiftID = guardShift1.ShiftID, Date = new DateTime(2023, 6, 15), DayOfWeek = new DateTime(2023, 6, 15).DayOfWeek.ToString() },
                new ShiftAssignment { AssignmentID = Guid.NewGuid(), FkStaffID = guard2.StaffID, FkShiftID = guardShift2.ShiftID, Date = new DateTime(2023, 6, 16), DayOfWeek = new DateTime(2023, 6, 16).DayOfWeek.ToString() },
                new ShiftAssignment { AssignmentID = Guid.NewGuid(), FkStaffID = guard3.StaffID, FkShiftID = guardShift2.ShiftID, Date = new DateTime(2023, 6, 17), DayOfWeek = new DateTime(2023, 6, 17).DayOfWeek.ToString() }
            );

            // INSERT DATA FOR E-COMMERCE
            // Insert rows for Customer
            var customer1 = new Customer { CustomerID = Guid.NewGuid(), Username = "customer1", Email = "customer1@gmail.com", PhoneNumber = "1234567890", Password = "Password1#", Dob = new DateTime(1990, 1, 13), Address = "Address X", Balance = 3000};
            var customer2 = new Customer { CustomerID = Guid.NewGuid(), Username = "customer2", Email = "customer2@gmail.com", PhoneNumber = "1234567890", Password = "Password2#", Dob = new DateTime(1990, 1, 14), Address = "Address X", Balance = 5000};
            var customer3 = new Customer { CustomerID = Guid.NewGuid(), Username = "customer3", Email = "customer3@gmail.com", PhoneNumber = "1234567890", Password = "Password3#", Dob = new DateTime(1990, 1, 15), Address = "Address X", Balance = 7000};

            modelBuilder.Entity<Customer>().HasData(
                customer1,
                customer2,
                customer3
            );

            // Insert rows for Voucher
            var voucher10 = new Voucher { VoucherID = Guid.NewGuid(), Code = "DISCOUNT10", DiscountPercentage = (decimal)0.1 };
            var voucher30 = new Voucher { VoucherID = Guid.NewGuid(), Code = "DISCOUNT30", DiscountPercentage = (decimal)0.3 };
            var voucher50 = new Voucher { VoucherID = Guid.NewGuid(), Code = "DISCOUNT50", DiscountPercentage = (decimal)0.5 };

            modelBuilder.Entity<Voucher>().HasData(
                voucher10,
                voucher30,
                voucher50
            );

            // Insert rows for Category
            var categoryMac = new Category { CategoryID = Guid.NewGuid(), CategoryName = "Mac" };
            var categoryiPad = new Category { CategoryID = Guid.NewGuid(), CategoryName = "iPad" };
            var categoryiPhone = new Category { CategoryID = Guid.NewGuid(), CategoryName = "iPhone" };
            var categoryWatch = new Category { CategoryID = Guid.NewGuid(), CategoryName = "Watch" };
            var categoryAirPods = new Category { CategoryID = Guid.NewGuid(), CategoryName = "AirPods" };

            modelBuilder.Entity<Category>().HasData(
                categoryMac,
                categoryiPad,
                categoryiPhone,
                categoryWatch,
                categoryAirPods
            );

            // Insert rows for Products
            // Mac
            var macBookAirM1 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "MacBook Air (M1, 2020)",
                ProductImg = "images/products/Mac/MacBook Air (M1, 2020).jpg",
                ProductDescription = "Experience the power and efficiency of the Apple M1 chip with the MacBook Air (M1, 2020). This lightweight and portable laptop deliver incredible performance and battery life. With its stunning Retina display, Magic Keyboard, and spacious trackpad, the MacBook Air provides an immersive and comfortable user experience. Whether you're browsing the web, editing photos, or working on intensive tasks, the MacBook Air is up to the challenge.",
                ProductBrand = "Apple",
                ProductPrice = 999.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var macBookAirM2 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "MacBook Air (M2, 2022)",
                ProductImg = "images/products/Mac/MacBook Air (M2, 2022).png",
                ProductDescription = "Unleash your creativity and productivity with the MacBook Air (M2, 2022). Powered by the next-generation M2 chip, this laptop delivers exceptional performance and energy efficiency. The stunning Retina display with True Tone technology brings your content to life with vibrant colors and sharp details. With its sleek and portable design, comfortable keyboard, and all-day battery life, the MacBook Air is the perfect companion for your everyday tasks and creative endeavors.",
                ProductBrand = "Apple",
                ProductPrice = 1199.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var macBookPro13M2 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "MacBook Pro (13-inch, M2, 2022)",
                ProductImg = "images/products/Mac/MacBook Pro (13-inch, M2, 2022).png",
                ProductDescription = "Take your productivity to new heights with the MacBook Pro (13-inch, M2, 2022). Featuring the powerful M2 chip and advanced graphics, this laptop delivers incredible performance for demanding tasks and creative projects. The stunning Retina display with ProMotion technology provides smooth visuals and precise color reproduction. With its innovative Touch Bar, responsive keyboard, and immersive sound, the MacBook Pro (13-inch, M2, 2022) is designed to elevate your work and entertainment experience.",
                ProductBrand = "Apple",
                ProductPrice = 1299.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var macBookPro14 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "MacBook Pro (14-inch, 2023)",
                ProductImg = "images/products/Mac/MacBook Pro (14-inch, 2023).png",
                ProductDescription = "Experience the future of professional computing with the MacBook Pro (14-inch, 2023). This redesigned laptop features a stunning 14-inch Liquid Retina XDR display with mini-LED backlighting, offering exceptional brightness and contrast. Powered by the latest generation of Apple silicon, it delivers incredible performance and efficiency. With its advanced thermal system, redesigned keyboard, and immersive audio, the MacBook Pro (14-inch, 2023) is the ultimate tool for professionals and power users.",
                ProductBrand = "Apple",
                ProductPrice = 1999.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var macBookPro16 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "MacBook Pro (16-inch, 2023)",
                ProductImg = "images/products/Mac/MacBook Pro (16-inch, 2023).png",
                ProductDescription = "Get ready for the most powerful MacBook Pro ever with the MacBook Pro (16-inch, 2023). Equipped with the latest Intel processors, advanced graphics, and a stunning Retina display, this laptop is built to handle the most demanding tasks and deliver incredible performance. With its immersive sound, spacious trackpad, and comfortable keyboard, the MacBook Pro (16-inch, 2023) provides an unparalleled user experience for professionals and creative enthusiasts.",
                ProductBrand = "Apple",
                ProductPrice = 2499.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var macMini2023 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "Mac mini (2023)",
                ProductImg = "images/products/Mac/Mac mini (2023).png",
                ProductDescription = "Experience desktop power in a compact form factor with the Mac mini (2023). This versatile desktop computer packs incredible performance and connectivity options into a small and stylish design. Whether you're using it for everyday tasks, creative projects, or as a home media center, the Mac mini (2023) delivers exceptional speed and responsiveness. With its advanced thermal architecture and wide range of ports, you can connect a variety of peripherals and unleash your productivity.",
                ProductBrand = "Apple",
                ProductPrice = 499.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var macStudio2023 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "Mac Studio (2023)",
                ProductImg = "images/products/Mac/Mac Studio (2023).png",
                ProductDescription = "Discover the power and versatility of the Mac Studio (2023). This all-in-one desktop computer combines performance, display, and audio into a seamless and immersive experience. With its stunning 32-inch Retina display, powerful M1 chip, and studio-quality speakers, the Mac Studio is designed for professionals in creative fields such as design, photography, and music production. Experience the future of desktop computing with the Mac Studio (2023).",
                ProductBrand = "Apple",
                ProductPrice = 1999.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var macProM2Ultra = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "Mac Pro (M2 Ultra)",
                ProductImg = "images/products/Mac/Mac Pro(M2 Ultra).jpg",
                ProductDescription = "Unleash your creativity with the Mac Pro (M2 Ultra). This high-performance desktop computer is designed for professionals who demand the ultimate in processing power and expansion capabilities. With the next-generation M2 Ultra chip, advanced graphics, and modular design, the Mac Pro delivers unmatched performance for tasks such as 3D rendering, video editing, and scientific simulations. Customize your system with powerful GPUs, high-speed storage, and vast amounts of memory to create a workstation that meets your specific needs.",
                ProductBrand = "Apple",
                ProductPrice = 6999.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iMacM1 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryMac.CategoryID,
                ProductName = "iMac (24-inch, M1, 2021)",
                ProductImg = "images/products/Mac/iMac (24-inch, M1, 2021).jpg",
                ProductDescription = "Experience a new level of performance and design with the iMac (24-inch, M1, 2021). Powered by the Apple M1 chip, this all-in-one desktop computer delivers remarkable speed and efficiency. The 24-inch Retina display with True Tone technology brings your content to life with vibrant colors and sharp details. With its sleek and thin design, enhanced audio capabilities, and advanced camera and microphone system, the iMac (24-inch, M1, 2021) is perfect for work, creativity, and entertainment.",
                ProductBrand = "Apple",
                ProductPrice = 1499.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            modelBuilder.Entity<Product>().HasData(
                macBookAirM1,
                macBookAirM2,
                macBookPro13M2,
                macBookPro14,
                macBookPro16,
                macMini2023,
                macStudio2023,
                macProM2Ultra,
                iMacM1
            );

            // iPad
            var iPadMini6 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPad.CategoryID,
                ProductName = "iPad mini (6th generation)",
                ProductImg = "images/products/iPad/iPad mini (6th generation).png",
                ProductDescription = "Experience the power of iPad in a compact form with the iPad mini (6th generation). Featuring the A15 Bionic chip, this tablet delivers incredible performance and graphics capabilities. With its 8.3-inch Liquid Retina display, Apple Pencil support, and advanced cameras, the iPad mini is perfect for gaming, content creation, and on-the-go productivity.",
                ProductBrand = "Apple",
                ProductPrice = 499.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPad9 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPad.CategoryID,
                ProductName = "iPad (9th generation)",
                ProductImg = "images/products/iPad/iPad (9th generation).png",
                ProductDescription = "Discover the versatility of the iPad (9th generation). Powered by the A13 Bionic chip, this tablet offers impressive performance for both work and play. Its 10.2-inch Retina display provides a stunning canvas for your content, while features like Apple Pencil support and the Smart Keyboard make it even more versatile and productive.",
                ProductBrand = "Apple",
                ProductPrice = 459.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPad10 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPad.CategoryID,
                ProductName = "iPad (10th generation)",
                ProductImg = "images/products/iPad/iPad (10th generation).png",
                ProductDescription = "Experience the next generation of iPad with the iPad (10th generation). Powered by the A14 Bionic chip, this tablet delivers remarkable performance and graphics capabilities. Its 10.2-inch Retina display provides a stunning visual experience, while features like Apple Pencil support and the Smart Keyboard make it even more versatile and productive.",
                ProductBrand = "Apple",
                ProductPrice = 599.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPadAir5 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPad.CategoryID,
                ProductName = "iPad Air (5th generation)",
                ProductImg = "images/products/iPad/iPad Air (5th generation).png",
                ProductDescription = "Unleash your creativity with the iPad Air (5th generation). Powered by the A15 Bionic chip, this tablet offers incredible performance and graphics capabilities. Its 10.9-inch Liquid Retina display with True Tone brings your content to life, while features like Apple Pencil support and the Magic Keyboard enhance your productivity and creativity.",
                ProductBrand = "Apple",
                ProductPrice = 599.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPadPro11_4 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPad.CategoryID,
                ProductName = "iPad Pro 11-inch (4th generation)",
                ProductImg = "images/products/iPad/iPad Pro 11-inch (4th generation).png",
                ProductDescription = "Experience the ultimate iPad performance with the iPad Pro 11-inch (4th generation). Powered by the M1 chip, this tablet delivers unmatched speed and power. Its stunning Liquid Retina XDR display with ProMotion technology offers true-to-life colors and smooth scrolling. With advanced features like Face ID, Apple Pencil support, and the Magic Keyboard, the iPad Pro 11-inch is a powerful tool for creative professionals and productivity enthusiasts.",
                ProductBrand = "Apple",
                ProductPrice = 799.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPadPro12_9_6 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPad.CategoryID,
                ProductName = "iPad Pro 12.9-inch (6th generation)",
                ProductImg = "images/products/iPad/iPad Pro 12.9-inch (6th generation).png",
                ProductDescription = "Immerse yourself in the ultimate iPad experience with the iPad Pro 12.9-inch (6th generation). Powered by the M1 chip, this tablet delivers unprecedented performance and graphics capabilities. Its breathtaking Liquid Retina XDR display with ProMotion technology provides stunning visuals with HDR content support. With advanced features like Face ID, Apple Pencil support, and the Magic Keyboard, the iPad Pro 12.9-inch is the perfect companion for creative professionals and demanding tasks.",
                ProductBrand = "Apple",
                ProductPrice = 1099.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            modelBuilder.Entity<Product>().HasData(
                iPadMini6,
                iPad9,
                iPad10,
                iPadAir5,
                iPadPro11_4,
                iPadPro12_9_6
            );

            // iPhone
            var iPhoneSE3 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone SE (3rd generation)",
                ProductImg = "images/products/iPhone/iPhone SE (3rd generation).png",
                ProductDescription = "Discover the power of the compact iPhone SE (3rd generation). Featuring the A15 Bionic chip, this phone delivers remarkable performance and graphics capabilities. Its 4.7-inch Retina HD display and advanced camera system make it perfect for capturing stunning photos and videos. With features like Touch ID and wireless charging, the iPhone SE is a powerful device in a compact form factor.",
                ProductBrand = "Apple",
                ProductPrice = 429.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPhone13Mini = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone 13 mini",
                ProductImg = "images/products/iPhone/iPhone 13 mini.png",
                ProductDescription = "Experience the power of the iPhone 13 mini. Powered by the A15 Bionic chip, this phone offers incredible performance and efficiency. Its 5.4-inch Super Retina XDR display provides stunning visuals, while the advanced camera system allows you to capture professional-quality photos and videos. With features like Face ID, 5G connectivity, and all-day battery life, the iPhone 13 mini is a compact powerhouse.",
                ProductBrand = "Apple",
                ProductPrice = 599.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPhone13 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone 13",
                ProductImg = "images/products/iPhone/iPhone 13.png",
                ProductDescription = "Discover the exceptional capabilities of the iPhone 13. Powered by the A15 Bionic chip, this phone delivers incredible performance and efficiency. Its 6.1-inch Super Retina XDR display with ProMotion technology provides a stunning visual experience. With advanced camera features, including Night mode and Deep Fusion, you can capture professional-quality photos and videos. The iPhone 13 also offers 5G connectivity, all-day battery life, and enhanced durability.",
                ProductBrand = "Apple",
                ProductPrice = 699.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPhone13Pro = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone 13 Pro",
                ProductImg = "images/products/iPhone/iPhone 13 Pro.png",
                ProductDescription = "Experience the ultimate iPhone with the iPhone 13 Pro. Powered by the A15 Bionic chip, this phone offers unmatched performance and efficiency. Its 6.1-inch Super Retina XDR display with ProMotion technology delivers stunning visuals with HDR content support. With the advanced camera system, including the new Pro camera mode and ProRAW, you can capture and edit professional-quality photos and videos. The iPhone 13 Pro also features 5G connectivity, enhanced durability, and all-day battery life.",
                ProductBrand = "Apple",
                ProductPrice = 999.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPhone13ProMax = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone 13 Pro Max",
                ProductImg = "images/products/iPhone/iPhone 13 Pro Max.png",
                ProductDescription = "Discover the epitome of iPhone excellence with the iPhone 13 Pro Max. Powered by the A15 Bionic chip, this phone offers exceptional performance and efficiency. Its 6.7-inch Super Retina XDR display with ProMotion technology provides breathtaking visuals with HDR content support. With the advanced camera system, including the new Pro camera mode and ProRAW, you can capture and edit professional-quality photos and videos. The iPhone 13 Pro Max also features 5G connectivity, enhanced durability, and all-day battery life.",
                ProductBrand = "Apple",
                ProductPrice = 1099.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPhone14 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone 14",
                ProductImg = "images/products/iPhone/iPhone 14.png",
                ProductDescription = "Experience the future of iPhone with the iPhone 14. Powered by the next-generation A16 chip, this phone delivers unparalleled performance and efficiency. Its stunning Super Retina XDR display provides a vibrant and immersive visual experience. With advanced camera features, enhanced AI capabilities, and seamless integration with Apple's ecosystem, the iPhone 14 redefines what a smartphone can do.",
                ProductBrand = "Apple",
                ProductPrice = 799.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPhone14Plus = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone 14 Plus",
                ProductImg = "images/products/iPhone/iPhone 14 Plus.png",
                ProductDescription = "Discover the larger variant of the iPhone 14 with the iPhone 14 Plus. Powered by the next-generation A16 chip, this phone offers exceptional performance and efficiency. Its expansive Super Retina XDR display provides a stunning visual experience with true-to-life colors. With advanced camera features, enhanced AI capabilities, and a range of innovative technologies, the iPhone 14 Plus takes your smartphone experience to new heights.",
                ProductBrand = "Apple",
                ProductPrice = 899.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPhone14Pro = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone 14 Pro",
                ProductImg = "images/products/iPhone/iPhone 14 Pro.png",
                ProductDescription = "Experience the pinnacle of iPhone technology with the iPhone 14 Pro. Powered by the next-generation A16 chip, this phone offers unmatched performance and efficiency. Its Super Retina XDR display with ProMotion technology delivers breathtaking visuals with HDR content support. With the advanced camera system, powerful AI capabilities, and a suite of professional-grade features, the iPhone 14 Pro empowers you to capture, create, and explore like never before.",
                ProductBrand = "Apple",
                ProductPrice = 999.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var iPhone14ProMax = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryiPhone.CategoryID,
                ProductName = "iPhone 14 Pro Max",
                ProductImg = "images/products/iPhone/iPhone 14 Pro Max.png",
                ProductDescription = "Discover the pinnacle of iPhone excellence with the iPhone 14 Pro Max. Powered by the next-generation A16 chip, this phone delivers unparalleled performance and efficiency. Its Super Retina XDR display with ProMotion technology provides a stunning visual experience with true blacks and vibrant colors. With the advanced camera system, enhanced AI capabilities, and a range of innovative features, the iPhone 14 Pro Max is designed to elevate your smartphone experience to new heights.",
                ProductBrand = "Apple",
                ProductPrice = 1199.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            modelBuilder.Entity<Product>().HasData(
                iPhoneSE3,
                iPhone13Mini,
                iPhone13,
                iPhone13Pro,
                iPhone13ProMax,
                iPhone14,
                iPhone14Plus,
                iPhone14Pro,
                iPhone14ProMax
            );

            // Watch
            var watchSeries1 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Series 1",
                ProductImg = "images/products/Watch/Apple Watch Series 1.jpg",
                ProductDescription = "Introducing the Apple Watch Series 1, your ultimate fitness and productivity companion. With its sleek design and powerful features, this watch keeps you connected and motivated throughout the day. Stay on top of your fitness goals, track your workouts, and monitor your heart rate. Receive notifications, answer calls, and access your favorite apps right from your wrist. The Apple Watch Series 1 is the perfect blend of style and functionality.",
                ProductBrand = "Apple",
                ProductPrice = 199.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSeries2 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Series 2",
                ProductImg = "images/products/Watch/Apple Watch Series 2.jpg",
                ProductDescription = "Upgrade your wristwear with the Apple Watch Series 2. Packed with advanced features and improved performance, this watch takes your experience to the next level. Enjoy a brighter display, built-in GPS, and water resistance up to 50 meters. Track your workouts with precision, receive personalized coaching, and stay motivated throughout the day. With its stylish design and seamless integration with your iPhone, the Apple Watch Series 2 is the perfect companion for an active and connected lifestyle.",
                ProductBrand = "Apple",
                ProductPrice = 299.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSeries3 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Series 3",
                ProductImg = "images/products/Watch/Apple Watch Series 3.jpg",
                ProductDescription = "Experience the power of Apple Watch Series 3 on your wrist. With built-in cellular connectivity, you can make calls, send messages, stream music, and more, even without your iPhone nearby. Stay active and motivated with advanced fitness tracking, automatic workout detection, and heart rate monitoring. The bright and crisp display keeps you informed and engaged throughout the day. With its iconic design and seamless integration with your Apple devices, the Apple Watch Series 3 is your ultimate companion for a connected life.",
                ProductBrand = "Apple",
                ProductPrice = 349.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSeries4 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Series 4",
                ProductImg = "images/products/Watch/Apple Watch Series 4.jpg",
                ProductDescription = "Discover a new era of Apple Watch with the Series 4. With its stunning display, slimmer profile, and breakthrough health features, this watch redefines what a timepiece can do. The larger screen allows for more immersive experiences and improved readability. Track your workouts with enhanced precision, monitor your heart rate, and receive proactive health notifications. With built-in ECG and fall detection, the Apple Watch Series 4 is your intelligent health guardian on your wrist.",
                ProductBrand = "Apple",
                ProductPrice = 399.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSeries5 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Series 5",
                ProductImg = "images/products/Watch/Apple Watch Series 5.jpg",
                ProductDescription = "Experience the future of smartwatches with the Apple Watch Series 5. This watch features an Always-On Retina display that never sleeps, allowing you to check the time and glance at your important information without raising your wrist. Stay connected with built-in cellular and GPS capabilities, track your workouts with advanced sensors, and monitor your heart rate throughout the day. With its stylish design and powerful features, the Apple Watch Series 5 is the perfect blend of fashion and technology.",
                ProductBrand = "Apple",
                ProductPrice = 499.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSeries6 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Series 6",
                ProductImg = "images/products/Watch/Apple Watch Series 6.jpg",
                ProductDescription = "Embrace the future of smartwatches with the Apple Watch Series 6. Featuring advanced health sensors, this watch helps you monitor your blood oxygen levels, measure your ECG, and track your sleep patterns. Stay connected, make calls, send messages, and stream music, all without your iPhone nearby. The Always-On Retina display is now brighter and more energy-efficient, providing you with clear and vibrant visuals throughout the day. With its powerful features and stylish design, the Apple Watch Series 6 is the ultimate companion for a healthy and connected life.",
                ProductBrand = "Apple",
                ProductPrice = 599.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSeries7 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Series 7",
                ProductImg = "images/products/Watch/Apple Watch Series 7.jpeg",
                ProductDescription = "Experience the next generation of Apple Watch with the Series 7. With its larger display, sleek design, and powerful features, this watch is the epitome of innovation and style. The Always-On Retina display is now even more expansive, allowing for more information and greater readability. Stay connected, track your workouts, monitor your health, and access your favorite apps with ease. With its durable construction and advanced technologies, the Apple Watch Series 7 is built to accompany you on all of life's adventures.",
                ProductBrand = "Apple",
                ProductPrice = 699.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSeries8 = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Series 8",
                ProductImg = "images/products/Watch/Apple Watch Series 8.jpeg",
                ProductDescription = "Introducing the Apple Watch Series 8, the next generation of smartwatches. With its advanced features and sleek design, this watch is a true technological marvel. The stunning always-on display is larger and brighter, providing crystal-clear visuals and enhanced readability. Track your fitness goals, monitor your health, and stay connected with ease. The Apple Watch Series 8 is built to elevate your daily life and empower you to achieve more.",
                ProductBrand = "Apple",
                ProductPrice = 1499.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSE1stGen = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch SE (1st generation)",
                ProductImg = "images/products/Watch/Apple Watch SE (1st generation).jpeg",
                ProductDescription = "Get the best of Apple Watch at an affordable price with the Apple Watch SE (1st generation). Featuring a powerful dual-core processor, this watch delivers fast performance and seamless connectivity. Track your workouts, monitor your heart rate, and stay motivated with advanced fitness features. With its sleek design and stunning Retina display, the Apple Watch SE (1st generation) is the perfect blend of style and functionality.",
                ProductBrand = "Apple",
                ProductPrice = 149.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchSE2ndGen = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch SE (2nd generation)",
                ProductImg = "images/products/Watch/Apple Watch SE (2nd generation).jpeg",
                ProductDescription = "Introducing the Apple Watch SE (2nd generation), the perfect companion for your active lifestyle. With its powerful features and affordable price, this watch offers a seamless experience. Stay connected, track your workouts, and monitor your health with ease. With its stylish design and expansive Retina display, the Apple Watch SE (2nd generation) is the ultimate fusion of affordability and performance.",
                ProductBrand = "Apple",
                ProductPrice = 249.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var watchUltra = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryWatch.CategoryID,
                ProductName = "Apple Watch Ultra",
                ProductImg = "images/products/Watch/Apple Watch Ultra.jpg",
                ProductDescription = "Experience the pinnacle of smartwatch technology with the Apple Watch Ultra. This watch pushes the boundaries of innovation with its cutting-edge features and premium design. Enjoy a larger and more vibrant display, enhanced health tracking capabilities, and seamless integration with your favorite apps and services. Stay connected, monitor your fitness, and elevate your productivity with the Apple Watch Ultra.",
                ProductBrand = "Apple",
                ProductPrice = 799.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            modelBuilder.Entity<Product>().HasData(
                watchSeries1,
                watchSeries2,
                watchSeries3,
                watchSeries4,
                watchSeries5,
                watchSeries6,
                watchSeries7,
                watchSeries8,
                watchSE1stGen,
                watchSE2ndGen,
                watchUltra
            );

            // AirPods
            var airPods1stGen = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryAirPods.CategoryID,
                ProductName = "AirPods (1st generation)",
                ProductImg = "images/products/AirPods/AirPods (1st generation).jpeg",
                ProductDescription = "Experience the freedom of wireless listening with AirPods (1st generation). These innovative earbuds offer quick and easy setup, delivering high-quality audio with remarkable clarity. Seamlessly switch between your Apple devices and enjoy the convenience of hands-free Siri access. Whether you're listening to music, watching movies, or taking calls, AirPods (1st generation) provide a truly wireless and immersive audio experience.",
                ProductBrand = "Apple",
                ProductPrice = 129.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var airPods2ndGen = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryAirPods.CategoryID,
                ProductName = "AirPods (2nd generation)",
                ProductImg = "images/products/AirPods/AirPods (2nd generation).png",
                ProductDescription = "Upgrade your audio experience with AirPods (2nd generation). Powered by the advanced H1 chip, these wireless earbuds offer faster and more stable connections, reducing latency and enhancing overall performance. With longer talk time and hands-free Siri access, you can effortlessly manage your music, make calls, and control your devices with a simple voice command. The sleek design and comfortable fit make AirPods (2nd generation) the perfect companion for your active lifestyle.",
                ProductBrand = "Apple",
                ProductPrice = 159.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var airPods3rdGen = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryAirPods.CategoryID,
                ProductName = "AirPods (3rd generation)",
                ProductImg = "images/products/AirPods/AirPods (3rd generation).png",
                ProductDescription = "Immerse yourself in sound with AirPods (3rd generation). These cutting-edge wireless earbuds feature a redesigned shape and fit, providing enhanced comfort and a more secure listening experience. Enjoy immersive audio with active noise cancellation, which blocks out external distractions and allows you to focus on your music or calls. AirPods (3rd generation) also introduce spatial audio, delivering a truly immersive soundstage that surrounds you. With Adaptive EQ and seamless device switching, these earbuds elevate your audio experience to new heights.",
                ProductBrand = "Apple",
                ProductPrice = 179.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var airPodsPro1stGen = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryAirPods.CategoryID,
                ProductName = "AirPods Pro (1st generation)",
                ProductImg = "images/products/AirPods/AirPods Pro (1st generation).png",
                ProductDescription = "Step into the world of premium audio with AirPods Pro (1st generation). These exceptional wireless earbuds feature active noise cancellation, allowing you to block out unwanted noise and fully immerse yourself in your music or calls. The customizable fit, achieved through three sizes of soft, tapered silicone tips, ensures a secure and comfortable seal in your ear. Experience rich and detailed sound with Adaptive EQ, which automatically tunes the music to the shape of your ear. With transparency mode, you can easily switch to hear your surroundings when needed. AirPods Pro (1st generation) redefine the way you listen.",
                ProductBrand = "Apple",
                ProductPrice = 249.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var airPodsPro2ndGen = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryAirPods.CategoryID,
                ProductName = "AirPods Pro (2nd generation)",
                ProductImg = "images/products/AirPods/AirPods Pro (2nd generation).png",
                ProductDescription = "Elevate your audio experience with AirPods Pro (2nd generation). These advanced wireless earbuds build upon the success of the previous generation, offering enhanced features and performance. Enjoy the immersive sound of active noise cancellation, which intelligently adapts to your environment, providing an uninterrupted listening experience. The customizable fit, achieved through three sizes of soft, tapered silicone tips, ensures a comfortable and secure seal for hours of listening pleasure. With spatial audio and Adaptive EQ, AirPods Pro (2nd generation) deliver a truly immersive and personalized soundstage. Stay connected, enjoy hands-free Siri access, and embrace the future of wireless audio.",
                ProductBrand = "Apple",
                ProductPrice = 279.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            var airPodsMax = new Product
            {
                ProductID = Guid.NewGuid(),
                FkCategoryID = categoryAirPods.CategoryID,
                ProductName = "AirPods Max",
                ProductImg = "images/products/AirPods/AirPods Max.png",
                ProductDescription = "Experience audio excellence with AirPods Max. These over-ear headphones combine high-fidelity audio with the convenience of Apple's ecosystem. Equipped with Adaptive EQ and spatial audio, AirPods Max deliver a truly immersive and captivating listening experience. The Active Noise Cancellation technology blocks out external distractions, allowing you to focus on the rich and detailed sound. With a comfortable and premium design, the AirPods Max are built to provide hours of luxurious listening pleasure. Enjoy seamless device switching, hands-free Siri access, and up to 20 hours of battery life. AirPods Max redefine what you can expect from a pair of headphones.",
                ProductBrand = "Apple",
                ProductPrice = 549.99m,
                ProductQuantity = 50,
                ProductStatus = true
            };

            modelBuilder.Entity<Product>().HasData(
                airPods1stGen,
                airPods2ndGen,
                airPods3rdGen,
                airPodsPro1stGen,
                airPodsPro2ndGen,
                airPodsMax
            );
        }
    }
}
