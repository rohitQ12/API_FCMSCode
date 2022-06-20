using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalApi.Data.Migrations
{
    public partial class DrugMasterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistant_Specialization_Assi_Spe_id_fk",
                table: "Assistant");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_ComplaintMst_CPT_MST_Id_FK",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_PatientAppointment_CPT_APPT_Id_FK",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Diseases_Dis_Id_FK",
                table: "Consultation");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseasesDtl_Diseases_Dis_Id_FK",
                table: "DiseasesDtl");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseasesDtl_PatientAppointment_Ddtl_APPT_Id_FK",
                table: "DiseasesDtl");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorLanguage_Doctor_doc_Id_FK",
                table: "DoctorLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_DrugMaster_DrugType_DT_Id_FK",
                table: "DrugMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_DrugMaster_Unit_UT_Id_FK",
                table: "DrugMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_PatientAppointment_PA_APPT_Id_FK",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Prescription_DTL_DrugMaster_DrugMst_Id_FK",
                table: "Patient_Prescription_DTL");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_PatientAppointment_SYM_APPT_Id_FK",
                table: "Symptoms");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_SymptomsMst_SYM_MST_Id_FK",
                table: "Symptoms");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "DrugType");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_PA_APPT_Id_FK",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Assistant_Assi_Spe_id_fk",
                table: "Assistant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrugMaster",
                table: "DrugMaster");

            migrationBuilder.DropIndex(
                name: "IX_DrugMaster_DT_Id_FK",
                table: "DrugMaster");

            migrationBuilder.DropColumn(
                name: "VL_Taluk",
                table: "Vle");

            migrationBuilder.DropColumn(
                name: "VL_Village",
                table: "Vle");

            migrationBuilder.DropColumn(
                name: "PR_Caste",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_Nationality",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_Occupation",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_Religion",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PA_APPT_Id_FK",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "Hos_village",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "Assi_Spe_id_fk",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "Assi_Taluk",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "Assi_Village",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DrugMaster");

            migrationBuilder.DropColumn(
                name: "DrugName",
                table: "DrugMaster");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "DrugMaster");

            migrationBuilder.DropColumn(
                name: "delete_flag",
                table: "DrugMaster");

            migrationBuilder.DropColumn(
                name: "deleted_by",
                table: "DrugMaster");

            migrationBuilder.RenameTable(
                name: "DrugMaster",
                newName: "Drug_Master");

            migrationBuilder.RenameColumn(
                name: "SYM_MST_Id_FK",
                table: "Symptoms",
                newName: "Smst_Id");

            migrationBuilder.RenameColumn(
                name: "SYM_APPT_Id_FK",
                table: "Symptoms",
                newName: "MAppt_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Symptoms_SYM_MST_Id_FK",
                table: "Symptoms",
                newName: "IX_Symptoms_Smst_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Symptoms_SYM_APPT_Id_FK",
                table: "Symptoms",
                newName: "IX_Symptoms_MAppt_Id");

            migrationBuilder.RenameColumn(
                name: "Ph_Village",
                table: "Pharmacy",
                newName: "RegNo");

            migrationBuilder.RenameColumn(
                name: "PR_Taluk",
                table: "Patient",
                newName: "PR_Identity_No");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "OfficeRoles",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Hos_Taluk",
                table: "Hospital",
                newName: "RegNo");

            migrationBuilder.RenameColumn(
                name: "Hos_HospitalType",
                table: "Hospital",
                newName: "PANno");

            migrationBuilder.RenameColumn(
                name: "doc_Id_FK",
                table: "DoctorLanguage",
                newName: "DO_Id");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorLanguage_doc_Id_FK",
                table: "DoctorLanguage",
                newName: "IX_DoctorLanguage_DO_Id");

            migrationBuilder.RenameColumn(
                name: "DO_Taluk",
                table: "Doctor",
                newName: "Regno");

            migrationBuilder.RenameColumn(
                name: "Dis_Id_FK",
                table: "DiseasesDtl",
                newName: "MAppt_Id");

            migrationBuilder.RenameColumn(
                name: "Ddtl_APPT_Id_FK",
                table: "DiseasesDtl",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_DiseasesDtl_Dis_Id_FK",
                table: "DiseasesDtl",
                newName: "IX_DiseasesDtl_MAppt_Id");

            migrationBuilder.RenameIndex(
                name: "IX_DiseasesDtl_Ddtl_APPT_Id_FK",
                table: "DiseasesDtl",
                newName: "IX_DiseasesDtl_Id");

            migrationBuilder.RenameColumn(
                name: "DGSTC_Village",
                table: "DiagnosticCenters",
                newName: "RegNo");

            migrationBuilder.RenameColumn(
                name: "Dis_Id_FK",
                table: "Consultation",
                newName: "Phc_ApptId");

            migrationBuilder.RenameIndex(
                name: "IX_Consultation_Dis_Id_FK",
                table: "Consultation",
                newName: "IX_Consultation_Phc_ApptId");

            migrationBuilder.RenameColumn(
                name: "CPT_MST_Id_FK",
                table: "Complaint",
                newName: "MAppt_Id");

            migrationBuilder.RenameColumn(
                name: "CPT_APPT_Id_FK",
                table: "Complaint",
                newName: "Cmst_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_CPT_MST_Id_FK",
                table: "Complaint",
                newName: "IX_Complaint_MAppt_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_CPT_APPT_Id_FK",
                table: "Complaint",
                newName: "IX_Complaint_Cmst_Id");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Drug_Master",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "Drug_Master",
                newName: "Drg_mst_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "Drug_Master",
                newName: "Drg_strength");

            migrationBuilder.RenameColumn(
                name: "deleted_date",
                table: "Drug_Master",
                newName: "Drg_mst_deleted_date");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "Drug_Master",
                newName: "Drg_mst_created_date");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Drug_Master",
                newName: "Drg_unit_id_FK");

            migrationBuilder.RenameColumn(
                name: "UT_Id_FK",
                table: "Drug_Master",
                newName: "Drg_type_id_FK");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Drug_Master",
                newName: "Discription");

            migrationBuilder.RenameColumn(
                name: "DT_Id_FK",
                table: "Drug_Master",
                newName: "Drg_mst_id");

            migrationBuilder.RenameIndex(
                name: "IX_DrugMaster_UT_Id_FK",
                table: "Drug_Master",
                newName: "IX_Drug_Master_Drg_type_id_FK");

            migrationBuilder.AlterColumn<string>(
                name: "VLE_Code",
                table: "Vle",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Gram_id",
                table: "Vle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Vle",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Taluk_id",
                table: "Vle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Appt_Id",
                table: "Symptoms",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "state_name",
                table: "States",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "state_code",
                table: "States",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "States",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SP_Specialization",
                table: "Specialization",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SP_Code",
                table: "Specialization",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Specialization",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Skillset_name",
                table: "SkillSets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "SkillSets",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skillset_Code",
                table: "SkillSets",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "qualification_code",
                table: "Qualification",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "qualification_Name",
                table: "Qualification",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Qualification",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ph_Code",
                table: "Pharmacy",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GSTno",
                table: "Pharmacy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PANno",
                table: "Pharmacy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ph_Branch",
                table: "Pharmacy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ph_COUN_Id",
                table: "Pharmacy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ph_GR_Id",
                table: "Pharmacy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ph_Logo",
                table: "Pharmacy",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ph_NE_Id",
                table: "Pharmacy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ph_tl_Id",
                table: "Pharmacy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryOrBranch",
                table: "Pharmacy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Pharmacy",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "T_Id",
                table: "Pharmacy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cat_id",
                table: "Pharmacy",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doc_UserId_FK",
                table: "PatientDocument",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Choose_Document",
                table: "PatientDocument",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "PatientAppointment",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnderBPMedication",
                table: "PatientAppointment",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnderSugarMedication",
                table: "PatientAppointment",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PR_MotherTongue",
                table: "Patient",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PR_LandlineNo",
                table: "Patient",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PR_Alternative_No",
                table: "Patient",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PR_CAT_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PR_Gram_Id",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PR_IDN_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PR_INU_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PR_Insured_Sum",
                table: "Patient",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PR_NAL_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PR_OCU_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PR_REG_Id_FK",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PR_RegNo",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PR_Taluk_Id",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_Weight",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "PA_UserId_FK",
                table: "Parameters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PA_TempInFahrenheit",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PA_TempInCelsius",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PA_Sugar",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PA_RespiratoryRate",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PA_PulseRate",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PA_OxygenSaturation",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PA_Height",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PA_ECG",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PA_Code",
                table: "Parameters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PA_BloodPressure",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "Appt_Id",
                table: "Parameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MAppt_Id",
                table: "Parameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PA_Hemoglobin",
                table: "Parameters",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "OfficeRoles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NE_Description",
                table: "Network",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NE_Code",
                table: "Network",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Network",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hos_HospitalName",
                table: "Hospital",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hos_HospitalCode",
                table: "Hospital",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Hos_Branch",
                table: "Hospital",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GSTno",
                table: "Hospital",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hos_Gram_Id",
                table: "Hospital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hos_Taluk_Id",
                table: "Hospital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hos_Type_Id",
                table: "Hospital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hos_cat_Id",
                table: "Hospital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Hospital",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Doctor",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DO_UserId_FK",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DO_ST_Id_FK",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DO_PostalCode",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DO_DOB",
                table: "Doctor",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DO_DI_Id_FK",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DO_Country_Id_FK",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DO_Code",
                table: "Doctor",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DO_Gram_Id",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DO_MotherTongue",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DO_RegNo",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DO_Taluk_Id",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GSTno",
                table: "Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PANno",
                table: "Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Doctor",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "district_name",
                table: "Districts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "district_code",
                table: "Districts",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Districts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cntry_id",
                table: "Districts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Appt_Id",
                table: "DiseasesDtl",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CD_Code",
                table: "Discipline",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CD_ClinicalDiscipline",
                table: "Discipline",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Discipline",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DGSTC_Name",
                table: "DiagnosticCenters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DGSTC_Code",
                table: "DiagnosticCenters",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DGSTC_Branch",
                table: "DiagnosticCenters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DGSTC_COUN_Id_FK",
                table: "DiagnosticCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DGSTC_GR_Id_FK",
                table: "DiagnosticCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DGSTC_Logo",
                table: "DiagnosticCenters",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DGSTC_NE_Id",
                table: "DiagnosticCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DGSTC_TL_Id_FK",
                table: "DiagnosticCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DGSTC_Type_Id",
                table: "DiagnosticCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GSTno",
                table: "DiagnosticCenters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PANno",
                table: "DiagnosticCenters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryOrBranch",
                table: "DiagnosticCenters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "DiagnosticCenters",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cat_id",
                table: "DiagnosticCenters",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "designation_desc",
                table: "Designation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "designation_code",
                table: "Designation",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Designation",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country_name",
                table: "Countries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "country_code",
                table: "Countries",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Countries",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CON_ConsultedDate",
                table: "Consultation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CON_APPT_Id_FK",
                table: "Consultation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CON_ConsultedTime",
                table: "Consultation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Consultation",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "created_by",
                table: "Complaint",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Appt_Id",
                table: "Complaint",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Assi_code",
                table: "Assistant",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Assi_ST_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Assi_Qua_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Assi_PostalCode",
                table: "Assistant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Assi_Hos_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Assi_Des_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Assi_DI_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Assi_Country_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ASISfxPrfxId",
                table: "Assistant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Assi_MotherTongue",
                table: "Assistant",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Assi_skill_id",
                table: "Assistant",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Assistant",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "gram_Id_Fk",
                table: "Assistant",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "taluk_Id_Fk",
                table: "Assistant",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rolecategory",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Drug_Master",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Drg_mst_created_by",
                table: "Drug_Master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Drg_mst_deletd_by",
                table: "Drug_Master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Drg_mst_delete_flag",
                table: "Drug_Master",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Drg_mst_modified_by",
                table: "Drug_Master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Drg_name",
                table: "Drug_Master",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Drug_Master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drug_Master",
                table: "Drug_Master",
                column: "Drg_mst_id");

            migrationBuilder.CreateTable(
                name: "AllergySigns",
                columns: table => new
                {
                    Al_Id = table.Column<int>(type: "int", nullable: false),
                    Al_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Al_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_AllergySigns", x => x.Al_Id);
                });

            migrationBuilder.CreateTable(
                name: "Caste_MST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nationality_ID_FK = table.Column<int>(type: "int", nullable: false),
                    Religion_ID_FK = table.Column<int>(type: "int", nullable: false),
                    Caste = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Caste_MST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnoCategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_DiagnoCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_DiagnosticType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDocument",
                columns: table => new
                {
                    DDoc_Id = table.Column<int>(type: "int", nullable: false),
                    DO_Id = table.Column<int>(type: "int", nullable: true),
                    doctype_id = table.Column<int>(type: "int", nullable: true),
                    Choose_Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doc_UserId_FK = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_DoctorDocument", x => x.DDoc_Id);
                    table.ForeignKey(
                        name: "FK_DoctorDocument_Doctor_DO_Id",
                        column: x => x.DO_Id,
                        principalTable: "Doctor",
                        principalColumn: "DO_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorDocument_DocumentType_doctype_id",
                        column: x => x.doctype_id,
                        principalTable: "DocumentType",
                        principalColumn: "doctype_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drug_Frequency",
                columns: table => new
                {
                    Drg_freq_Id = table.Column<int>(type: "int", nullable: false),
                    Drg_frq_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_frq_order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_frq_created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_frq_created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Drg_frq_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_frq_modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Drg_frq_deleted_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_frq_deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Drg_frq_delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug_Frequency", x => x.Drg_freq_Id);
                });

            migrationBuilder.CreateTable(
                name: "Drug_Type",
                columns: table => new
                {
                    Drug_type_Id = table.Column<int>(type: "int", nullable: false),
                    Drg_type_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Drg_type_created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_type_created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Drg_type_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_type_modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Drg_type_deleted_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_type_deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Drg_type_delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug_Type", x => x.Drug_type_Id);
                });

            migrationBuilder.CreateTable(
                name: "Hos_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Hos_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity_DOC_MST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DOC_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Identity_DOC_MST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insurer_MST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Insurer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Insurer_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Insurer_Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Insurer_MST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language_MST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Language_MST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManualAppointment",
                columns: table => new
                {
                    MAppt_Id = table.Column<int>(type: "int", nullable: false),
                    Appt_PatientId_FK = table.Column<int>(type: "int", nullable: true),
                    CD_Id = table.Column<int>(type: "int", nullable: true),
                    Appt_DO_Id_FK = table.Column<int>(type: "int", nullable: true),
                    Hos_Id = table.Column<int>(type: "int", nullable: true),
                    Appt_DateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    Select_day = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Select_FrmTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Select_toTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Appt_Is_active = table.Column<int>(type: "int", nullable: true),
                    Appt_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Assi_Id = table.Column<int>(type: "int", nullable: true),
                    Ref_Id_FK = table.Column<int>(type: "int", nullable: true),
                    UnderBPMedication = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    UnderSugarMedication = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualAppointment", x => x.MAppt_Id);
                    table.ForeignKey(
                        name: "FK_ManualAppointment_Assistant_Assi_Id",
                        column: x => x.Assi_Id,
                        principalTable: "Assistant",
                        principalColumn: "Assi_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualAppointment_Discipline_CD_Id",
                        column: x => x.CD_Id,
                        principalTable: "Discipline",
                        principalColumn: "CD_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualAppointment_Doctor_Appt_DO_Id_FK",
                        column: x => x.Appt_DO_Id_FK,
                        principalTable: "Doctor",
                        principalColumn: "DO_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualAppointment_Hospital_Hos_Id",
                        column: x => x.Hos_Id,
                        principalTable: "Hospital",
                        principalColumn: "Hos_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualAppointment_Patient_Appt_PatientId_FK",
                        column: x => x.Appt_PatientId_FK,
                        principalTable: "Patient",
                        principalColumn: "PR_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualAppointment_SHReferrals_Ref_Id_FK",
                        column: x => x.Ref_Id_FK,
                        principalTable: "SHReferrals",
                        principalColumn: "SHR_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nationality_MST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Nationality_MST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Occupation_MST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Occupation_MST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientHealthRecords",
                columns: table => new
                {
                    PHR_Id = table.Column<int>(type: "int", nullable: false),
                    Appt_Id = table.Column<int>(type: "int", nullable: true),
                    Choose_Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doc_UserId_FK = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_PatientHealthRecords", x => x.PHR_Id);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyCategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_PharmacyCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_PharmacyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religion_MST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nationality_ID_FK = table.Column<int>(type: "int", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Religion_MST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuffixPrefix",
                columns: table => new
                {
                    SuffixprefixId = table.Column<int>(type: "int", nullable: false),
                    DocPkTblId = table.Column<int>(type: "int", nullable: true),
                    StartIndex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Suffix = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WidthOfNumericalPart = table.Column<int>(type: "int", nullable: false),
                    PrefillWithZero = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuffixPrefix", x => x.SuffixprefixId);
                    table.ForeignKey(
                        name: "FK_SuffixPrefix_DocPkValue_DocPkTblId",
                        column: x => x.DocPkTblId,
                        principalTable: "DocPkValue",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Taluk",
                columns: table => new
                {
                    Taluk_id = table.Column<int>(type: "int", nullable: false),
                    Taluk_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Taluk_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cntry_id = table.Column<int>(type: "int", nullable: false),
                    state_id = table.Column<int>(type: "int", nullable: false),
                    district_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taluk", x => x.Taluk_id);
                    table.ForeignKey(
                        name: "FK_Taluk_Countries_cntry_id",
                        column: x => x.cntry_id,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taluk_Districts_district_id",
                        column: x => x.district_id,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taluk_States_state_id",
                        column: x => x.state_id,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drug_Units",
                columns: table => new
                {
                    Drg_unit_id = table.Column<int>(type: "int", nullable: false),
                    Drg_Type_Id_FK = table.Column<int>(type: "int", nullable: false),
                    Drg_Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Drg_unit_created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_unit_created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Drg_unit_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_unit_modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Drg_unit_deleted_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drg_unit_deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Drg_unit_delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug_Units", x => x.Drg_unit_id);
                    table.ForeignKey(
                        name: "FK_Drug_Units_Drug_Type_Drg_Type_Id_FK",
                        column: x => x.Drg_Type_Id_FK,
                        principalTable: "Drug_Type",
                        principalColumn: "Drug_type_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllergySigns_DTL",
                columns: table => new
                {
                    Ddtl_Id = table.Column<int>(type: "int", nullable: false),
                    Al_Id = table.Column<int>(type: "int", nullable: true),
                    Appt_Id = table.Column<int>(type: "int", nullable: true),
                    MAppt_Id = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_AllergySigns_DTL", x => x.Ddtl_Id);
                    table.ForeignKey(
                        name: "FK_AllergySigns_DTL_AllergySigns_Al_Id",
                        column: x => x.Al_Id,
                        principalTable: "AllergySigns",
                        principalColumn: "Al_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllergySigns_DTL_ManualAppointment_MAppt_Id",
                        column: x => x.MAppt_Id,
                        principalTable: "ManualAppointment",
                        principalColumn: "MAppt_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllergySigns_DTL_PatientAppointment_Appt_Id",
                        column: x => x.Appt_Id,
                        principalTable: "PatientAppointment",
                        principalColumn: "Appt_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gram",
                columns: table => new
                {
                    Gram_id = table.Column<int>(type: "int", nullable: false),
                    Gram_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Gram_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cntry_id = table.Column<int>(type: "int", nullable: false),
                    state_id = table.Column<int>(type: "int", nullable: false),
                    dist_id = table.Column<int>(type: "int", nullable: false),
                    Taluk_id = table.Column<int>(type: "int", nullable: false),
                    Postal_Code = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gram", x => x.Gram_id);
                    table.ForeignKey(
                        name: "FK_Gram_Countries_cntry_id",
                        column: x => x.cntry_id,
                        principalTable: "Countries",
                        principalColumn: "cntry_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gram_Districts_dist_id",
                        column: x => x.dist_id,
                        principalTable: "Districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gram_States_state_id",
                        column: x => x.state_id,
                        principalTable: "States",
                        principalColumn: "stat_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gram_Taluk_Taluk_id",
                        column: x => x.Taluk_id,
                        principalTable: "Taluk",
                        principalColumn: "Taluk_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vle_Gram_id",
                table: "Vle",
                column: "Gram_id");

            migrationBuilder.CreateIndex(
                name: "IX_Vle_Taluk_id",
                table: "Vle",
                column: "Taluk_id");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_Appt_Id",
                table: "Symptoms",
                column: "Appt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_Ph_COUN_Id",
                table: "Pharmacy",
                column: "Ph_COUN_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_Ph_GR_Id",
                table: "Pharmacy",
                column: "Ph_GR_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_Ph_NE_Id",
                table: "Pharmacy",
                column: "Ph_NE_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_Ph_tl_Id",
                table: "Pharmacy",
                column: "Ph_tl_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_T_Id",
                table: "Pharmacy",
                column: "T_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PR_Gram_Id",
                table: "Patient",
                column: "PR_Gram_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PR_Taluk_Id",
                table: "Patient",
                column: "PR_Taluk_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_Appt_Id",
                table: "Parameters",
                column: "Appt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_MAppt_Id",
                table: "Parameters",
                column: "MAppt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_Hos_Gram_Id",
                table: "Hospital",
                column: "Hos_Gram_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_Hos_Taluk_Id",
                table: "Hospital",
                column: "Hos_Taluk_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_Gram_Id",
                table: "Doctor",
                column: "DO_Gram_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DO_Taluk_Id",
                table: "Doctor",
                column: "DO_Taluk_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_cntry_id",
                table: "Districts",
                column: "cntry_id");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasesDtl_Appt_Id",
                table: "DiseasesDtl",
                column: "Appt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticCenters_DGSTC_COUN_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_COUN_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticCenters_DGSTC_GR_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_GR_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticCenters_DGSTC_NE_Id",
                table: "DiagnosticCenters",
                column: "DGSTC_NE_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticCenters_DGSTC_TL_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_TL_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticCenters_DGSTC_Type_Id",
                table: "DiagnosticCenters",
                column: "DGSTC_Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_Appt_Id",
                table: "Complaint",
                column: "Appt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Drug_Master_Drg_unit_id_FK",
                table: "Drug_Master",
                column: "Drg_unit_id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_AllergySigns_DTL_Al_Id",
                table: "AllergySigns_DTL",
                column: "Al_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AllergySigns_DTL_Appt_Id",
                table: "AllergySigns_DTL",
                column: "Appt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AllergySigns_DTL_MAppt_Id",
                table: "AllergySigns_DTL",
                column: "MAppt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Caste_MST_Caste_status_Nationality_ID_FK_Religion_ID_FK",
                table: "Caste_MST",
                columns: new[] { "Caste", "status", "Nationality_ID_FK", "Religion_ID_FK" },
                unique: true,
                filter: "[Caste] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDocument_DO_Id",
                table: "DoctorDocument",
                column: "DO_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDocument_doctype_id",
                table: "DoctorDocument",
                column: "doctype_id");

            migrationBuilder.CreateIndex(
                name: "IX_Drug_Units_Drg_Type_Id_FK",
                table: "Drug_Units",
                column: "Drg_Type_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Gram_cntry_id",
                table: "Gram",
                column: "cntry_id");

            migrationBuilder.CreateIndex(
                name: "IX_Gram_dist_id",
                table: "Gram",
                column: "dist_id");

            migrationBuilder.CreateIndex(
                name: "IX_Gram_state_id",
                table: "Gram",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_Gram_Taluk_id",
                table: "Gram",
                column: "Taluk_id");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_DOC_MST_DOC_Name_status",
                table: "Identity_DOC_MST",
                columns: new[] { "DOC_Name", "status" },
                unique: true,
                filter: "[DOC_Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Language_MST_Language_status",
                table: "Language_MST",
                columns: new[] { "Language", "status" },
                unique: true,
                filter: "[Language] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ManualAppointment_Appt_DO_Id_FK",
                table: "ManualAppointment",
                column: "Appt_DO_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ManualAppointment_Appt_PatientId_FK",
                table: "ManualAppointment",
                column: "Appt_PatientId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ManualAppointment_Assi_Id",
                table: "ManualAppointment",
                column: "Assi_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ManualAppointment_CD_Id",
                table: "ManualAppointment",
                column: "CD_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ManualAppointment_Hos_Id",
                table: "ManualAppointment",
                column: "Hos_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ManualAppointment_Ref_Id_FK",
                table: "ManualAppointment",
                column: "Ref_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Nationality_MST_Nationality_status",
                table: "Nationality_MST",
                columns: new[] { "Nationality", "status" },
                unique: true,
                filter: "[Nationality] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Occupation_MST_Occupation_status",
                table: "Occupation_MST",
                columns: new[] { "Occupation", "status" },
                unique: true,
                filter: "[Occupation] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Religion_MST_Religion_status_Nationality_ID_FK",
                table: "Religion_MST",
                columns: new[] { "Religion", "status", "Nationality_ID_FK" },
                unique: true,
                filter: "[Religion] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SuffixPrefix_DocPkTblId",
                table: "SuffixPrefix",
                column: "DocPkTblId");

            migrationBuilder.CreateIndex(
                name: "IX_Taluk_cntry_id",
                table: "Taluk",
                column: "cntry_id");

            migrationBuilder.CreateIndex(
                name: "IX_Taluk_district_id",
                table: "Taluk",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_Taluk_state_id",
                table: "Taluk",
                column: "state_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_ComplaintMst_Cmst_Id",
                table: "Complaint",
                column: "Cmst_Id",
                principalTable: "ComplaintMst",
                principalColumn: "Cmst_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_ManualAppointment_MAppt_Id",
                table: "Complaint",
                column: "MAppt_Id",
                principalTable: "ManualAppointment",
                principalColumn: "MAppt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_PatientAppointment_Appt_Id",
                table: "Complaint",
                column: "Appt_Id",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_ManualAppointment_Phc_ApptId",
                table: "Consultation",
                column: "Phc_ApptId",
                principalTable: "ManualAppointment",
                principalColumn: "MAppt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticCenters_Countries_DGSTC_COUN_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_COUN_Id_FK",
                principalTable: "Countries",
                principalColumn: "cntry_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticCenters_DiagnosticType_DGSTC_Type_Id",
                table: "DiagnosticCenters",
                column: "DGSTC_Type_Id",
                principalTable: "DiagnosticType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticCenters_Gram_DGSTC_GR_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_GR_Id_FK",
                principalTable: "Gram",
                principalColumn: "Gram_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticCenters_Network_DGSTC_NE_Id",
                table: "DiagnosticCenters",
                column: "DGSTC_NE_Id",
                principalTable: "Network",
                principalColumn: "NE_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticCenters_Taluk_DGSTC_TL_Id_FK",
                table: "DiagnosticCenters",
                column: "DGSTC_TL_Id_FK",
                principalTable: "Taluk",
                principalColumn: "Taluk_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasesDtl_Diseases_Id",
                table: "DiseasesDtl",
                column: "Id",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasesDtl_ManualAppointment_MAppt_Id",
                table: "DiseasesDtl",
                column: "MAppt_Id",
                principalTable: "ManualAppointment",
                principalColumn: "MAppt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasesDtl_PatientAppointment_Appt_Id",
                table: "DiseasesDtl",
                column: "Appt_Id",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Countries_cntry_id",
                table: "Districts",
                column: "cntry_id",
                principalTable: "Countries",
                principalColumn: "cntry_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Gram_DO_Gram_Id",
                table: "Doctor",
                column: "DO_Gram_Id",
                principalTable: "Gram",
                principalColumn: "Gram_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Taluk_DO_Taluk_Id",
                table: "Doctor",
                column: "DO_Taluk_Id",
                principalTable: "Taluk",
                principalColumn: "Taluk_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorLanguage_Doctor_DO_Id",
                table: "DoctorLanguage",
                column: "DO_Id",
                principalTable: "Doctor",
                principalColumn: "DO_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_Master_Drug_Type_Drg_type_id_FK",
                table: "Drug_Master",
                column: "Drg_type_id_FK",
                principalTable: "Drug_Type",
                principalColumn: "Drug_type_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_Master_Drug_Units_Drg_unit_id_FK",
                table: "Drug_Master",
                column: "Drg_unit_id_FK",
                principalTable: "Drug_Units",
                principalColumn: "Drg_unit_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_Gram_Hos_Gram_Id",
                table: "Hospital",
                column: "Hos_Gram_Id",
                principalTable: "Gram",
                principalColumn: "Gram_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_Taluk_Hos_Taluk_Id",
                table: "Hospital",
                column: "Hos_Taluk_Id",
                principalTable: "Taluk",
                principalColumn: "Taluk_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_ManualAppointment_MAppt_Id",
                table: "Parameters",
                column: "MAppt_Id",
                principalTable: "ManualAppointment",
                principalColumn: "MAppt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_PatientAppointment_Appt_Id",
                table: "Parameters",
                column: "Appt_Id",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Gram_PR_Gram_Id",
                table: "Patient",
                column: "PR_Gram_Id",
                principalTable: "Gram",
                principalColumn: "Gram_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Taluk_PR_Taluk_Id",
                table: "Patient",
                column: "PR_Taluk_Id",
                principalTable: "Taluk",
                principalColumn: "Taluk_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Prescription_DTL_Drug_Master_DrugMst_Id_FK",
                table: "Patient_Prescription_DTL",
                column: "DrugMst_Id_FK",
                principalTable: "Drug_Master",
                principalColumn: "Drg_mst_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacy_Countries_Ph_COUN_Id",
                table: "Pharmacy",
                column: "Ph_COUN_Id",
                principalTable: "Countries",
                principalColumn: "cntry_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacy_Gram_Ph_GR_Id",
                table: "Pharmacy",
                column: "Ph_GR_Id",
                principalTable: "Gram",
                principalColumn: "Gram_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacy_Network_Ph_NE_Id",
                table: "Pharmacy",
                column: "Ph_NE_Id",
                principalTable: "Network",
                principalColumn: "NE_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacy_PharmacyType_T_Id",
                table: "Pharmacy",
                column: "T_Id",
                principalTable: "PharmacyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacy_Taluk_Ph_tl_Id",
                table: "Pharmacy",
                column: "Ph_tl_Id",
                principalTable: "Taluk",
                principalColumn: "Taluk_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_ManualAppointment_MAppt_Id",
                table: "Symptoms",
                column: "MAppt_Id",
                principalTable: "ManualAppointment",
                principalColumn: "MAppt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_PatientAppointment_Appt_Id",
                table: "Symptoms",
                column: "Appt_Id",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_SymptomsMst_Smst_Id",
                table: "Symptoms",
                column: "Smst_Id",
                principalTable: "SymptomsMst",
                principalColumn: "Smst_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vle_Gram_Gram_id",
                table: "Vle",
                column: "Gram_id",
                principalTable: "Gram",
                principalColumn: "Gram_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vle_Taluk_Taluk_id",
                table: "Vle",
                column: "Taluk_id",
                principalTable: "Taluk",
                principalColumn: "Taluk_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_ComplaintMst_Cmst_Id",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_ManualAppointment_MAppt_Id",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_PatientAppointment_Appt_Id",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_ManualAppointment_Phc_ApptId",
                table: "Consultation");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticCenters_Countries_DGSTC_COUN_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticCenters_DiagnosticType_DGSTC_Type_Id",
                table: "DiagnosticCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticCenters_Gram_DGSTC_GR_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticCenters_Network_DGSTC_NE_Id",
                table: "DiagnosticCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticCenters_Taluk_DGSTC_TL_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseasesDtl_Diseases_Id",
                table: "DiseasesDtl");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseasesDtl_ManualAppointment_MAppt_Id",
                table: "DiseasesDtl");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseasesDtl_PatientAppointment_Appt_Id",
                table: "DiseasesDtl");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Countries_cntry_id",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Gram_DO_Gram_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Taluk_DO_Taluk_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorLanguage_Doctor_DO_Id",
                table: "DoctorLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_Drug_Master_Drug_Type_Drg_type_id_FK",
                table: "Drug_Master");

            migrationBuilder.DropForeignKey(
                name: "FK_Drug_Master_Drug_Units_Drg_unit_id_FK",
                table: "Drug_Master");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_Gram_Hos_Gram_Id",
                table: "Hospital");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_Taluk_Hos_Taluk_Id",
                table: "Hospital");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_ManualAppointment_MAppt_Id",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_PatientAppointment_Appt_Id",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Gram_PR_Gram_Id",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Taluk_PR_Taluk_Id",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Prescription_DTL_Drug_Master_DrugMst_Id_FK",
                table: "Patient_Prescription_DTL");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacy_Countries_Ph_COUN_Id",
                table: "Pharmacy");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacy_Gram_Ph_GR_Id",
                table: "Pharmacy");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacy_Network_Ph_NE_Id",
                table: "Pharmacy");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacy_PharmacyType_T_Id",
                table: "Pharmacy");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacy_Taluk_Ph_tl_Id",
                table: "Pharmacy");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_ManualAppointment_MAppt_Id",
                table: "Symptoms");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_PatientAppointment_Appt_Id",
                table: "Symptoms");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_SymptomsMst_Smst_Id",
                table: "Symptoms");

            migrationBuilder.DropForeignKey(
                name: "FK_Vle_Gram_Gram_id",
                table: "Vle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vle_Taluk_Taluk_id",
                table: "Vle");

            migrationBuilder.DropTable(
                name: "AllergySigns_DTL");

            migrationBuilder.DropTable(
                name: "Caste_MST");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "DiagnoCategory");

            migrationBuilder.DropTable(
                name: "DiagnosticType");

            migrationBuilder.DropTable(
                name: "DoctorDocument");

            migrationBuilder.DropTable(
                name: "Drug_Frequency");

            migrationBuilder.DropTable(
                name: "Drug_Units");

            migrationBuilder.DropTable(
                name: "Gram");

            migrationBuilder.DropTable(
                name: "Hos_Type");

            migrationBuilder.DropTable(
                name: "Identity_DOC_MST");

            migrationBuilder.DropTable(
                name: "Insurer_MST");

            migrationBuilder.DropTable(
                name: "Language_MST");

            migrationBuilder.DropTable(
                name: "Nationality_MST");

            migrationBuilder.DropTable(
                name: "Occupation_MST");

            migrationBuilder.DropTable(
                name: "PatientHealthRecords");

            migrationBuilder.DropTable(
                name: "PharmacyCategory");

            migrationBuilder.DropTable(
                name: "PharmacyType");

            migrationBuilder.DropTable(
                name: "Religion_MST");

            migrationBuilder.DropTable(
                name: "SuffixPrefix");

            migrationBuilder.DropTable(
                name: "AllergySigns");

            migrationBuilder.DropTable(
                name: "ManualAppointment");

            migrationBuilder.DropTable(
                name: "Drug_Type");

            migrationBuilder.DropTable(
                name: "Taluk");

            migrationBuilder.DropIndex(
                name: "IX_Vle_Gram_id",
                table: "Vle");

            migrationBuilder.DropIndex(
                name: "IX_Vle_Taluk_id",
                table: "Vle");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_Appt_Id",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacy_Ph_COUN_Id",
                table: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacy_Ph_GR_Id",
                table: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacy_Ph_NE_Id",
                table: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacy_Ph_tl_Id",
                table: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacy_T_Id",
                table: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_Patient_PR_Gram_Id",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_PR_Taluk_Id",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_Appt_Id",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_MAppt_Id",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Hospital_Hos_Gram_Id",
                table: "Hospital");

            migrationBuilder.DropIndex(
                name: "IX_Hospital_Hos_Taluk_Id",
                table: "Hospital");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_DO_Gram_Id",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_DO_Taluk_Id",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Districts_cntry_id",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_DiseasesDtl_Appt_Id",
                table: "DiseasesDtl");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticCenters_DGSTC_COUN_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticCenters_DGSTC_GR_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticCenters_DGSTC_NE_Id",
                table: "DiagnosticCenters");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticCenters_DGSTC_TL_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticCenters_DGSTC_Type_Id",
                table: "DiagnosticCenters");

            migrationBuilder.DropIndex(
                name: "IX_Complaint_Appt_Id",
                table: "Complaint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drug_Master",
                table: "Drug_Master");

            migrationBuilder.DropIndex(
                name: "IX_Drug_Master_Drg_unit_id_FK",
                table: "Drug_Master");

            migrationBuilder.DropColumn(
                name: "Gram_id",
                table: "Vle");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Vle");

            migrationBuilder.DropColumn(
                name: "Taluk_id",
                table: "Vle");

            migrationBuilder.DropColumn(
                name: "Appt_Id",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "States");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Specialization");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "SkillSets");

            migrationBuilder.DropColumn(
                name: "Skillset_Code",
                table: "SkillSets");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "GSTno",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "PANno",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "Ph_Branch",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "Ph_COUN_Id",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "Ph_GR_Id",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "Ph_Logo",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "Ph_NE_Id",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "Ph_tl_Id",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "PrimaryOrBranch",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "T_Id",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "cat_id",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "PatientAppointment");

            migrationBuilder.DropColumn(
                name: "UnderBPMedication",
                table: "PatientAppointment");

            migrationBuilder.DropColumn(
                name: "UnderSugarMedication",
                table: "PatientAppointment");

            migrationBuilder.DropColumn(
                name: "PR_CAT_Id_FK",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_Gram_Id",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_IDN_Id_FK",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_INU_Id_FK",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_Insured_Sum",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_NAL_Id_FK",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_OCU_Id_FK",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_REG_Id_FK",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_RegNo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PR_Taluk_Id",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Appt_Id",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "MAppt_Id",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "PA_Hemoglobin",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Network");

            migrationBuilder.DropColumn(
                name: "GSTno",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "Hos_Gram_Id",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "Hos_Taluk_Id",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "Hos_Type_Id",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "Hos_cat_Id",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "DO_Gram_Id",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DO_MotherTongue",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DO_RegNo",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DO_Taluk_Id",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "GSTno",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PANno",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "cntry_id",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Appt_Id",
                table: "DiseasesDtl");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Discipline");

            migrationBuilder.DropColumn(
                name: "DGSTC_Branch",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "DGSTC_COUN_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "DGSTC_GR_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "DGSTC_Logo",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "DGSTC_NE_Id",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "DGSTC_TL_Id_FK",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "DGSTC_Type_Id",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "GSTno",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "PANno",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "PrimaryOrBranch",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "cat_id",
                table: "DiagnosticCenters");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Designation");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CON_ConsultedTime",
                table: "Consultation");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Consultation");

            migrationBuilder.DropColumn(
                name: "Appt_Id",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "ASISfxPrfxId",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "Assi_MotherTongue",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "Assi_skill_id",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "gram_Id_Fk",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "taluk_Id_Fk",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rolecategory",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Drg_mst_created_by",
                table: "Drug_Master");

            migrationBuilder.DropColumn(
                name: "Drg_mst_deletd_by",
                table: "Drug_Master");

            migrationBuilder.DropColumn(
                name: "Drg_mst_delete_flag",
                table: "Drug_Master");

            migrationBuilder.DropColumn(
                name: "Drg_mst_modified_by",
                table: "Drug_Master");

            migrationBuilder.DropColumn(
                name: "Drg_name",
                table: "Drug_Master");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Drug_Master");

            migrationBuilder.RenameTable(
                name: "Drug_Master",
                newName: "DrugMaster");

            migrationBuilder.RenameColumn(
                name: "Smst_Id",
                table: "Symptoms",
                newName: "SYM_MST_Id_FK");

            migrationBuilder.RenameColumn(
                name: "MAppt_Id",
                table: "Symptoms",
                newName: "SYM_APPT_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_Symptoms_Smst_Id",
                table: "Symptoms",
                newName: "IX_Symptoms_SYM_MST_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_Symptoms_MAppt_Id",
                table: "Symptoms",
                newName: "IX_Symptoms_SYM_APPT_Id_FK");

            migrationBuilder.RenameColumn(
                name: "RegNo",
                table: "Pharmacy",
                newName: "Ph_Village");

            migrationBuilder.RenameColumn(
                name: "PR_Identity_No",
                table: "Patient",
                newName: "PR_Taluk");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OfficeRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "RegNo",
                table: "Hospital",
                newName: "Hos_Taluk");

            migrationBuilder.RenameColumn(
                name: "PANno",
                table: "Hospital",
                newName: "Hos_HospitalType");

            migrationBuilder.RenameColumn(
                name: "DO_Id",
                table: "DoctorLanguage",
                newName: "doc_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorLanguage_DO_Id",
                table: "DoctorLanguage",
                newName: "IX_DoctorLanguage_doc_Id_FK");

            migrationBuilder.RenameColumn(
                name: "Regno",
                table: "Doctor",
                newName: "DO_Taluk");

            migrationBuilder.RenameColumn(
                name: "MAppt_Id",
                table: "DiseasesDtl",
                newName: "Dis_Id_FK");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DiseasesDtl",
                newName: "Ddtl_APPT_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_DiseasesDtl_MAppt_Id",
                table: "DiseasesDtl",
                newName: "IX_DiseasesDtl_Dis_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_DiseasesDtl_Id",
                table: "DiseasesDtl",
                newName: "IX_DiseasesDtl_Ddtl_APPT_Id_FK");

            migrationBuilder.RenameColumn(
                name: "RegNo",
                table: "DiagnosticCenters",
                newName: "DGSTC_Village");

            migrationBuilder.RenameColumn(
                name: "Phc_ApptId",
                table: "Consultation",
                newName: "Dis_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_Consultation_Phc_ApptId",
                table: "Consultation",
                newName: "IX_Consultation_Dis_Id_FK");

            migrationBuilder.RenameColumn(
                name: "MAppt_Id",
                table: "Complaint",
                newName: "CPT_MST_Id_FK");

            migrationBuilder.RenameColumn(
                name: "Cmst_Id",
                table: "Complaint",
                newName: "CPT_APPT_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_MAppt_Id",
                table: "Complaint",
                newName: "IX_Complaint_CPT_MST_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_Cmst_Id",
                table: "Complaint",
                newName: "IX_Complaint_CPT_APPT_Id_FK");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DrugMaster",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Drg_unit_id_FK",
                table: "DrugMaster",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Drg_type_id_FK",
                table: "DrugMaster",
                newName: "UT_Id_FK");

            migrationBuilder.RenameColumn(
                name: "Drg_strength",
                table: "DrugMaster",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "Drg_mst_modified_date",
                table: "DrugMaster",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "Drg_mst_deleted_date",
                table: "DrugMaster",
                newName: "deleted_date");

            migrationBuilder.RenameColumn(
                name: "Drg_mst_created_date",
                table: "DrugMaster",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "DrugMaster",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Drg_mst_id",
                table: "DrugMaster",
                newName: "DT_Id_FK");

            migrationBuilder.RenameIndex(
                name: "IX_Drug_Master_Drg_type_id_FK",
                table: "DrugMaster",
                newName: "IX_DrugMaster_UT_Id_FK");

            migrationBuilder.AlterColumn<int>(
                name: "VLE_Code",
                table: "Vle",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VL_Taluk",
                table: "Vle",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VL_Village",
                table: "Vle",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "state_name",
                table: "States",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "state_code",
                table: "States",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SP_Specialization",
                table: "Specialization",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SP_Code",
                table: "Specialization",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Skillset_name",
                table: "SkillSets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "qualification_code",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "qualification_Name",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Ph_Code",
                table: "Pharmacy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doc_UserId_FK",
                table: "PatientDocument",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Choose_Document",
                table: "PatientDocument",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PR_MotherTongue",
                table: "Patient",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PR_LandlineNo",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PR_Alternative_No",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PR_Caste",
                table: "Patient",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PR_Nationality",
                table: "Patient",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PR_Occupation",
                table: "Patient",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PR_Religion",
                table: "Patient",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_Weight",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PA_UserId_FK",
                table: "Parameters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_TempInFahrenheit",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_TempInCelsius",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_Sugar",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_RespiratoryRate",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_PulseRate",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_OxygenSaturation",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_Height",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_ECG",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_Code",
                table: "Parameters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PA_BloodPressure",
                table: "Parameters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PA_APPT_Id_FK",
                table: "Parameters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "OfficeRoles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NE_Description",
                table: "Network",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NE_Code",
                table: "Network",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hos_HospitalName",
                table: "Hospital",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hos_HospitalCode",
                table: "Hospital",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hos_Branch",
                table: "Hospital",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hos_village",
                table: "Hospital",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Doctor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DO_UserId_FK",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DO_ST_Id_FK",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DO_PostalCode",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DO_DOB",
                table: "Doctor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DO_DI_Id_FK",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DO_Country_Id_FK",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DO_Code",
                table: "Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "district_name",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "district_code",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CD_Code",
                table: "Discipline",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CD_ClinicalDiscipline",
                table: "Discipline",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DGSTC_Name",
                table: "DiagnosticCenters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DGSTC_Code",
                table: "DiagnosticCenters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "designation_desc",
                table: "Designation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "designation_code",
                table: "Designation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "country_name",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country_code",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CON_ConsultedDate",
                table: "Consultation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CON_APPT_Id_FK",
                table: "Consultation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "created_by",
                table: "Complaint",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Assi_code",
                table: "Assistant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Assi_ST_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Assi_Qua_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Assi_PostalCode",
                table: "Assistant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Assi_Hos_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Assi_Des_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Assi_DI_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Assi_Country_Id_FK",
                table: "Assistant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Assi_Spe_id_fk",
                table: "Assistant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Assi_Taluk",
                table: "Assistant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assi_Village",
                table: "Assistant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "DrugMaster",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DrugMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DrugName",
                table: "DrugMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Strength",
                table: "DrugMaster",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "delete_flag",
                table: "DrugMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "deleted_by",
                table: "DrugMaster",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrugMaster",
                table: "DrugMaster",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_PA_APPT_Id_FK",
                table: "Parameters",
                column: "PA_APPT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Assistant_Assi_Spe_id_fk",
                table: "Assistant",
                column: "Assi_Spe_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_DrugMaster_DT_Id_FK",
                table: "DrugMaster",
                column: "DT_Id_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_DType_Id_FK",
                table: "Unit",
                column: "DType_Id_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistant_Specialization_Assi_Spe_id_fk",
                table: "Assistant",
                column: "Assi_Spe_id_fk",
                principalTable: "Specialization",
                principalColumn: "SP_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_ComplaintMst_CPT_MST_Id_FK",
                table: "Complaint",
                column: "CPT_MST_Id_FK",
                principalTable: "ComplaintMst",
                principalColumn: "Cmst_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_PatientAppointment_CPT_APPT_Id_FK",
                table: "Complaint",
                column: "CPT_APPT_Id_FK",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Diseases_Dis_Id_FK",
                table: "Consultation",
                column: "Dis_Id_FK",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasesDtl_Diseases_Dis_Id_FK",
                table: "DiseasesDtl",
                column: "Dis_Id_FK",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasesDtl_PatientAppointment_Ddtl_APPT_Id_FK",
                table: "DiseasesDtl",
                column: "Ddtl_APPT_Id_FK",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorLanguage_Doctor_doc_Id_FK",
                table: "DoctorLanguage",
                column: "doc_Id_FK",
                principalTable: "Doctor",
                principalColumn: "DO_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrugMaster_DrugType_DT_Id_FK",
                table: "DrugMaster",
                column: "DT_Id_FK",
                principalTable: "DrugType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrugMaster_Unit_UT_Id_FK",
                table: "DrugMaster",
                column: "UT_Id_FK",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_PatientAppointment_PA_APPT_Id_FK",
                table: "Parameters",
                column: "PA_APPT_Id_FK",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Prescription_DTL_DrugMaster_DrugMst_Id_FK",
                table: "Patient_Prescription_DTL",
                column: "DrugMst_Id_FK",
                principalTable: "DrugMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_PatientAppointment_SYM_APPT_Id_FK",
                table: "Symptoms",
                column: "SYM_APPT_Id_FK",
                principalTable: "PatientAppointment",
                principalColumn: "Appt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_SymptomsMst_SYM_MST_Id_FK",
                table: "Symptoms",
                column: "SYM_MST_Id_FK",
                principalTable: "SymptomsMst",
                principalColumn: "Smst_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
