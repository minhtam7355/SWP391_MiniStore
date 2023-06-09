using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SWP391_MiniStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategories",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "tblCustomers",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCustomers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "tblStoreStaff",
                columns: table => new
                {
                    StaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    StaffStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStoreStaff", x => x.StaffID);
                });

            migrationBuilder.CreateTable(
                name: "tblVouchers",
                columns: table => new
                {
                    VoucherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVouchers", x => x.VoucherID);
                });

            migrationBuilder.CreateTable(
                name: "tblWorkShifts",
                columns: table => new
                {
                    ShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Holiday = table.Column<bool>(type: "bit", nullable: true),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    WorkHours = table.Column<int>(type: "int", nullable: true),
                    SalaryCoef = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWorkShifts", x => x.ShiftID);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    ProductQuantity = table.Column<int>(type: "int", nullable: true),
                    ProductStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_tblProducts_tblCategories_FkCategoryID",
                        column: x => x.FkCategoryID,
                        principalTable: "tblCategories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "tblAttendances",
                columns: table => new
                {
                    AttendanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAttendances", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_tblAttendances_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                });

            migrationBuilder.CreateTable(
                name: "tblLeaveApplications",
                columns: table => new
                {
                    LeaveApplicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeaveStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaveEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaveReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaveApprovalStatus = table.Column<bool>(type: "bit", nullable: true),
                    LeaveApprovalReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLeaveApplications", x => x.LeaveApplicationID);
                    table.ForeignKey(
                        name: "FK_tblLeaveApplications_tblStoreStaff_FkManagerID",
                        column: x => x.FkManagerID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                    table.ForeignKey(
                        name: "FK_tblLeaveApplications_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                });

            migrationBuilder.CreateTable(
                name: "tblPayrolls",
                columns: table => new
                {
                    PayrollID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Payslip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossPay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    NetPay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayrolls", x => x.PayrollID);
                    table.ForeignKey(
                        name: "FK_tblPayrolls_tblStoreStaff_FkManagerID",
                        column: x => x.FkManagerID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                    table.ForeignKey(
                        name: "FK_tblPayrolls_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                });

            migrationBuilder.CreateTable(
                name: "tblOrders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkCustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkVoucherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderTotalBeforeVoucher = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    OrderTotalAfterVoucher = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: true),
                    OrderApprovalStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_tblOrders_tblCustomers_FkCustomerID",
                        column: x => x.FkCustomerID,
                        principalTable: "tblCustomers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_tblOrders_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                    table.ForeignKey(
                        name: "FK_tblOrders_tblVouchers_FkVoucherID",
                        column: x => x.FkVoucherID,
                        principalTable: "tblVouchers",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "tblShiftAssignments",
                columns: table => new
                {
                    AssignmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShiftAssignments", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_tblShiftAssignments_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                    table.ForeignKey(
                        name: "FK_tblShiftAssignments_tblWorkShifts_FkShiftID",
                        column: x => x.FkShiftID,
                        principalTable: "tblWorkShifts",
                        principalColumn: "ShiftID");
                });

            migrationBuilder.CreateTable(
                name: "tblFeedbacks",
                columns: table => new
                {
                    FeedbackID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkCustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFeedbacks", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_tblFeedbacks_tblCustomers_FkCustomerID",
                        column: x => x.FkCustomerID,
                        principalTable: "tblCustomers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_tblFeedbacks_tblProducts_FkProductID",
                        column: x => x.FkProductID,
                        principalTable: "tblProducts",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "tblOrderDetails",
                columns: table => new
                {
                    FkOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderedProductPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    OrderedProductQuantity = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderDetails", x => new { x.FkOrderID, x.FkProductID });
                    table.ForeignKey(
                        name: "FK_tblOrderDetails_tblOrders_FkOrderID",
                        column: x => x.FkOrderID,
                        principalTable: "tblOrders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblOrderDetails_tblProducts_FkProductID",
                        column: x => x.FkProductID,
                        principalTable: "tblProducts",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblCategories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Watch" },
                    { new Guid("88063adf-151c-41e6-a8d4-53f5a5fad4e5"), "iPad" },
                    { new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "iPhone" },
                    { new Guid("c6406d0a-9199-478e-9753-efbfc08a61f5"), "AirPods" },
                    { new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Mac" }
                });

            migrationBuilder.InsertData(
                table: "tblCustomers",
                columns: new[] { "CustomerID", "Address", "Balance", "Dob", "Email", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("26db4faf-d106-407a-8f85-ee3f7e5bb21d"), "Address X", 3000m, new DateTime(1990, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer1@gmail.com", "Password1#", "1234567890", "customer1" },
                    { new Guid("72d43499-5f35-488b-b239-7b0584f2a7c6"), "Address X", 7000m, new DateTime(1990, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer3@gmail.com", "Password3#", "1234567890", "customer3" },
                    { new Guid("9e362b75-79b5-4408-9269-768614296389"), "Address X", 5000m, new DateTime(1990, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer2@gmail.com", "Password2#", "1234567890", "customer2" }
                });

            migrationBuilder.InsertData(
                table: "tblStoreStaff",
                columns: new[] { "StaffID", "Address", "Dob", "Email", "HourlyRate", "Password", "PhoneNumber", "StaffRole", "StaffStatus", "Username" },
                values: new object[,]
                {
                    { new Guid("45dd31d8-5738-488f-ab2d-7cfb1fc98e12"), "Address 7", new DateTime(1990, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "guard1@gmail.com", 17m, "Password7#", "5555555555", "Guard", true, "guard1" },
                    { new Guid("822f703e-05ac-4811-8817-e25bf0b708bd"), "Address 9", new DateTime(1990, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "guard3@gmail.com", 21m, "Password9#", "7777777777", "Guard", true, "guard3" },
                    { new Guid("85425342-2db3-4842-961e-b1e0030410ac"), "Address 6", new DateTime(1990, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "sales3@gmail.com", 43m, "Password6#", "4444444444", "Sales", true, "sales3" },
                    { new Guid("9252ed85-31c9-4338-8795-94db97cc1dc6"), "Address 2", new DateTime(1990, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "manager2@gmail.com", 63m, "Password2#", "9876543210", "Manager", true, "manager2" },
                    { new Guid("9d3a2374-39d9-49ca-874d-8eaa3cb5978f"), "Address 1", new DateTime(1990, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "manager1@gmail.com", 53m, "Password1#", "1234567890", "Manager", true, "manager1" },
                    { new Guid("a25afe31-a7ea-4a5f-94c9-d8a26d46281e"), "Address 3", new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "manager3@gmail.com", 59m, "Password3#", "1111111111", "Manager", true, "manager3" },
                    { new Guid("a7931c2e-5f52-49bd-9879-5545b93c5b1f"), "Address 4", new DateTime(1990, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "sales1@gmail.com", 36m, "Password4#", "2222222222", "Sales", true, "sales1" },
                    { new Guid("b1ff55b3-6864-4a68-ae07-f0d2d92588f2"), "Address 8", new DateTime(1990, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "guard2@gmail.com", 27m, "Password8#", "6666666666", "Guard", true, "guard2" },
                    { new Guid("cdc56d5f-8405-49c2-91ab-0fe7bb5e2ba0"), "Address 5", new DateTime(1990, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "sales2@gmail.com", 38m, "Password5#", "3333333333", "Sales", true, "sales2" }
                });

            migrationBuilder.InsertData(
                table: "tblVouchers",
                columns: new[] { "VoucherID", "Code", "DiscountPercentage" },
                values: new object[,]
                {
                    { new Guid("44eaec69-1152-4982-88db-2fcf93b420ae"), "DISCOUNT30", 0.3m },
                    { new Guid("bc8aa7a1-25df-41c8-8005-7bfb55996d59"), "DISCOUNT50", 0.5m },
                    { new Guid("c0275e96-dd48-43e7-8ea1-f83837d5f993"), "DISCOUNT10", 0.1m }
                });

            migrationBuilder.InsertData(
                table: "tblWorkShifts",
                columns: new[] { "ShiftID", "DayOfWeek", "EndTime", "Holiday", "SalaryCoef", "StaffRole", "StartTime", "WorkHours" },
                values: new object[,]
                {
                    { new Guid("1d8dd322-0f51-4bea-ab40-f7c56f97308a"), null, new TimeSpan(0, 12, 0, 0, 0), false, 1m, "Sales", new TimeSpan(0, 6, 0, 0, 0), 6 },
                    { new Guid("26f57e90-92d5-4e89-a0f3-59b9a062c130"), null, new TimeSpan(0, 6, 0, 0, 0), false, 1.5m, "Sales", new TimeSpan(0, 18, 0, 0, 0), 12 },
                    { new Guid("439ecd08-a898-4748-87ff-441fa256f6d0"), null, new TimeSpan(0, 18, 0, 0, 0), false, 1m, "Sales", new TimeSpan(0, 12, 0, 0, 0), 6 },
                    { new Guid("6bf04167-bc09-41ca-92b0-bcd7073d6b02"), null, new TimeSpan(0, 6, 0, 0, 0), true, 3m, "Sales", new TimeSpan(0, 18, 0, 0, 0), 12 },
                    { new Guid("8c4e7eec-68f8-4a89-932a-5e89b577170a"), "Sunday", new TimeSpan(0, 6, 0, 0, 0), false, 2m, "Sales", new TimeSpan(0, 18, 0, 0, 0), 12 },
                    { new Guid("dde4e4dd-ee82-4f6e-839c-1ae187eda794"), null, new TimeSpan(0, 6, 0, 0, 0), false, 1.5m, "Guard", new TimeSpan(0, 18, 0, 0, 0), 12 },
                    { new Guid("eefb372d-7fab-4e12-a045-795589d8a3f2"), null, new TimeSpan(0, 18, 0, 0, 0), false, 1m, "Guard", new TimeSpan(0, 6, 0, 0, 0), 12 }
                });

            migrationBuilder.InsertData(
                table: "tblProducts",
                columns: new[] { "ProductID", "FkCategoryID", "ProductBrand", "ProductDescription", "ProductImg", "ProductName", "ProductPrice", "ProductQuantity", "ProductStatus" },
                values: new object[,]
                {
                    { new Guid("02147554-b8be-4b1d-9709-f3bdfa8dfe79"), new Guid("c6406d0a-9199-478e-9753-efbfc08a61f5"), "Apple", "Experience audio excellence with AirPods Max. These over-ear headphones combine high-fidelity audio with the convenience of Apple's ecosystem. Equipped with Adaptive EQ and spatial audio, AirPods Max deliver a truly immersive and captivating listening experience. The Active Noise Cancellation technology blocks out external distractions, allowing you to focus on the rich and detailed sound. With a comfortable and premium design, the AirPods Max are built to provide hours of luxurious listening pleasure. Enjoy seamless device switching, hands-free Siri access, and up to 20 hours of battery life. AirPods Max redefine what you can expect from a pair of headphones.", "~/images/products/AirPods/AirPods Max.png", "AirPods Max", 549.99m, 50, true },
                    { new Guid("0361f4c6-bb57-4394-ba14-ea98125ef4e1"), new Guid("88063adf-151c-41e6-a8d4-53f5a5fad4e5"), "Apple", "Experience the power of iPad in a compact form with the iPad mini (6th generation). Featuring the A15 Bionic chip, this tablet delivers incredible performance and graphics capabilities. With its 8.3-inch Liquid Retina display, Apple Pencil support, and advanced cameras, the iPad mini is perfect for gaming, content creation, and on-the-go productivity.", "~/images/products/iPad/iPad mini (6th generation).png", "iPad mini (6th generation)", 499.99m, 50, true },
                    { new Guid("09ae1a5a-6bdd-413f-ac08-77c4a3a86dc3"), new Guid("c6406d0a-9199-478e-9753-efbfc08a61f5"), "Apple", "Upgrade your audio experience with AirPods (2nd generation). Powered by the advanced H1 chip, these wireless earbuds offer faster and more stable connections, reducing latency and enhancing overall performance. With longer talk time and hands-free Siri access, you can effortlessly manage your music, make calls, and control your devices with a simple voice command. The sleek design and comfortable fit make AirPods (2nd generation) the perfect companion for your active lifestyle.", "~/images/products/AirPods/AirPods (2nd generation).png", "AirPods (2nd generation)", 159.99m, 50, true },
                    { new Guid("1baf912d-fb5b-434a-80e0-8355866288ec"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Unleash your creativity and productivity with the MacBook Air (M2, 2022). Powered by the next-generation M2 chip, this laptop delivers exceptional performance and energy efficiency. The stunning Retina display with True Tone technology brings your content to life with vibrant colors and sharp details. With its sleek and portable design, comfortable keyboard, and all-day battery life, the MacBook Air is the perfect companion for your everyday tasks and creative endeavors.", "~/images/products/Mac/MacBook Air (M2, 2022).png", "MacBook Air (M2, 2022)", 1199.99m, 50, true },
                    { new Guid("23e0d0cb-851a-4f8f-befe-affb4a8bdb90"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Get ready for the most powerful MacBook Pro ever with the MacBook Pro (16-inch, 2023). Equipped with the latest Intel processors, advanced graphics, and a stunning Retina display, this laptop is built to handle the most demanding tasks and deliver incredible performance. With its immersive sound, spacious trackpad, and comfortable keyboard, the MacBook Pro (16-inch, 2023) provides an unparalleled user experience for professionals and creative enthusiasts.", "~/images/products/Mac/MacBook Pro (16-inch, 2023).png", "MacBook Pro (16-inch, 2023)", 2499.99m, 50, true },
                    { new Guid("2affa58e-dd99-4cad-ba67-6ef4cd8d899f"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Introducing the Apple Watch Series 1, your ultimate fitness and productivity companion. With its sleek design and powerful features, this watch keeps you connected and motivated throughout the day. Stay on top of your fitness goals, track your workouts, and monitor your heart rate. Receive notifications, answer calls, and access your favorite apps right from your wrist. The Apple Watch Series 1 is the perfect blend of style and functionality.", "~/images/products/Watch/Apple Watch Series 1.jpg", "Apple Watch Series 1", 199.99m, 50, true },
                    { new Guid("2e9ed802-37df-484e-a953-53765ac2165b"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Discover the pinnacle of iPhone excellence with the iPhone 14 Pro Max. Powered by the next-generation A16 chip, this phone delivers unparalleled performance and efficiency. Its Super Retina XDR display with ProMotion technology provides a stunning visual experience with true blacks and vibrant colors. With the advanced camera system, enhanced AI capabilities, and a range of innovative features, the iPhone 14 Pro Max is designed to elevate your smartphone experience to new heights.", "~/images/products/iPhone/iPhone 14 Pro Max.png", "iPhone 14 Pro Max", 1199.99m, 50, true },
                    { new Guid("2edeb83f-dedd-4f59-99ef-e9815488e90b"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Take your productivity to new heights with the MacBook Pro (13-inch, M2, 2022). Featuring the powerful M2 chip and advanced graphics, this laptop delivers incredible performance for demanding tasks and creative projects. The stunning Retina display with ProMotion technology provides smooth visuals and precise color reproduction. With its innovative Touch Bar, responsive keyboard, and immersive sound, the MacBook Pro (13-inch, M2, 2022) is designed to elevate your work and entertainment experience.", "~/images/products/Mac/MacBook Pro (13-inch, M2, 2022).png", "MacBook Pro (13-inch, M2, 2022)", 1299.99m, 50, true },
                    { new Guid("31a04832-b590-4d70-a455-b89eae800b18"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Upgrade your wristwear with the Apple Watch Series 2. Packed with advanced features and improved performance, this watch takes your experience to the next level. Enjoy a brighter display, built-in GPS, and water resistance up to 50 meters. Track your workouts with precision, receive personalized coaching, and stay motivated throughout the day. With its stylish design and seamless integration with your iPhone, the Apple Watch Series 2 is the perfect companion for an active and connected lifestyle.", "~/images/products/Watch/Apple Watch Series 2.jpg", "Apple Watch Series 2", 299.99m, 50, true },
                    { new Guid("33f4dc42-e30e-4c44-88b8-21b83019d853"), new Guid("c6406d0a-9199-478e-9753-efbfc08a61f5"), "Apple", "Step into the world of premium audio with AirPods Pro (1st generation). These exceptional wireless earbuds feature active noise cancellation, allowing you to block out unwanted noise and fully immerse yourself in your music or calls. The customizable fit, achieved through three sizes of soft, tapered silicone tips, ensures a secure and comfortable seal in your ear. Experience rich and detailed sound with Adaptive EQ, which automatically tunes the music to the shape of your ear. With transparency mode, you can easily switch to hear your surroundings when needed. AirPods Pro (1st generation) redefine the way you listen.", "~/images/products/AirPods/AirPods Pro (1st generation).png", "AirPods Pro (1st generation)", 249.99m, 50, true },
                    { new Guid("36c40ba3-a786-476e-9c71-b2d50e24f754"), new Guid("c6406d0a-9199-478e-9753-efbfc08a61f5"), "Apple", "Experience the freedom of wireless listening with AirPods (1st generation). These innovative earbuds offer quick and easy setup, delivering high-quality audio with remarkable clarity. Seamlessly switch between your Apple devices and enjoy the convenience of hands-free Siri access. Whether you're listening to music, watching movies, or taking calls, AirPods (1st generation) provide a truly wireless and immersive audio experience.", "~/images/products/AirPods/AirPods (1st generation).jpeg", "AirPods (1st generation)", 129.99m, 50, true },
                    { new Guid("3b7b84ae-9f91-4339-a419-022b151c183b"), new Guid("88063adf-151c-41e6-a8d4-53f5a5fad4e5"), "Apple", "Experience the ultimate iPad performance with the iPad Pro 11-inch (4th generation). Powered by the M1 chip, this tablet delivers unmatched speed and power. Its stunning Liquid Retina XDR display with ProMotion technology offers true-to-life colors and smooth scrolling. With advanced features like Face ID, Apple Pencil support, and the Magic Keyboard, the iPad Pro 11-inch is a powerful tool for creative professionals and productivity enthusiasts.", "~/images/products/iPad/iPad Pro 11-inch (4th generation).png", "iPad Pro 11-inch (4th generation)", 799.99m, 50, true },
                    { new Guid("41e6e2aa-7289-4dbc-9567-77f494fda73c"), new Guid("88063adf-151c-41e6-a8d4-53f5a5fad4e5"), "Apple", "Immerse yourself in the ultimate iPad experience with the iPad Pro 12.9-inch (6th generation). Powered by the M1 chip, this tablet delivers unprecedented performance and graphics capabilities. Its breathtaking Liquid Retina XDR display with ProMotion technology provides stunning visuals with HDR content support. With advanced features like Face ID, Apple Pencil support, and the Magic Keyboard, the iPad Pro 12.9-inch is the perfect companion for creative professionals and demanding tasks.", "~/images/products/iPad/iPad Pro 12.9-inch (6th generation).png", "iPad Pro 12.9-inch (6th generation)", 1099.99m, 50, true },
                    { new Guid("4bf78ae3-62c0-4bcd-bfb0-9a480800a87d"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Unleash your creativity with the Mac Pro (M2 Ultra). This high-performance desktop computer is designed for professionals who demand the ultimate in processing power and expansion capabilities. With the next-generation M2 Ultra chip, advanced graphics, and modular design, the Mac Pro delivers unmatched performance for tasks such as 3D rendering, video editing, and scientific simulations. Customize your system with powerful GPUs, high-speed storage, and vast amounts of memory to create a workstation that meets your specific needs.", "~/images/products/Mac/Mac Pro(M2 Ultra).jpg", "Mac Pro (M2 Ultra)", 6999.99m, 50, true },
                    { new Guid("506fdf9d-806a-4174-8c0b-41ee4879b62f"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Discover the exceptional capabilities of the iPhone 13. Powered by the A15 Bionic chip, this phone delivers incredible performance and efficiency. Its 6.1-inch Super Retina XDR display with ProMotion technology provides a stunning visual experience. With advanced camera features, including Night mode and Deep Fusion, you can capture professional-quality photos and videos. The iPhone 13 also offers 5G connectivity, all-day battery life, and enhanced durability.", "~/images/products/iPhone/iPhone 13.png", "iPhone 13", 699.99m, 50, true },
                    { new Guid("59e76ce5-746e-4ffe-85e7-422a0fcc73f1"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Experience the pinnacle of smartwatch technology with the Apple Watch Ultra. This watch pushes the boundaries of innovation with its cutting-edge features and premium design. Enjoy a larger and more vibrant display, enhanced health tracking capabilities, and seamless integration with your favorite apps and services. Stay connected, monitor your fitness, and elevate your productivity with the Apple Watch Ultra.", "~/images/products/Watch/Apple Watch Ultra.jpg", "Apple Watch Ultra", 799.99m, 50, true },
                    { new Guid("5a7c2cda-40c4-43f3-8922-1954357df59a"), new Guid("c6406d0a-9199-478e-9753-efbfc08a61f5"), "Apple", "Immerse yourself in sound with AirPods (3rd generation). These cutting-edge wireless earbuds feature a redesigned shape and fit, providing enhanced comfort and a more secure listening experience. Enjoy immersive audio with active noise cancellation, which blocks out external distractions and allows you to focus on your music or calls. AirPods (3rd generation) also introduce spatial audio, delivering a truly immersive soundstage that surrounds you. With Adaptive EQ and seamless device switching, these earbuds elevate your audio experience to new heights.", "~/images/products/AirPods/AirPods (3rd generation).png", "AirPods (3rd generation)", 179.99m, 50, true },
                    { new Guid("5fbd9a4b-8989-4397-b795-14d5feee613d"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Experience the power and efficiency of the Apple M1 chip with the MacBook Air (M1, 2020). This lightweight and portable laptop deliver incredible performance and battery life. With its stunning Retina display, Magic Keyboard, and spacious trackpad, the MacBook Air provides an immersive and comfortable user experience. Whether you're browsing the web, editing photos, or working on intensive tasks, the MacBook Air is up to the challenge.", "~/images/products/Mac/MacBook Air (M1, 2020).jpg", "MacBook Air (M1, 2020)", 999.99m, 50, true },
                    { new Guid("707b337f-e8b9-48ed-91c1-bac977c02cdd"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Get the best of Apple Watch at an affordable price with the Apple Watch SE (1st generation). Featuring a powerful dual-core processor, this watch delivers fast performance and seamless connectivity. Track your workouts, monitor your heart rate, and stay motivated with advanced fitness features. With its sleek design and stunning Retina display, the Apple Watch SE (1st generation) is the perfect blend of style and functionality.", "~/images/products/Watch/Apple Watch SE (1st generation).jpeg", "Apple Watch SE (1st generation)", 149.99m, 50, true },
                    { new Guid("733b8ba0-4ea9-4a56-b1a3-c691c3fdd340"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Experience desktop power in a compact form factor with the Mac mini (2023). This versatile desktop computer packs incredible performance and connectivity options into a small and stylish design. Whether you're using it for everyday tasks, creative projects, or as a home media center, the Mac mini (2023) delivers exceptional speed and responsiveness. With its advanced thermal architecture and wide range of ports, you can connect a variety of peripherals and unleash your productivity.", "~/images/products/Mac/Mac mini (2023).png", "Mac mini (2023)", 499.99m, 50, true },
                    { new Guid("7ac21f62-1a1f-41b0-bfed-f10d3d1913ca"), new Guid("88063adf-151c-41e6-a8d4-53f5a5fad4e5"), "Apple", "Discover the versatility of the iPad (9th generation). Powered by the A13 Bionic chip, this tablet offers impressive performance for both work and play. Its 10.2-inch Retina display provides a stunning canvas for your content, while features like Apple Pencil support and the Smart Keyboard make it even more versatile and productive.", "~/images/products/iPad/iPad (9th generation).png", "iPad (9th generation)", 459.99m, 50, true },
                    { new Guid("8b1446fe-24b8-4b1f-8d15-24ca82fe74db"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Experience the next generation of Apple Watch with the Series 7. With its larger display, sleek design, and powerful features, this watch is the epitome of innovation and style. The Always-On Retina display is now even more expansive, allowing for more information and greater readability. Stay connected, track your workouts, monitor your health, and access your favorite apps with ease. With its durable construction and advanced technologies, the Apple Watch Series 7 is built to accompany you on all of life's adventures.", "~/images/products/Watch/Apple Watch Series 7.jpeg", "Apple Watch Series 7", 699.99m, 50, true },
                    { new Guid("8d8df9f3-807b-4915-b25a-9ca986be4888"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Experience the future of smartwatches with the Apple Watch Series 5. This watch features an Always-On Retina display that never sleeps, allowing you to check the time and glance at your important information without raising your wrist. Stay connected with built-in cellular and GPS capabilities, track your workouts with advanced sensors, and monitor your heart rate throughout the day. With its stylish design and powerful features, the Apple Watch Series 5 is the perfect blend of fashion and technology.", "~/images/products/Watch/Apple Watch Series 5.jpg", "Apple Watch Series 5", 499.99m, 50, true },
                    { new Guid("9928429d-b3fd-4b01-b9f2-c07204d150e1"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Experience the power of Apple Watch Series 3 on your wrist. With built-in cellular connectivity, you can make calls, send messages, stream music, and more, even without your iPhone nearby. Stay active and motivated with advanced fitness tracking, automatic workout detection, and heart rate monitoring. The bright and crisp display keeps you informed and engaged throughout the day. With its iconic design and seamless integration with your Apple devices, the Apple Watch Series 3 is your ultimate companion for a connected life.", "~/images/products/Watch/Apple Watch Series 3.jpg", "Apple Watch Series 3", 349.99m, 50, true },
                    { new Guid("a4a1efc3-8e80-49e6-8a65-c8a8104bf709"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Experience the future of iPhone with the iPhone 14. Powered by the next-generation A16 chip, this phone delivers unparalleled performance and efficiency. Its stunning Super Retina XDR display provides a vibrant and immersive visual experience. With advanced camera features, enhanced AI capabilities, and seamless integration with Apple's ecosystem, the iPhone 14 redefines what a smartphone can do.", "~/images/products/iPhone/iPhone 14.png", "iPhone 14", 799.99m, 50, true },
                    { new Guid("ac69f854-e69d-4a59-9a10-af6e3a4ed4ae"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Experience the power of the iPhone 13 mini. Powered by the A15 Bionic chip, this phone offers incredible performance and efficiency. Its 5.4-inch Super Retina XDR display provides stunning visuals, while the advanced camera system allows you to capture professional-quality photos and videos. With features like Face ID, 5G connectivity, and all-day battery life, the iPhone 13 mini is a compact powerhouse.", "~/images/products/iPhone/iPhone 13 mini.png", "iPhone 13 mini", 599.99m, 50, true },
                    { new Guid("aca70684-7506-43ef-8094-b612846c235e"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Discover the power and versatility of the Mac Studio (2023). This all-in-one desktop computer combines performance, display, and audio into a seamless and immersive experience. With its stunning 32-inch Retina display, powerful M1 chip, and studio-quality speakers, the Mac Studio is designed for professionals in creative fields such as design, photography, and music production. Experience the future of desktop computing with the Mac Studio (2023).", "~/images/products/Mac/Mac Studio (2023).png", "Mac Studio (2023)", 1999.99m, 50, true },
                    { new Guid("b10c8144-f84b-4ace-a111-0dd14c3a749c"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Discover the larger variant of the iPhone 14 with the iPhone 14 Plus. Powered by the next-generation A16 chip, this phone offers exceptional performance and efficiency. Its expansive Super Retina XDR display provides a stunning visual experience with true-to-life colors. With advanced camera features, enhanced AI capabilities, and a range of innovative technologies, the iPhone 14 Plus takes your smartphone experience to new heights.", "~/images/products/iPhone/iPhone 14 Plus.png", "iPhone 14 Plus", 899.99m, 50, true },
                    { new Guid("ba1d4cf2-cf4c-4710-a604-0546b6bdc66f"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Experience the pinnacle of iPhone technology with the iPhone 14 Pro. Powered by the next-generation A16 chip, this phone offers unmatched performance and efficiency. Its Super Retina XDR display with ProMotion technology delivers breathtaking visuals with HDR content support. With the advanced camera system, powerful AI capabilities, and a suite of professional-grade features, the iPhone 14 Pro empowers you to capture, create, and explore like never before.", "~/images/products/iPhone/iPhone 14 Pro.png", "iPhone 14 Pro", 999.99m, 50, true },
                    { new Guid("bf0f6d00-5ac9-4eee-97db-8d58f1a31009"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Introducing the Apple Watch Series 8, the next generation of smartwatches. With its advanced features and sleek design, this watch is a true technological marvel. The stunning always-on display is larger and brighter, providing crystal-clear visuals and enhanced readability. Track your fitness goals, monitor your health, and stay connected with ease. The Apple Watch Series 8 is built to elevate your daily life and empower you to achieve more.", "~/images/products/Watch/Apple Watch Series 8.jpeg", "Apple Watch Series 8", 1499.99m, 50, true },
                    { new Guid("c2640f7a-8f2e-42b1-bc09-cfd8f3bca401"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Experience a new level of performance and design with the iMac (24-inch, M1, 2021). Powered by the Apple M1 chip, this all-in-one desktop computer delivers remarkable speed and efficiency. The 24-inch Retina display with True Tone technology brings your content to life with vibrant colors and sharp details. With its sleek and thin design, enhanced audio capabilities, and advanced camera and microphone system, the iMac (24-inch, M1, 2021) is perfect for work, creativity, and entertainment.", "~/images/products/Mac/iMac (24-inch, M1, 2021).jpg", "iMac (24-inch, M1, 2021)", 1499.99m, 50, true },
                    { new Guid("c985f989-3a2f-4bf8-bd4e-28648ac24643"), new Guid("c6406d0a-9199-478e-9753-efbfc08a61f5"), "Apple", "Elevate your audio experience with AirPods Pro (2nd generation). These advanced wireless earbuds build upon the success of the previous generation, offering enhanced features and performance. Enjoy the immersive sound of active noise cancellation, which intelligently adapts to your environment, providing an uninterrupted listening experience. The customizable fit, achieved through three sizes of soft, tapered silicone tips, ensures a comfortable and secure seal for hours of listening pleasure. With spatial audio and Adaptive EQ, AirPods Pro (2nd generation) deliver a truly immersive and personalized soundstage. Stay connected, enjoy hands-free Siri access, and embrace the future of wireless audio.", "~/images/products/AirPods/AirPods Pro (2nd generation).png", "AirPods Pro (2nd generation)", 279.99m, 50, true },
                    { new Guid("cb9d9f8d-4595-4459-8de4-6e9e8dbe036e"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Embrace the future of smartwatches with the Apple Watch Series 6. Featuring advanced health sensors, this watch helps you monitor your blood oxygen levels, measure your ECG, and track your sleep patterns. Stay connected, make calls, send messages, and stream music, all without your iPhone nearby. The Always-On Retina display is now brighter and more energy-efficient, providing you with clear and vibrant visuals throughout the day. With its powerful features and stylish design, the Apple Watch Series 6 is the ultimate companion for a healthy and connected life.", "~/images/products/Watch/Apple Watch Series 6.jpg", "Apple Watch Series 6", 599.99m, 50, true },
                    { new Guid("d2be6bc3-867b-4d0b-a46a-f25e94faedbd"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Experience the ultimate iPhone with the iPhone 13 Pro. Powered by the A15 Bionic chip, this phone offers unmatched performance and efficiency. Its 6.1-inch Super Retina XDR display with ProMotion technology delivers stunning visuals with HDR content support. With the advanced camera system, including the new Pro camera mode and ProRAW, you can capture and edit professional-quality photos and videos. The iPhone 13 Pro also features 5G connectivity, enhanced durability, and all-day battery life.", "~/images/products/iPhone/iPhone 13 Pro.png", "iPhone 13 Pro", 999.99m, 50, true },
                    { new Guid("d355d6d6-7a48-49d9-ade2-59d10bf3d029"), new Guid("ed09d5cd-cbde-4892-a32e-1e3424084731"), "Apple", "Experience the future of professional computing with the MacBook Pro (14-inch, 2023). This redesigned laptop features a stunning 14-inch Liquid Retina XDR display with mini-LED backlighting, offering exceptional brightness and contrast. Powered by the latest generation of Apple silicon, it delivers incredible performance and efficiency. With its advanced thermal system, redesigned keyboard, and immersive audio, the MacBook Pro (14-inch, 2023) is the ultimate tool for professionals and power users.", "~/images/products/Mac/MacBook Pro (14-inch, 2023).png", "MacBook Pro (14-inch, 2023)", 1999.99m, 50, true },
                    { new Guid("d706e2dc-50f2-4d0c-9545-a347dd605e38"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Introducing the Apple Watch SE (2nd generation), the perfect companion for your active lifestyle. With its powerful features and affordable price, this watch offers a seamless experience. Stay connected, track your workouts, and monitor your health with ease. With its stylish design and expansive Retina display, the Apple Watch SE (2nd generation) is the ultimate fusion of affordability and performance.", "~/images/products/Watch/Apple Watch SE (2nd generation).jpeg", "Apple Watch SE (2nd generation)", 249.99m, 50, true },
                    { new Guid("d99cde06-9bb2-4e7d-991f-74e56ebcc14c"), new Guid("88063adf-151c-41e6-a8d4-53f5a5fad4e5"), "Apple", "Unleash your creativity with the iPad Air (5th generation). Powered by the A15 Bionic chip, this tablet offers incredible performance and graphics capabilities. Its 10.9-inch Liquid Retina display with True Tone brings your content to life, while features like Apple Pencil support and the Magic Keyboard enhance your productivity and creativity.", "~/images/products/iPad/iPad Air (5th generation).png", "iPad Air (5th generation)", 599.99m, 50, true },
                    { new Guid("e1eedf4c-d09a-4552-b9d3-796bca20c9e7"), new Guid("88063adf-151c-41e6-a8d4-53f5a5fad4e5"), "Apple", "Experience the next generation of iPad with the iPad (10th generation). Powered by the A14 Bionic chip, this tablet delivers remarkable performance and graphics capabilities. Its 10.2-inch Retina display provides a stunning visual experience, while features like Apple Pencil support and the Smart Keyboard make it even more versatile and productive.", "~/images/products/iPad/iPad (10th generation).png", "iPad (10th generation)", 599.99m, 50, true },
                    { new Guid("e9d77afe-581b-484d-95e7-09c3e2b0b3a5"), new Guid("73903055-498b-4fda-b2c9-4e7f5a7e539b"), "Apple", "Discover a new era of Apple Watch with the Series 4. With its stunning display, slimmer profile, and breakthrough health features, this watch redefines what a timepiece can do. The larger screen allows for more immersive experiences and improved readability. Track your workouts with enhanced precision, monitor your heart rate, and receive proactive health notifications. With built-in ECG and fall detection, the Apple Watch Series 4 is your intelligent health guardian on your wrist.", "~/images/products/Watch/Apple Watch Series 4.jpg", "Apple Watch Series 4", 399.99m, 50, true },
                    { new Guid("eb88bcc2-a83e-455f-beda-4f3032c7b59e"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Discover the power of the compact iPhone SE (3rd generation). Featuring the A15 Bionic chip, this phone delivers remarkable performance and graphics capabilities. Its 4.7-inch Retina HD display and advanced camera system make it perfect for capturing stunning photos and videos. With features like Touch ID and wireless charging, the iPhone SE is a powerful device in a compact form factor.", "~/images/products/iPhone/iPhone SE (3rd generation).png", "iPhone SE (3rd generation)", 429.99m, 50, true },
                    { new Guid("eed3aa81-7998-4cea-9544-b8d4d9bc61d8"), new Guid("95629ef1-8c3c-44fc-a7c6-868f2013cedb"), "Apple", "Discover the epitome of iPhone excellence with the iPhone 13 Pro Max. Powered by the A15 Bionic chip, this phone offers exceptional performance and efficiency. Its 6.7-inch Super Retina XDR display with ProMotion technology provides breathtaking visuals with HDR content support. With the advanced camera system, including the new Pro camera mode and ProRAW, you can capture and edit professional-quality photos and videos. The iPhone 13 Pro Max also features 5G connectivity, enhanced durability, and all-day battery life.", "~/images/products/iPhone/iPhone 13 Pro Max.png", "iPhone 13 Pro Max", 1099.99m, 50, true }
                });

            migrationBuilder.InsertData(
                table: "tblShiftAssignments",
                columns: new[] { "AssignmentID", "Date", "DayOfWeek", "FkShiftID", "FkStaffID" },
                values: new object[,]
                {
                    { new Guid("2b523f88-a195-45d8-9858-1f17fe0fb8e0"), new DateTime(2023, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tuesday", new Guid("439ecd08-a898-4748-87ff-441fa256f6d0"), new Guid("cdc56d5f-8405-49c2-91ab-0fe7bb5e2ba0") },
                    { new Guid("47add7d7-3acf-428b-a152-00c780772dbf"), new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thursday", new Guid("eefb372d-7fab-4e12-a045-795589d8a3f2"), new Guid("45dd31d8-5738-488f-ab2d-7cfb1fc98e12") },
                    { new Guid("8e2a7075-7fae-4aa7-a2a9-85a9575ceb3b"), new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monday", new Guid("1d8dd322-0f51-4bea-ab40-f7c56f97308a"), new Guid("a7931c2e-5f52-49bd-9879-5545b93c5b1f") },
                    { new Guid("99c7ea2f-73ce-4ce7-9951-9b3b21fde5be"), new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Friday", new Guid("dde4e4dd-ee82-4f6e-839c-1ae187eda794"), new Guid("b1ff55b3-6864-4a68-ae07-f0d2d92588f2") },
                    { new Guid("b80e1ee9-00a3-4bea-a3cf-02d6103e4578"), new DateTime(2023, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saturday", new Guid("dde4e4dd-ee82-4f6e-839c-1ae187eda794"), new Guid("822f703e-05ac-4811-8817-e25bf0b708bd") },
                    { new Guid("b873906a-ffb5-4e39-9307-8ec2678f52c0"), new DateTime(2023, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wednesday", new Guid("26f57e90-92d5-4e89-a0f3-59b9a062c130"), new Guid("85425342-2db3-4842-961e-b1e0030410ac") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAttendances_FkStaffID",
                table: "tblAttendances",
                column: "FkStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbacks_FkCustomerID",
                table: "tblFeedbacks",
                column: "FkCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbacks_FkProductID",
                table: "tblFeedbacks",
                column: "FkProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tblLeaveApplications_FkManagerID",
                table: "tblLeaveApplications",
                column: "FkManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_tblLeaveApplications_FkStaffID",
                table: "tblLeaveApplications",
                column: "FkStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderDetails_FkProductID",
                table: "tblOrderDetails",
                column: "FkProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_FkCustomerID",
                table: "tblOrders",
                column: "FkCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_FkStaffID",
                table: "tblOrders",
                column: "FkStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_FkVoucherID",
                table: "tblOrders",
                column: "FkVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayrolls_FkManagerID",
                table: "tblPayrolls",
                column: "FkManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayrolls_FkStaffID",
                table: "tblPayrolls",
                column: "FkStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_FkCategoryID",
                table: "tblProducts",
                column: "FkCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblShiftAssignments_FkShiftID",
                table: "tblShiftAssignments",
                column: "FkShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_tblShiftAssignments_FkStaffID",
                table: "tblShiftAssignments",
                column: "FkStaffID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAttendances");

            migrationBuilder.DropTable(
                name: "tblFeedbacks");

            migrationBuilder.DropTable(
                name: "tblLeaveApplications");

            migrationBuilder.DropTable(
                name: "tblOrderDetails");

            migrationBuilder.DropTable(
                name: "tblPayrolls");

            migrationBuilder.DropTable(
                name: "tblShiftAssignments");

            migrationBuilder.DropTable(
                name: "tblOrders");

            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblWorkShifts");

            migrationBuilder.DropTable(
                name: "tblCustomers");

            migrationBuilder.DropTable(
                name: "tblStoreStaff");

            migrationBuilder.DropTable(
                name: "tblVouchers");

            migrationBuilder.DropTable(
                name: "tblCategories");
        }
    }
}
