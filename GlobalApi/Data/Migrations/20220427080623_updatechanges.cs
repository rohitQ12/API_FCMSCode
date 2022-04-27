using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalApi.Data.Migrations
{
    public partial class updatechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_Assistant_Assi_Id_FK",
                table: "PatientAppointment");

            migrationBuilder.DropIndex(
                name: "IX_PatientAppointment_Assi_Id_FK",
                table: "PatientAppointment");

            migrationBuilder.DropColumn(
                name: "Assi_Id_FK",
                table: "PatientAppointment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Doctor_approval_status",
                table: "PatientAppointment",
                newName: "Assi_Id");

            migrationBuilder.RenameColumn(
                name: "imagename",
                table: "AspNetUsers",
                newName: "Imagename");

            migrationBuilder.AddColumn<int>(
                name: "Smst_SP_Id_FK",
                table: "SymptomsMst",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Choose_Document",
                table: "PatientDocument",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "PR_S_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PR_RegistrationDateTime",
                table: "Patient",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "PR_Postalcode",
                table: "Patient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PR_MobileNumber",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PR_D_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PR_DOB",
                table: "Patient",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "PR_Country_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Dis_SP_Id_FK",
                table: "Diseases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cmst_SP_Id_FK",
                table: "ComplaintMst",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role_Id_FK",
                table: "AspNetUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    sts_id = table.Column<int>(type: "int", nullable: false),
                    sts_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.sts_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymptomsMst_Smst_SP_Id_FK",
                table: "SymptomsMst",
                column: "Smst_SP_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointment_Assi_Id",
                table: "PatientAppointment",
                column: "Assi_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_Dis_SP_Id_FK",
                table: "Diseases",
                column: "Dis_SP_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintMst_Cmst_SP_Id_FK",
                table: "ComplaintMst",
                column: "Cmst_SP_Id_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintMst_Specialization_Cmst_SP_Id_FK",
                table: "ComplaintMst",
                column: "Cmst_SP_Id_FK",
                principalTable: "Specialization",
                principalColumn: "SP_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Diseases_Specialization_Dis_SP_Id_FK",
                table: "Diseases",
                column: "Dis_SP_Id_FK",
                principalTable: "Specialization",
                principalColumn: "SP_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_Assistant_Assi_Id",
                table: "PatientAppointment",
                column: "Assi_Id",
                principalTable: "Assistant",
                principalColumn: "Assi_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomsMst_Specialization_Smst_SP_Id_FK",
                table: "SymptomsMst",
                column: "Smst_SP_Id_FK",
                principalTable: "Specialization",
                principalColumn: "SP_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintMst_Specialization_Cmst_SP_Id_FK",
                table: "ComplaintMst");

            migrationBuilder.DropForeignKey(
                name: "FK_Diseases_Specialization_Dis_SP_Id_FK",
                table: "Diseases");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_Assistant_Assi_Id",
                table: "PatientAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_SymptomsMst_Specialization_Smst_SP_Id_FK",
                table: "SymptomsMst");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_SymptomsMst_Smst_SP_Id_FK",
                table: "SymptomsMst");

            migrationBuilder.DropIndex(
                name: "IX_PatientAppointment_Assi_Id",
                table: "PatientAppointment");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_Dis_SP_Id_FK",
                table: "Diseases");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintMst_Cmst_SP_Id_FK",
                table: "ComplaintMst");

            migrationBuilder.DropColumn(
                name: "Smst_SP_Id_FK",
                table: "SymptomsMst");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Dis_SP_Id_FK",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Cmst_SP_Id_FK",
                table: "ComplaintMst");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Assi_Id",
                table: "PatientAppointment",
                newName: "Doctor_approval_status");

            migrationBuilder.RenameColumn(
                name: "Imagename",
                table: "AspNetUsers",
                newName: "imagename");

            migrationBuilder.AlterColumn<string>(
                name: "Choose_Document",
                table: "PatientDocument",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Assi_Id_FK",
                table: "PatientAppointment",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PR_S_Id_FK",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PR_RegistrationDateTime",
                table: "Patient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PR_Postalcode",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PR_MobileNumber",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PR_D_Id_FK",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PR_DOB",
                table: "Patient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PR_Country_Id_FK",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role_Id_FK",
                table: "AspNetUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointment_Assi_Id_FK",
                table: "PatientAppointment",
                column: "Assi_Id_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_Assistant_Assi_Id_FK",
                table: "PatientAppointment",
                column: "Assi_Id_FK",
                principalTable: "Assistant",
                principalColumn: "Assi_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
