using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalApi.Data.Migrations
{
    public partial class NewChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_Discipline_Appt_CD_Id_FK",
                table: "PatientAppointment");

            migrationBuilder.RenameColumn(
                name: "Appt_CD_Id_FK",
                table: "PatientAppointment",
                newName: "CD_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointment_Appt_CD_Id_FK",
                table: "PatientAppointment",
                newName: "IX_PatientAppointment_CD_Id");

            migrationBuilder.AlterColumn<int>(
                name: "SYM_MST_Id_FK",
                table: "Symptoms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SYM_APPT_Id_FK",
                table: "Symptoms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Parameters",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "created_by",
                table: "Parameters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Dis_Id_FK",
                table: "DiseasesDtl",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Ddtl_APPT_Id_FK",
                table: "DiseasesDtl",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CPT_MST_Id_FK",
                table: "Complaint",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CPT_APPT_Id_FK",
                table: "Complaint",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_Discipline_CD_Id",
                table: "PatientAppointment",
                column: "CD_Id",
                principalTable: "Discipline",
                principalColumn: "CD_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointment_Discipline_CD_Id",
                table: "PatientAppointment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patient");

            migrationBuilder.RenameColumn(
                name: "CD_Id",
                table: "PatientAppointment",
                newName: "Appt_CD_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAppointment_CD_Id",
                table: "PatientAppointment",
                newName: "IX_PatientAppointment_Appt_CD_Id_FK");

            migrationBuilder.AlterColumn<int>(
                name: "SYM_MST_Id_FK",
                table: "Symptoms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SYM_APPT_Id_FK",
                table: "Symptoms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Parameters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "created_by",
                table: "Parameters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Dis_Id_FK",
                table: "DiseasesDtl",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Ddtl_APPT_Id_FK",
                table: "DiseasesDtl",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CPT_MST_Id_FK",
                table: "Complaint",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CPT_APPT_Id_FK",
                table: "Complaint",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointment_Discipline_Appt_CD_Id_FK",
                table: "PatientAppointment",
                column: "Appt_CD_Id_FK",
                principalTable: "Discipline",
                principalColumn: "CD_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
