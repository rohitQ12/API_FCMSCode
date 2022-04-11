using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalApi.Data.Migrations
{
    public partial class GlobalContextDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Inactive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Role_Id_FK = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Inactive = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    imagename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintMst",
                columns: table => new
                {
                    Cmst_Id = table.Column<int>(type: "int", nullable: false),
                    Cmst_Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Cmst_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintMst", x => x.Cmst_Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    cntry_id = table.Column<int>(type: "int", nullable: false),
                    country_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.cntry_id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Dept_Id = table.Column<int>(type: "int", nullable: false),
                    Dept_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Dept_Id);
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    designation_id = table.Column<int>(type: "int", nullable: false),
                    designation_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    designation_desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.designation_id);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    CD_Id = table.Column<int>(type: "int", nullable: false),
                    CD_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CD_ClinicalDiscipline = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.CD_Id);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Diseases_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Diseases_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acronyms = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocPkValue",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PkName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PkStartValue = table.Column<int>(type: "int", nullable: false),
                    PkEndValue = table.Column<int>(type: "int", nullable: false),
                    PkPresentValue = table.Column<int>(type: "int", nullable: false),
                    PkPreviousValue = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocPkValue", x => x.PkId);
                });

            migrationBuilder.CreateTable(
                name: "Doctor_Schedule_history",
                columns: table => new
                {
                    Doc_schedule_history_Id = table.Column<int>(type: "int", nullable: false),
                    Doc_schedule_Id = table.Column<int>(type: "int", nullable: false),
                    DO_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Do_Scd_day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time_from = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time_to = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<int>(type: "int", nullable: true),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: true),
                    Delete_status = table.Column<int>(type: "int", nullable: false),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: true),
                    Is_active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_Schedule_history", x => x.Doc_schedule_history_Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor_Schedules",
                columns: table => new
                {
                    Doc_schedule_Id = table.Column<int>(type: "int", nullable: false),
                    DO_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Do_Scd_day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time_from = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time_to = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<int>(type: "int", nullable: true),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: true),
                    Delete_status = table.Column<int>(type: "int", nullable: false),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: true),
                    Is_active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_Schedules", x => x.Doc_schedule_Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    doctype_id = table.Column<int>(type: "int", nullable: false),
                    doctype_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doc_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.doctype_id);
                });

            migrationBuilder.CreateTable(
                name: "DrugType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emp_Category",
                columns: table => new
                {
                    emp_cat_id = table.Column<int>(type: "int", nullable: false),
                    emp_cat_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emp_Category", x => x.emp_cat_id);
                });

            migrationBuilder.CreateTable(
                name: "Emp_Type",
                columns: table => new
                {
                    emptype_id = table.Column<int>(type: "int", nullable: false),
                    emptype_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emp_Type", x => x.emptype_id);
                });

            migrationBuilder.CreateTable(
                name: "IMG_INVESTIGATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMG_INVESTIGATIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LAB_INVESTIGATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAB_INVESTIGATIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    M_Id = table.Column<int>(type: "int", nullable: false),
                    M_label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    M_icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    M_Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    M_Redirect_URL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    M_DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Created_by = table.Column<int>(type: "int", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: true),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: true),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.M_Id);
                });

            migrationBuilder.CreateTable(
                name: "Network",
                columns: table => new
                {
                    NE_Id = table.Column<int>(type: "int", nullable: false),
                    NE_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NE_Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Network", x => x.NE_Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFullDay = table.Column<bool>(type: "bit", nullable: false),
                    ReadNotifcation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OfficeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Off_Level = table.Column<int>(type: "int", nullable: false),
                    Off_Address1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Off_District_Id_Fk = table.Column<int>(type: "int", nullable: false),
                    Off_pincode = table.Column<int>(type: "int", nullable: false),
                    Off_Address2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Off_Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Off_PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Off_Landline = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Inactive = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Off_UserId = table.Column<int>(type: "int", nullable: false),
                    Off_TS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Off_LastEdited_UserId = table.Column<int>(type: "int", nullable: false),
                    Off_LastEdited_TS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Off_OfficerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Off_Designation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Created_by = table.Column<int>(type: "int", nullable: false),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: false),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: false),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficeRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Phonenumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    qualification_id = table.Column<int>(type: "int", nullable: false),
                    qualification_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qualification_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.qualification_id);
                });

            migrationBuilder.CreateTable(
                name: "Relation",
                columns: table => new
                {
                    relation_id = table.Column<int>(type: "int", nullable: false),
                    relation_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relation", x => x.relation_id);
                });

            migrationBuilder.CreateTable(
                name: "SubMenu",
                columns: table => new
                {
                    SM_Id = table.Column<int>(type: "int", nullable: false),
                    SM_label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    M_MenuName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SM_icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SM_link = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SM_Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SM_Redirect_URL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SM_DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    SM_M_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Created_by = table.Column<int>(type: "int", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: true),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: true),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenu", x => x.SM_Id);
                });

            migrationBuilder.CreateTable(
                name: "SymptomsMst",
                columns: table => new
                {
                    Smst_Id = table.Column<int>(type: "int", nullable: false),
                    Smst_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Smst_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomsMst", x => x.Smst_Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    User_cat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_ref_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    currency_id = table.Column<int>(type: "int", nullable: false),
                    currency_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currency_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cntry_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.currency_id);
                    table.ForeignKey(
                        name: "FK_Currency_Countries_cntry_id",
                        column: x => x.cntry_id,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    stat_id = table.Column<int>(type: "int", nullable: false),
                    state_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cntry_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.stat_id);
                    table.ForeignKey(
                        name: "FK_States_Countries_cntry_id",
                        column: x => x.cntry_id,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Dept_Id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Section_Id);
                    table.ForeignKey(
                        name: "FK_Section_Department_Dept_Id",
                        column: x => x.Dept_Id,
                        principalTable: "Department",
                        principalColumn: "Dept_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    SP_Id = table.Column<int>(type: "int", nullable: false),
                    SP_Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SP_CD_Id = table.Column<int>(type: "int", nullable: false),
                    SP_Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.SP_Id);
                    table.ForeignKey(
                        name: "FK_Specialization_Discipline_SP_CD_Id",
                        column: x => x.SP_CD_Id,
                        principalTable: "Discipline",
                        principalColumn: "CD_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DType_Id_FK = table.Column<int>(type: "int", nullable: false),
                    DrugUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_DrugType_DType_Id_FK",
                        column: x => x.DType_Id_FK,
                        principalTable: "DrugType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IMG_SUBINVESTIGATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Img_Invt_Id = table.Column<int>(type: "int", nullable: false),
                    Sub_Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMG_SUBINVESTIGATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IMG_SUBINVESTIGATIONS_IMG_INVESTIGATIONS_Img_Invt_Id",
                        column: x => x.Img_Invt_Id,
                        principalTable: "IMG_INVESTIGATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LAB_SUBINVESTIGATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Lab_Invt_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Sub_Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAB_SUBINVESTIGATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LAB_SUBINVESTIGATIONS_LAB_INVESTIGATIONS_Lab_Invt_Id_FK",
                        column: x => x.Lab_Invt_Id_FK,
                        principalTable: "LAB_INVESTIGATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillSets",
                columns: table => new
                {
                    Skillset_id = table.Column<int>(type: "int", nullable: false),
                    Skillset_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qualification_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillSets", x => x.Skillset_id);
                    table.ForeignKey(
                        name: "FK_SkillSets_Qualification_qualification_id",
                        column: x => x.qualification_id,
                        principalTable: "Qualification",
                        principalColumn: "qualification_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    RC_Id = table.Column<int>(type: "int", nullable: false),
                    RC_RoleId_FK = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PageFunctionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RC_SM_Id_FK = table.Column<int>(type: "int", nullable: false),
                    RC_M_Id_FK = table.Column<int>(type: "int", nullable: true),
                    RC_SMD_Id_FK = table.Column<int>(type: "int", nullable: true),
                    RC_Value = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    RC_UserId_FK = table.Column<int>(type: "int", nullable: false),
                    RC_INSTS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_by = table.Column<int>(type: "int", nullable: false),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: false),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: false),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.RC_Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_SubMenu_RC_SM_Id_FK",
                        column: x => x.RC_SM_Id_FK,
                        principalTable: "SubMenu",
                        principalColumn: "SM_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubMenusDetails",
                columns: table => new
                {
                    SMD_Id = table.Column<int>(type: "int", nullable: false),
                    SMD_SubMenusFunction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SMD_IsClaimShown_In_UI = table.Column<bool>(type: "bit", nullable: false),
                    SMD_SM_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Created_by = table.Column<int>(type: "int", nullable: false),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: false),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: false),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenusDetails", x => x.SMD_Id);
                    table.ForeignKey(
                        name: "FK_SubMenusDetails_SubMenu_SMD_SM_Id_FK",
                        column: x => x.SMD_SM_Id_FK,
                        principalTable: "SubMenu",
                        principalColumn: "SM_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubMenusFunctions",
                columns: table => new
                {
                    SMF_Id = table.Column<int>(type: "int", nullable: false),
                    SMF_label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SMF_icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SMF_link = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SMF_SM_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Created_by = table.Column<int>(type: "int", nullable: false),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: false),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: false),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenusFunctions", x => x.SMF_Id);
                    table.ForeignKey(
                        name: "FK_SubMenusFunctions_SubMenu_SMF_SM_Id_FK",
                        column: x => x.SMF_SM_Id_FK,
                        principalTable: "SubMenu",
                        principalColumn: "SM_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    district_id = table.Column<int>(type: "int", nullable: false),
                    district_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    district_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stat_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.district_id);
                    table.ForeignKey(
                        name: "FK_Districts_States_stat_id",
                        column: x => x.stat_id,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrugMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DrugName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DT_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UT_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugMaster_DrugType_DT_Id_FK",
                        column: x => x.DT_Id_FK,
                        principalTable: "DrugType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrugMaster_Unit_UT_Id_FK",
                        column: x => x.UT_Id_FK,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubMenusFunctionDetails",
                columns: table => new
                {
                    SMFD_Id = table.Column<int>(type: "int", nullable: false),
                    SMFD_SubMenusFunction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SMFD_IsClaimShown_In_UI = table.Column<bool>(type: "bit", nullable: false),
                    SMFD_SMF_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Created_by = table.Column<int>(type: "int", nullable: false),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: false),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: false),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenusFunctionDetails", x => x.SMFD_Id);
                    table.ForeignKey(
                        name: "FK_SubMenusFunctionDetails_SubMenusFunctions_SMFD_SMF_Id_FK",
                        column: x => x.SMFD_SMF_Id_FK,
                        principalTable: "SubMenusFunctions",
                        principalColumn: "SMF_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubRoleClaims",
                columns: table => new
                {
                    SRC_Id = table.Column<int>(type: "int", nullable: false),
                    SRC_RoleId_FK = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SRC_SMF_Id_FK = table.Column<int>(type: "int", nullable: false),
                    SRC_SMFD_Id_FK = table.Column<int>(type: "int", nullable: true),
                    SRC_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SRC_UserId_FK = table.Column<int>(type: "int", nullable: false),
                    SRC_INSTS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_by = table.Column<int>(type: "int", nullable: false),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_by = table.Column<int>(type: "int", nullable: false),
                    Modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<int>(type: "int", nullable: false),
                    Deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRoleClaims", x => x.SRC_Id);
                    table.ForeignKey(
                        name: "FK_SubRoleClaims_SubMenusFunctions_SRC_SMF_Id_FK",
                        column: x => x.SRC_SMF_Id_FK,
                        principalTable: "SubMenusFunctions",
                        principalColumn: "SMF_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hospital",
                columns: table => new
                {
                    Hos_Id = table.Column<int>(type: "int", nullable: false),
                    Hos_HospitalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Hos_HospitalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hos_HospitalType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hos_Branch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hos_HospitalEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hos_HospitalPhoneNo = table.Column<long>(type: "bigint", nullable: true),
                    Hos_HospitalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryorBranch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hos_Country_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Hos_ST_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Hos_DI_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Hos_Taluk = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hos_PostalCode = table.Column<int>(type: "int", nullable: false),
                    Hos_NE_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Hos_village = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Hos_Alterno = table.Column<long>(type: "bigint", nullable: true),
                    Hos_Landline = table.Column<long>(type: "bigint", nullable: true),
                    Hos_HospitalLogo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospital", x => x.Hos_Id);
                    table.ForeignKey(
                        name: "FK_Hospital_Countries_Hos_Country_Id_FK",
                        column: x => x.Hos_Country_Id_FK,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hospital_Districts_Hos_DI_Id_FK",
                        column: x => x.Hos_DI_Id_FK,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hospital_Network_Hos_NE_Id_FK",
                        column: x => x.Hos_NE_Id_FK,
                        principalTable: "Network",
                        principalColumn: "NE_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hospital_States_Hos_ST_Id_FK",
                        column: x => x.Hos_ST_Id_FK,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vle",
                columns: table => new
                {
                    VL_Id = table.Column<int>(type: "int", nullable: false),
                    VLE_Center = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VLE_Code = table.Column<int>(type: "int", nullable: false),
                    VL_ContactPerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VL_DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VL_Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VL_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VL_Country_Id_FK = table.Column<int>(type: "int", nullable: false),
                    VL_ST_Id_FK = table.Column<int>(type: "int", nullable: false),
                    VL_DI_Id_FK = table.Column<int>(type: "int", nullable: false),
                    VL_Taluk = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VL_Village = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VL_MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    VL_AlterNumber = table.Column<long>(type: "bigint", nullable: true),
                    VL_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VL_QU_Id_FK = table.Column<int>(type: "int", nullable: false),
                    VL_PostalCode = table.Column<int>(type: "int", nullable: false),
                    VL_Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vle", x => x.VL_Id);
                    table.ForeignKey(
                        name: "FK_Vle_Countries_VL_Country_Id_FK",
                        column: x => x.VL_Country_Id_FK,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vle_Districts_VL_DI_Id_FK",
                        column: x => x.VL_DI_Id_FK,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vle_Qualification_VL_QU_Id_FK",
                        column: x => x.VL_QU_Id_FK,
                        principalTable: "Qualification",
                        principalColumn: "qualification_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vle_States_VL_ST_Id_FK",
                        column: x => x.VL_ST_Id_FK,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assistant",
                columns: table => new
                {
                    Assi_Id = table.Column<int>(type: "int", nullable: false),
                    Assi_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Assi_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Assi_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Assi_DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Assi_Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Assi_Hos_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Assi_Qua_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Assi_Des_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Assi_Spe_id_fk = table.Column<int>(type: "int", nullable: false),
                    Assi_Photo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Assi_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assi_Country_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Assi_ST_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Assi_DI_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Assi_Taluk = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Assi_Village = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Assi_PostalCode = table.Column<int>(type: "int", nullable: false),
                    Assi_MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    Assi_LandLineNumber = table.Column<long>(type: "bigint", nullable: true),
                    Assi_AlternativeNumber = table.Column<long>(type: "bigint", nullable: true),
                    Assi_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assistant", x => x.Assi_Id);
                    table.ForeignKey(
                        name: "FK_Assistant_Countries_Assi_Country_Id_FK",
                        column: x => x.Assi_Country_Id_FK,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assistant_Designation_Assi_Des_Id_FK",
                        column: x => x.Assi_Des_Id_FK,
                        principalTable: "Designation",
                        principalColumn: "designation_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assistant_Districts_Assi_DI_Id_FK",
                        column: x => x.Assi_DI_Id_FK,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assistant_Hospital_Assi_Hos_Id_FK",
                        column: x => x.Assi_Hos_Id_FK,
                        principalTable: "Hospital",
                        principalColumn: "Hos_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assistant_Qualification_Assi_Qua_Id_FK",
                        column: x => x.Assi_Qua_Id_FK,
                        principalTable: "Qualification",
                        principalColumn: "qualification_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assistant_Specialization_Assi_Spe_id_fk",
                        column: x => x.Assi_Spe_id_fk,
                        principalTable: "Specialization",
                        principalColumn: "SP_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assistant_States_Assi_ST_Id_FK",
                        column: x => x.Assi_ST_Id_FK,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticCenters",
                columns: table => new
                {
                    DGSTC_Id = table.Column<int>(type: "int", nullable: false),
                    DGSTC_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DGSTC_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DGSTC_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DGSTC_HO_Id_FK = table.Column<int>(type: "int", nullable: true),
                    DGSTC_ST_Id_FK = table.Column<int>(type: "int", nullable: false),
                    DGSTC_DI_Id_FK = table.Column<int>(type: "int", nullable: false),
                    DGSTC_Village = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DGSTC_PostalCode = table.Column<int>(type: "int", nullable: false),
                    DGSTC_MobileNumber = table.Column<long>(type: "bigint", nullable: true),
                    DGSTC_AlterNumber = table.Column<long>(type: "bigint", nullable: true),
                    DGSTC_LandLineNo = table.Column<long>(type: "bigint", nullable: true),
                    DGSTC_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticCenters", x => x.DGSTC_Id);
                    table.ForeignKey(
                        name: "FK_DiagnosticCenters_Districts_DGSTC_DI_Id_FK",
                        column: x => x.DGSTC_DI_Id_FK,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosticCenters_Hospital_DGSTC_HO_Id_FK",
                        column: x => x.DGSTC_HO_Id_FK,
                        principalTable: "Hospital",
                        principalColumn: "Hos_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosticCenters_States_DGSTC_ST_Id_FK",
                        column: x => x.DGSTC_ST_Id_FK,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DO_Id = table.Column<int>(type: "int", nullable: false),
                    DO_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DO_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DO_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DO_DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DO_Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DO_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DO_Country_Id_FK = table.Column<int>(type: "int", nullable: false),
                    DO_ST_Id_FK = table.Column<int>(type: "int", nullable: false),
                    DO_DI_Id_FK = table.Column<int>(type: "int", nullable: false),
                    DO_Taluk = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DO_Village = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DO_PostalCode = table.Column<int>(type: "int", nullable: false),
                    DO_MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    DO_OfficialNumber = table.Column<long>(type: "bigint", nullable: true),
                    DO_Alernative_Numb = table.Column<long>(type: "bigint", nullable: true),
                    DO_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DO_HO_Id_FK = table.Column<int>(type: "int", nullable: true),
                    DO_QU_Id_FK = table.Column<int>(type: "int", nullable: true),
                    DO_DE_Id_FK = table.Column<int>(type: "int", nullable: true),
                    DO_CD_Id_FK = table.Column<int>(type: "int", nullable: true),
                    DO_SP_Id_FK = table.Column<int>(type: "int", nullable: true),
                    DO_Photo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DO_UserId_FK = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DO_Id);
                    table.ForeignKey(
                        name: "FK_Doctor_Countries_DO_Country_Id_FK",
                        column: x => x.DO_Country_Id_FK,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_Designation_DO_DE_Id_FK",
                        column: x => x.DO_DE_Id_FK,
                        principalTable: "Designation",
                        principalColumn: "designation_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_Discipline_DO_CD_Id_FK",
                        column: x => x.DO_CD_Id_FK,
                        principalTable: "Discipline",
                        principalColumn: "CD_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_Districts_DO_DI_Id_FK",
                        column: x => x.DO_DI_Id_FK,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_Hospital_DO_HO_Id_FK",
                        column: x => x.DO_HO_Id_FK,
                        principalTable: "Hospital",
                        principalColumn: "Hos_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_Qualification_DO_QU_Id_FK",
                        column: x => x.DO_QU_Id_FK,
                        principalTable: "Qualification",
                        principalColumn: "qualification_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_Specialization_DO_SP_Id_FK",
                        column: x => x.DO_SP_Id_FK,
                        principalTable: "Specialization",
                        principalColumn: "SP_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_States_DO_ST_Id_FK",
                        column: x => x.DO_ST_Id_FK,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PR_Id = table.Column<int>(type: "int", nullable: false),
                    PR_RemoteHospitalName_Id_FK = table.Column<int>(type: "int", nullable: true),
                    PR_PatientCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PR_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PR_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PR_Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PR_DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PR_Age = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PR_LandlineNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PR_Alternative_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PR_MaritalStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PR_FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PR_Religion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PR_Nationality = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PR_Caste = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PR_BloodGroup = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PR_MotherTongue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PR_Occupation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PR_Income = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PR_Insurance = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PR_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PR_Country_Id_FK = table.Column<int>(type: "int", nullable: false),
                    PR_S_Id_FK = table.Column<int>(type: "int", nullable: false),
                    PR_D_Id_FK = table.Column<int>(type: "int", nullable: false),
                    PR_Taluk = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PR_Village = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PR_Postalcode = table.Column<int>(type: "int", nullable: false),
                    PR_MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PR_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PR_PassportNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PR_RegistrationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PR_Photo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PR_UserId_FK = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PR_Id);
                    table.ForeignKey(
                        name: "FK_Patient_Countries_PR_Country_Id_FK",
                        column: x => x.PR_Country_Id_FK,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Districts_PR_D_Id_FK",
                        column: x => x.PR_D_Id_FK,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Hospital_PR_RemoteHospitalName_Id_FK",
                        column: x => x.PR_RemoteHospitalName_Id_FK,
                        principalTable: "Hospital",
                        principalColumn: "Hos_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_States_PR_S_Id_FK",
                        column: x => x.PR_S_Id_FK,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    Ph_Id = table.Column<int>(type: "int", nullable: false),
                    Ph_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ph_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ph_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ph_HO_Id_FK = table.Column<int>(type: "int", nullable: true),
                    Ph_ST_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Ph_DI_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Ph_Village = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ph_PostalCode = table.Column<int>(type: "int", nullable: false),
                    Ph_MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    Ph_AlterNumber = table.Column<long>(type: "bigint", nullable: true),
                    Ph_LandLineNo = table.Column<long>(type: "bigint", nullable: true),
                    Ph_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.Ph_Id);
                    table.ForeignKey(
                        name: "FK_Pharmacy_Districts_Ph_DI_Id_FK",
                        column: x => x.Ph_DI_Id_FK,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pharmacy_Hospital_Ph_HO_Id_FK",
                        column: x => x.Ph_HO_Id_FK,
                        principalTable: "Hospital",
                        principalColumn: "Hos_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pharmacy_States_Ph_ST_Id_FK",
                        column: x => x.Ph_ST_Id_FK,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    doc_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Lang_Id_FK = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorLanguage_Doctor_doc_Id_FK",
                        column: x => x.doc_Id_FK,
                        principalTable: "Doctor",
                        principalColumn: "DO_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorLanguage_Language_Lang_Id_FK",
                        column: x => x.Lang_Id_FK,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    doc_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorLocation_Doctor_doc_Id_FK",
                        column: x => x.doc_Id_FK,
                        principalTable: "Doctor",
                        principalColumn: "DO_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientDocument",
                columns: table => new
                {
                    Doc_Id = table.Column<int>(type: "int", nullable: false),
                    PR_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Doc_Type_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Choose_Document = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Doc_UserId_FK = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDocument", x => x.Doc_Id);
                    table.ForeignKey(
                        name: "FK_PatientDocument_DocumentType_Doc_Type_Id_FK",
                        column: x => x.Doc_Type_Id_FK,
                        principalTable: "DocumentType",
                        principalColumn: "doctype_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientDocument_Patient_PR_Id_FK",
                        column: x => x.PR_Id_FK,
                        principalTable: "Patient",
                        principalColumn: "PR_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Complaint",
                columns: table => new
                {
                    CPT_Id = table.Column<int>(type: "int", nullable: false),
                    CPT_MST_Id_FK = table.Column<int>(type: "int", nullable: false),
                    CPT_APPT_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.CPT_Id);
                    table.ForeignKey(
                        name: "FK_Complaint_ComplaintMst_CPT_MST_Id_FK",
                        column: x => x.CPT_MST_Id_FK,
                        principalTable: "ComplaintMst",
                        principalColumn: "Cmst_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consultation",
                columns: table => new
                {
                    CON_Id = table.Column<int>(type: "int", nullable: false),
                    CON_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CON_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CON_APPT_Id_FK = table.Column<int>(type: "int", nullable: false),
                    CON_PR_Id_FK = table.Column<int>(type: "int", nullable: true),
                    CON_DO_Id_FK = table.Column<int>(type: "int", nullable: true),
                    CON_HO_Id_FK = table.Column<int>(type: "int", nullable: true),
                    CON_CD_Id_FK = table.Column<int>(type: "int", nullable: true),
                    CON_SP_Id_FK = table.Column<int>(type: "int", nullable: true),
                    Dis_Id_FK = table.Column<int>(type: "int", nullable: true),
                    CON_Ref_AS_Id = table.Column<int>(type: "int", nullable: true),
                    CON_ConsultedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CON_UserId_FK = table.Column<int>(type: "int", nullable: false),
                    Inactive = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultation", x => x.CON_Id);
                    table.ForeignKey(
                        name: "FK_Consultation_Discipline_CON_CD_Id_FK",
                        column: x => x.CON_CD_Id_FK,
                        principalTable: "Discipline",
                        principalColumn: "CD_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consultation_Diseases_Dis_Id_FK",
                        column: x => x.Dis_Id_FK,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consultation_Doctor_CON_DO_Id_FK",
                        column: x => x.CON_DO_Id_FK,
                        principalTable: "Doctor",
                        principalColumn: "DO_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consultation_Hospital_CON_HO_Id_FK",
                        column: x => x.CON_HO_Id_FK,
                        principalTable: "Hospital",
                        principalColumn: "Hos_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consultation_Patient_CON_PR_Id_FK",
                        column: x => x.CON_PR_Id_FK,
                        principalTable: "Patient",
                        principalColumn: "PR_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consultation_Specialization_CON_SP_Id_FK",
                        column: x => x.CON_SP_Id_FK,
                        principalTable: "Specialization",
                        principalColumn: "SP_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DP_CON_Id_FK = table.Column<int>(type: "int", nullable: false),
                    BreakFast = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Lunch = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Dinner = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPlan_Consultation_DP_CON_Id_FK",
                        column: x => x.DP_CON_Id_FK,
                        principalTable: "Consultation",
                        principalColumn: "CON_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImgTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImgRefDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Img_CON_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Delivery_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AcceptImgTest = table.Column<bool>(type: "bit", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImgTest_Consultation_Img_CON_Id_FK",
                        column: x => x.Img_CON_Id_FK,
                        principalTable: "Consultation",
                        principalColumn: "CON_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabTesting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TstRefDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tst_CON_Id_FK = table.Column<int>(type: "int", nullable: false),
                    AcceptLabTest = table.Column<bool>(type: "bit", nullable: true),
                    SampleTaken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delivery_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTesting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTesting_Consultation_Tst_CON_Id_FK",
                        column: x => x.Tst_CON_Id_FK,
                        principalTable: "Consultation",
                        principalColumn: "CON_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientRxDetails",
                columns: table => new
                {
                    Rx_Id = table.Column<int>(type: "int", nullable: false),
                    Prescription_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rx_CON_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Delivery_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AcceptPrescription = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRxDetails", x => x.Rx_Id);
                    table.ForeignKey(
                        name: "FK_PatientRxDetails_Consultation_Rx_CON_Id_FK",
                        column: x => x.Rx_CON_Id_FK,
                        principalTable: "Consultation",
                        principalColumn: "CON_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImgTestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Img_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Img_Invst_Id = table.Column<int>(type: "int", nullable: false),
                    Img_SubInvst_Id = table.Column<int>(type: "int", nullable: false),
                    ImgRemarks = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgTestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImgTestDetails_IMG_INVESTIGATIONS_Img_Invst_Id",
                        column: x => x.Img_Invst_Id,
                        principalTable: "IMG_INVESTIGATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImgTestDetails_IMG_SUBINVESTIGATIONS_Img_SubInvst_Id",
                        column: x => x.Img_SubInvst_Id,
                        principalTable: "IMG_SUBINVESTIGATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImgTestDetails_ImgTest_Img_Id_FK",
                        column: x => x.Img_Id_FK,
                        principalTable: "ImgTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabTestingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LT_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Lab_Invst_Id = table.Column<int>(type: "int", nullable: false),
                    Lab_SubInvst_Id = table.Column<int>(type: "int", nullable: false),
                    FastingORNonFasting = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTestingDetails_LAB_INVESTIGATIONS_Lab_Invst_Id",
                        column: x => x.Lab_Invst_Id,
                        principalTable: "LAB_INVESTIGATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabTestingDetails_LAB_SUBINVESTIGATIONS_Lab_SubInvst_Id",
                        column: x => x.Lab_SubInvst_Id,
                        principalTable: "LAB_SUBINVESTIGATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabTestingDetails_LabTesting_LT_Id_FK",
                        column: x => x.LT_Id_FK,
                        principalTable: "LabTesting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patient_Prescription_DTL",
                columns: table => new
                {
                    Dtl_Id = table.Column<int>(type: "int", nullable: false),
                    Rx_Id_FK = table.Column<int>(type: "int", nullable: false),
                    DrugMst_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Rx_Dosage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Rx_Course = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Prescription_DTL", x => x.Dtl_Id);
                    table.ForeignKey(
                        name: "FK_Patient_Prescription_DTL_DrugMaster_DrugMst_Id_FK",
                        column: x => x.DrugMst_Id_FK,
                        principalTable: "DrugMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Prescription_DTL_PatientRxDetails_Rx_Id_FK",
                        column: x => x.Rx_Id_FK,
                        principalTable: "PatientRxDetails",
                        principalColumn: "Rx_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiseasesDtl",
                columns: table => new
                {
                    Ddtl_Id = table.Column<int>(type: "int", nullable: false),
                    Dis_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Ddtl_APPT_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseasesDtl", x => x.Ddtl_Id);
                    table.ForeignKey(
                        name: "FK_DiseasesDtl_Diseases_Dis_Id_FK",
                        column: x => x.Dis_Id_FK,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    PA_Id = table.Column<int>(type: "int", nullable: false),
                    PA_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PA_APPT_Id_FK = table.Column<int>(type: "int", nullable: false),
                    PA_Height = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_Weight = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_TempInFahrenheit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_TempInCelsius = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_BloodPressure = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_Sugar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_PulseRate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_RespiratoryRate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_ECG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_OxygenSaturation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PA_UserId_FK = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.PA_Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientAppointment",
                columns: table => new
                {
                    Appt_Id = table.Column<int>(type: "int", nullable: false),
                    Appt_PatientId_FK = table.Column<int>(type: "int", nullable: true),
                    Appt_CD_Id_FK = table.Column<int>(type: "int", nullable: true),
                    Appt_DO_Id_FK = table.Column<int>(type: "int", nullable: true),
                    Appt_DateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    Select_day = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Select_FrmTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Select_toTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Doctor_approval_status = table.Column<int>(type: "int", nullable: true),
                    Appt_Is_active = table.Column<int>(type: "int", nullable: true),
                    Appt_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Assi_Id_FK = table.Column<int>(type: "int", nullable: true),
                    Ref_Id_FK = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAppointment", x => x.Appt_Id);
                    table.ForeignKey(
                        name: "FK_PatientAppointment_Assistant_Assi_Id_FK",
                        column: x => x.Assi_Id_FK,
                        principalTable: "Assistant",
                        principalColumn: "Assi_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientAppointment_Discipline_Appt_CD_Id_FK",
                        column: x => x.Appt_CD_Id_FK,
                        principalTable: "Discipline",
                        principalColumn: "CD_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientAppointment_Doctor_Appt_DO_Id_FK",
                        column: x => x.Appt_DO_Id_FK,
                        principalTable: "Doctor",
                        principalColumn: "DO_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientAppointment_Patient_Appt_PatientId_FK",
                        column: x => x.Appt_PatientId_FK,
                        principalTable: "Patient",
                        principalColumn: "PR_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SHReferrals",
                columns: table => new
                {
                    SHR_Id = table.Column<int>(type: "int", nullable: false),
                    SHR_Appt_Id_FK = table.Column<int>(type: "int", nullable: false),
                    SHR_CON_Id_FK = table.Column<int>(type: "int", nullable: true),
                    SHR_PR_Id_FK = table.Column<int>(type: "int", nullable: true),
                    SHR_Ref_D_Id_FK = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHR_RH_DoctorRefferdTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SHR_UserId_FK = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHReferrals", x => x.SHR_Id);
                    table.ForeignKey(
                        name: "FK_SHReferrals_Consultation_SHR_CON_Id_FK",
                        column: x => x.SHR_CON_Id_FK,
                        principalTable: "Consultation",
                        principalColumn: "CON_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SHReferrals_Doctor_SHR_Ref_D_Id_FK",
                        column: x => x.SHR_Ref_D_Id_FK,
                        principalTable: "Doctor",
                        principalColumn: "DO_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SHReferrals_Patient_SHR_PR_Id_FK",
                        column: x => x.SHR_PR_Id_FK,
                        principalTable: "Patient",
                        principalColumn: "PR_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SHReferrals_PatientAppointment_SHR_Appt_Id_FK",
                        column: x => x.SHR_Appt_Id_FK,
                        principalTable: "PatientAppointment",
                        principalColumn: "Appt_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    SYM_Id = table.Column<int>(type: "int", nullable: false),
                    SYM_MST_Id_FK = table.Column<int>(type: "int", nullable: false),
                    SYM_APPT_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.SYM_Id);
                    table.ForeignKey(
                        name: "FK_Symptoms_PatientAppointment_SYM_APPT_Id_FK",
                        column: x => x.SYM_APPT_Id_FK,
                        principalTable: "PatientAppointment",
                        principalColumn: "Appt_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Symptoms_SymptomsMst_SYM_MST_Id_FK",
                        column: x => x.SYM_MST_Id_FK,
                        principalTable: "SymptomsMst",
                        principalColumn: "Smst_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assistant_Assi_Country_Id_FK",
                table: "Assistant",
                column: "Assi_Country_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Assistant_Assi_Des_Id_FK",
                table: "Assistant",
                column: "Assi_Des_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Assistant_Assi_DI_Id_FK",
                table: "Assistant",
                column: "Assi_DI_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Assistant_Assi_Hos_Id_FK",
                table: "Assistant",
                column: "Assi_Hos_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Assistant_Assi_Qua_Id_FK",
                table: "Assistant",
                column: "Assi_Qua_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Assistant_Assi_Spe_id_fk",
                table: "Assistant",
                column: "Assi_Spe_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Assistant_Assi_ST_Id_FK",
                table: "Assistant",
                column: "Assi_ST_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_CPT_APPT_Id_FK",
                table: "Complaint",
                column: "CPT_APPT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_CPT_MST_Id_FK",
                table: "Complaint",
                column: "CPT_MST_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_CON_APPT_Id_FK",
                table: "Consultation",
                column: "CON_APPT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_CON_CD_Id_FK",
                table: "Consultation",
                column: "CON_CD_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_CON_DO_Id_FK",
                table: "Consultation",
                column: "CON_DO_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_CON_HO_Id_FK",
                table: "Consultation",
                column: "CON_HO_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_CON_PR_Id_FK",
                table: "Consultation",
                column: "CON_PR_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_CON_SP_Id_FK",
                table: "Consultation",
                column: "CON_SP_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_Dis_Id_FK",
                table: "Consultation",
                column: "Dis_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_cntry_id",
                table: "Currency",
                column: "cntry_id");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticCenters_DGSTC_DI_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_DI_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticCenters_DGSTC_HO_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_HO_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticCenters_DGSTC_ST_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_ST_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlan_DP_CON_Id_FK",
                table: "DietPlan",
                column: "DP_CON_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasesDtl_Ddtl_APPT_Id_FK",
                table: "DiseasesDtl",
                column: "Ddtl_APPT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasesDtl_Dis_Id_FK",
                table: "DiseasesDtl",
                column: "Dis_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_stat_id",
                table: "Districts",
                column: "stat_id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_CD_Id_FK",
                table: "Doctor",
                column: "DO_CD_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_Country_Id_FK",
                table: "Doctor",
                column: "DO_Country_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_DE_Id_FK",
                table: "Doctor",
                column: "DO_DE_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_DI_Id_FK",
                table: "Doctor",
                column: "DO_DI_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_HO_Id_FK",
                table: "Doctor",
                column: "DO_HO_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_QU_Id_FK",
                table: "Doctor",
                column: "DO_QU_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_SP_Id_FK",
                table: "Doctor",
                column: "DO_SP_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_ST_Id_FK",
                table: "Doctor",
                column: "DO_ST_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorLanguage_doc_Id_FK",
                table: "DoctorLanguage",
                column: "doc_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorLanguage_Lang_Id_FK",
                table: "DoctorLanguage",
                column: "Lang_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorLocation_doc_Id_FK",
                table: "DoctorLocation",
                column: "doc_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DrugMaster_DT_Id_FK",
                table: "DrugMaster",
                column: "DT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DrugMaster_UT_Id_FK",
                table: "DrugMaster",
                column: "UT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_Hos_Country_Id_FK",
                table: "Hospital",
                column: "Hos_Country_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_Hos_DI_Id_FK",
                table: "Hospital",
                column: "Hos_DI_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_Hos_NE_Id_FK",
                table: "Hospital",
                column: "Hos_NE_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_Hos_ST_Id_FK",
                table: "Hospital",
                column: "Hos_ST_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_IMG_SUBINVESTIGATIONS_Img_Invt_Id",
                table: "IMG_SUBINVESTIGATIONS",
                column: "Img_Invt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImgTest_Img_CON_Id_FK",
                table: "ImgTest",
                column: "Img_CON_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ImgTestDetails_Img_Id_FK",
                table: "ImgTestDetails",
                column: "Img_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ImgTestDetails_Img_Invst_Id",
                table: "ImgTestDetails",
                column: "Img_Invst_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImgTestDetails_Img_SubInvst_Id",
                table: "ImgTestDetails",
                column: "Img_SubInvst_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LAB_SUBINVESTIGATIONS_Lab_Invt_Id_FK",
                table: "LAB_SUBINVESTIGATIONS",
                column: "Lab_Invt_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_LabTesting_Tst_CON_Id_FK",
                table: "LabTesting",
                column: "Tst_CON_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestingDetails_Lab_Invst_Id",
                table: "LabTestingDetails",
                column: "Lab_Invst_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestingDetails_Lab_SubInvst_Id",
                table: "LabTestingDetails",
                column: "Lab_SubInvst_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestingDetails_LT_Id_FK",
                table: "LabTestingDetails",
                column: "LT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_PA_APPT_Id_FK",
                table: "Parameters",
                column: "PA_APPT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PR_Country_Id_FK",
                table: "Patient",
                column: "PR_Country_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PR_D_Id_FK",
                table: "Patient",
                column: "PR_D_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PR_RemoteHospitalName_Id_FK",
                table: "Patient",
                column: "PR_RemoteHospitalName_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PR_S_Id_FK",
                table: "Patient",
                column: "PR_S_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Prescription_DTL_DrugMst_Id_FK",
                table: "Patient_Prescription_DTL",
                column: "DrugMst_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Prescription_DTL_Rx_Id_FK",
                table: "Patient_Prescription_DTL",
                column: "Rx_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointment_Appt_CD_Id_FK",
                table: "PatientAppointment",
                column: "Appt_CD_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointment_Appt_DO_Id_FK",
                table: "PatientAppointment",
                column: "Appt_DO_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointment_Appt_PatientId_FK",
                table: "PatientAppointment",
                column: "Appt_PatientId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointment_Assi_Id_FK",
                table: "PatientAppointment",
                column: "Assi_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointment_Ref_Id_FK",
                table: "PatientAppointment",
                column: "Ref_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDocument_Doc_Type_Id_FK",
                table: "PatientDocument",
                column: "Doc_Type_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDocument_PR_Id_FK",
                table: "PatientDocument",
                column: "PR_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRxDetails_Rx_CON_Id_FK",
                table: "PatientRxDetails",
                column: "Rx_CON_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_Ph_DI_Id_FK",
                table: "Pharmacy",
                column: "Ph_DI_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_Ph_HO_Id_FK",
                table: "Pharmacy",
                column: "Ph_HO_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_Ph_ST_Id_FK",
                table: "Pharmacy",
                column: "Ph_ST_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RC_SM_Id_FK",
                table: "RoleClaims",
                column: "RC_SM_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Section_Dept_Id",
                table: "Section",
                column: "Dept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SHReferrals_SHR_Appt_Id_FK",
                table: "SHReferrals",
                column: "SHR_Appt_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_SHReferrals_SHR_CON_Id_FK",
                table: "SHReferrals",
                column: "SHR_CON_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_SHReferrals_SHR_PR_Id_FK",
                table: "SHReferrals",
                column: "SHR_PR_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_SHReferrals_SHR_Ref_D_Id_FK",
                table: "SHReferrals",
                column: "SHR_Ref_D_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSets_qualification_id",
                table: "SkillSets",
                column: "qualification_id");

            migrationBuilder.CreateIndex(
                name: "IX_Specialization_SP_CD_Id",
                table: "Specialization",
                column: "SP_CD_Id");

            migrationBuilder.CreateIndex(
                name: "IX_States_cntry_id",
                table: "States",
                column: "cntry_id");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenusDetails_SMD_SM_Id_FK",
                table: "SubMenusDetails",
                column: "SMD_SM_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenusFunctionDetails_SMFD_SMF_Id_FK",
                table: "SubMenusFunctionDetails",
                column: "SMFD_SMF_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenusFunctions_SMF_SM_Id_FK",
                table: "SubMenusFunctions",
                column: "SMF_SM_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_SubRoleClaims_SRC_SMF_Id_FK",
                table: "SubRoleClaims",
                column: "SRC_SMF_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_SYM_APPT_Id_FK",
                table: "Symptoms",
                column: "SYM_APPT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_SYM_MST_Id_FK",
                table: "Symptoms",
                column: "SYM_MST_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_DType_Id_FK",
                table: "Unit",
                column: "DType_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Vle_VL_Country_Id_FK",
                table: "Vle",
                column: "VL_Country_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Vle_VL_DI_Id_FK",
                table: "Vle",
                column: "VL_DI_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Vle_VL_QU_Id_FK",
                table: "Vle",
                column: "VL_QU_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Vle_VL_ST_Id_FK",
                table: "Vle",
                column: "VL_ST_Id_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_PatientAppointment_CPT_APPT_Id_FK",
                table: "Complaint",
                column: "CPT_APPT_Id_FK",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_PatientAppointment_CON_APPT_Id_FK",
                table: "Consultation",
                column: "CON_APPT_Id_FK",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasesDtl_PatientAppointment_Ddtl_APPT_Id_FK",
                table: "DiseasesDtl",
                column: "Ddtl_APPT_Id_FK",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_PatientAppointment_PA_APPT_Id_FK",
                table: "Parameters",
                column: "PA_APPT_Id_FK",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_SHReferrals_Ref_Id_FK",
                table: "PatientAppointment",
                column: "Ref_Id_FK",
                principalTable: "SHReferrals",
                principalColumn: "SHR_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Diseases_Dis_Id_FK",
                table: "Consultation");

            migrationBuilder.DropForeignKey(
                name: "FK_SHReferrals_Consultation_SHR_CON_Id_FK",
                table: "SHReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_SHReferrals_PatientAppointment_SHR_Appt_Id_FK",
                table: "SHReferrals");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Complaint");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "DiagnosticCenters");

            migrationBuilder.DropTable(
                name: "DietPlan");

            migrationBuilder.DropTable(
                name: "DiseasesDtl");

            migrationBuilder.DropTable(
                name: "DocPkValue");

            migrationBuilder.DropTable(
                name: "Doctor_Schedule_history");

            migrationBuilder.DropTable(
                name: "Doctor_Schedules");

            migrationBuilder.DropTable(
                name: "DoctorLanguage");

            migrationBuilder.DropTable(
                name: "DoctorLocation");

            migrationBuilder.DropTable(
                name: "Emp_Category");

            migrationBuilder.DropTable(
                name: "Emp_Type");

            migrationBuilder.DropTable(
                name: "ImgTestDetails");

            migrationBuilder.DropTable(
                name: "LabTestingDetails");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropTable(
                name: "OfficeRoles");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Patient_Prescription_DTL");

            migrationBuilder.DropTable(
                name: "PatientDocument");

            migrationBuilder.DropTable(
                name: "Pharmacy");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Relation");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "SkillSets");

            migrationBuilder.DropTable(
                name: "SubMenusDetails");

            migrationBuilder.DropTable(
                name: "SubMenusFunctionDetails");

            migrationBuilder.DropTable(
                name: "SubRoleClaims");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "UsersLists");

            migrationBuilder.DropTable(
                name: "Vle");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ComplaintMst");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "IMG_SUBINVESTIGATIONS");

            migrationBuilder.DropTable(
                name: "ImgTest");

            migrationBuilder.DropTable(
                name: "LAB_SUBINVESTIGATIONS");

            migrationBuilder.DropTable(
                name: "LabTesting");

            migrationBuilder.DropTable(
                name: "DrugMaster");

            migrationBuilder.DropTable(
                name: "PatientRxDetails");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "SubMenusFunctions");

            migrationBuilder.DropTable(
                name: "SymptomsMst");

            migrationBuilder.DropTable(
                name: "IMG_INVESTIGATIONS");

            migrationBuilder.DropTable(
                name: "LAB_INVESTIGATIONS");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "SubMenu");

            migrationBuilder.DropTable(
                name: "DrugType");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Consultation");

            migrationBuilder.DropTable(
                name: "PatientAppointment");

            migrationBuilder.DropTable(
                name: "Assistant");

            migrationBuilder.DropTable(
                name: "SHReferrals");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "Specialization");

            migrationBuilder.DropTable(
                name: "Hospital");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Network");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
