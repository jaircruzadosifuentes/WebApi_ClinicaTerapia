using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer
{
    public static class StoreProcedure
    {
        //Person
        public static string G_STORE_GETALL_PERSON = "stp_getAll_person";
        public static string G_STORE_GETBYID_EMAIL_FOR_PERSON = "stp_getById_Email_For_PersonId";
        public static string G_STORE_GETBYID_DOCUMENT_FOR_PERSON = "stp_getById_Document_For_PersonId";
        public static string G_STORE_GETBYID_CELLPHONE_FOR_PERSON = "stp_getById_CellPhone_For_PersonId";
        //Patient
        public static string G_STORE_GETALL_PATIENT_IN_QUEUE = "stp_getPatientsInQueue";
        public static string G_STORE_GETALL_PATIENT_IN_ATTENTION = "stp_getPatientsInAtention";
        public static string G_STORE_VALIDA_SCHEDULE_OPEN = "stp_validaHorariosDisponibles";
        public static string G_STORE_GETALL_PATIENT_IN_PEND_APRO = "stp_getPatientsInQueueGeneral";
        public static string G_STORE_UPDATE_APPROVE_PATIENT_NEW = "stp_attention_patient_new";
        public static string G_STORE_UPDATE_APPROVE_PATIENT = "stp_approveToAttentionAndRemoveQueue";
        public static string G_STORE_GET_ALL_PATIENT_NEW_ATTENTION_BY_EMPLOYEED_ID = "stp_get_all_patients_first_attention_by_employeed_id";
        public static string G_STORE_GET_ALL_PATIENT_CLINICAL_CARE_ENDS = "stp_get_all_patients_clinical_care_ends";
        //Error
        public static string G_STORE_POST_INSERT_ERROR = "stp_insert_error";
        //Document
        public static string G_STORE_GETALL_DOCUMENT= "stp_get_all_document";
        //Employeed
        public static string G_STORE_GETALL_EMPLOYEED = "stp_get_all_employeed";
        public static string G_STORE_GETDISPONIBILITY_EMPLOYEED = "stp_getDisponibilidadEmpleado";
        //Solicitud
        public static string G_STORE_POST_REGISTER_SOLICITUD_ATTENTION = "stp_post_register_new_solicitude_attention";
        public static string G_STORE_POST_REGISTER_FIRST_CLINICAL_ANALYSIS = "stp_post_first_clinical_analysis";
        //Paquetes
        public static string G_STORE_GETALL_PACKETS = "stp_get_all_packets_or_sessiones";
        //Comunes
        public static string G_STORE_GETALL_PAY_METHODS = "stp_get_all_pay_method";
        //Schedule
        public static string G_STORE_PROCESS_SCHEDULE = "stp_process_schedule";
        public static string G_STORE_GET_ALL_SCHEDULE_PATIENT = "stp_get_all_schedule_patient";
        //Finaliza Solicitud
        public static string G_STORE_POST_REGISTER_FINALIZA_SOLICITUD = "stp_post_register_finaliza_solicitud";
        //Sesiones
        public static string G_STORE_GET_SESSION_FOR_PATIENT_ID = "stp_get_session_for_patient_id";
        public static string G_STORE_PA_APPOINTMENT_GET_PATIENTSOLICITUDE = "PA_APPOINTMENT_GET_PATIENTSOLICITUDE";
        public static string G_STORE_GET_ALL_PATIENT_IN_TREATMENT = "stp_get_all_patients_in_treatment";
        public static string G_STORE_GET_PAY_DUE_DETAIL_FOR_PATIENT_ID = "stp_get_pay_due_detail_for_patient_id";
        public static string G_STORE_GET_COUNT_PATIENTS_TYPE = "stp_get_count_patients_types";
        public static string G_STORE_GET_ALL_PATIENT_PERCENTAJE_TREATMENT = "stp_get_all_patient_percentaje_treatment";
        public static string G_STORE_GET_BY_ID_ADVANCE_CLINIC = "stp_get_by_id_advance_clinic";
        public static string G_STORE_GET_BY_ID_PATIENT_PROGRESS = "stp_getProgressPatientByProgressId";
        public static string G_STORE_POST_REGISTER_PROGRESS_SESION = "stp_postRegisterProgressSesion";
        public static string G_STORE_PUT_HOUR_SESION = "stp_updateHourSesion";

         
    }
}
