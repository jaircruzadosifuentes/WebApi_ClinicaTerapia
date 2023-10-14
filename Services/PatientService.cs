﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IPatientService
    {
        IEnumerable<PatientValidateSchedule> GetAllPatientsWithSchedule(string hourInitial, string hourFinished, DateTime fechaReserved, int employeedId);
        IEnumerable<Patient> GetAllPatientsPendApro();
        bool ApprovePatientNew(int patientId, string type);
        bool PutApproveSolicitude(int patientId, string type);
        IEnumerable<Patient> GetAllPatientsNewAttentionByEmployeedId(int employeedId);
        IEnumerable<Patient> GetAllPatientsPatientWithAppoiment();
        IEnumerable<Patient> GetAllPatientsInTreatment();
        IEnumerable<Patient> GetAllPatientsFinishedTreatment();
        IEnumerable<Patient> GetAllPatientsInPercentajeTreatment(int patientId);
        IEnumerable<PatientProgress> GetAdvanceCliniciForPatientId(int patientId);
        IEnumerable<PatientProgress> GetByIdPatientProgress(int progressPatientId);
        bool PostRegistrProgressSesion(PatientProgress patientProgress);
        bool PutUpdateHourSesion(PatientProgress patientProgress);
        IEnumerable<Patient> GetAllPatientsInWaiting();
        IEnumerable<Patient> GetAllPatientsInAttention();
    }
    public class PatientService : IPatientService
    {
         private IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool PutApproveSolicitude(int patientId, string type)
        {
            using var context = _unitOfWork.Create();
            bool approve = context.Repositories.PatientRepository.PutApproveSolicitude(patientId, type);
            context.SaveChanges();
            return approve;
        }

        public bool ApprovePatientNew(int patientId, string type)
        {
            using var context = _unitOfWork.Create();
            bool approve = context.Repositories.PatientRepository.ApprovePatientNew(patientId, type);
            context.SaveChanges();
            return approve;
        }

        public IEnumerable<PatientProgress> GetAdvanceCliniciForPatientId(int patientId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.PatientRepository.GetAdvanceCliniciForPatientId(patientId);
        }

        public IEnumerable<Patient> GetAllPatientsInPercentajeTreatment(int patientId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.PatientRepository.GetAllPatientsInPercentajeTreatment(patientId);
        }

        public IEnumerable<Patient> GetAllPatientsInTreatment()
        {
            using var context = _unitOfWork.Create();
            var patients = context.Repositories.PatientRepository.GetAllPatientsInTreatment();
            foreach (var patient in patients)
            {
                patient.PatientProgresses = (List<PatientProgress>)context.Repositories.PatientRepository.GetSessionForPatientId(patient.PatientId);
            }
            return patients;
        }

        public IEnumerable<Patient> GetAllPatientsNewAttentionByEmployeedId(int employeedId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.PatientRepository.GetAllPatientsNewAttentionByEmployeedId(employeedId);
        }

        public IEnumerable<Patient> GetAllPatientsPatientWithAppoiment()
        {
            using var context = _unitOfWork.Create();
            var patients = context.Repositories.PatientRepository.GetAllPatientsPatientWithAppoiment();
            foreach (var patient in patients)
            {
                patient.PatientProgresses = (List<PatientProgress>)context.Repositories.PatientRepository.GetSessionForPatientId(patient.PatientId);
            }
            return patients;
        }

        public IEnumerable<Patient> GetAllPatientsPendApro()
        {
            using var context = _unitOfWork.Create();
            var patients = context.Repositories.PatientRepository.GetAllPatientsPendApro();
            return patients;   
        }

        public IEnumerable<PatientValidateSchedule> GetAllPatientsWithSchedule(string hourInitial, string hourFinished, DateTime fechaReserved, int employeedId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.PatientRepository.GetAllPatientsWithSchedule(hourInitial, hourFinished, fechaReserved, employeedId);
        }

        public IEnumerable<PatientProgress> GetByIdPatientProgress(int progressPatientId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.PatientRepository.GetByIdPatientProgress(progressPatientId);
        }

        public bool PostRegistrProgressSesion(PatientProgress patientProgress)
        {
            using var context = _unitOfWork.Create();
            bool register = context.Repositories.PatientRepository.PostRegistrProgressSesion(patientProgress);
            context.SaveChanges();
            return register;
        }

        public bool PutUpdateHourSesion(PatientProgress patientProgress)
        {
            using var context = _unitOfWork.Create();
            bool update = context.Repositories.PatientRepository.PutUpdateHourSesion(patientProgress);
            context.SaveChanges();
            return update;
        }

        public IEnumerable<Patient> GetAllPatientsFinishedTreatment()
        {
            using var context = _unitOfWork.Create();
            var patients = context.Repositories.PatientRepository.GetAllPatientsFinishedTreatment();
            foreach (var patient in patients)
            {
                patient.PatientProgresses = (List<PatientProgress>)context.Repositories.PatientRepository.GetSessionForPatientId(patient.PatientId);
            }
            return patients;
        }

        public IEnumerable<Patient> GetAllPatientsInWaiting()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.PatientRepository.GetAllPatientsInWaiting();
        }

        public IEnumerable<Patient> GetAllPatientsInAttention()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.PatientRepository.GetAllPatientsInAttention();
        }
    }
}
