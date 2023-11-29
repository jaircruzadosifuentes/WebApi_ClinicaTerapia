using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IEmployeedService
    {
        IEnumerable<Employeed> GetAllEmployeed();
        IEnumerable<EmployeedDisponibilty> GetSchedulesDisponibility(DateTime dateToConsult, int employeedId);
        Employeed PostAccessSystem(Employeed employeed);
        bool PostRegisterEmployeed(Employeed employeed);
        IEnumerable<Employeed> GetAllEmployeedPendingAproval();
        bool PutAppproveContractEmployeed(Employeed employeed);
        Employeed GetByUserNameEmployeed(string userName);
        Task<bool> UpdateProfilePicture(IFormFile fileData, int id);
    }
    public class EmployeedService : IEmployeedService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Employeed> GetAllEmployeed()
        {
            using var context = _unitOfWork.Create();
            var employeeds = context.Repositories.EmployeedRepository.GetAllEmployeed();
            foreach (var item in employeeds)
            {
                item.Person.ProfilePicture = context.Repositories.CommonRepository.GetUrlImageFromS3(profilePicture: item.Person.ProfilePicture ?? "", "perfil", "bucket-users-photos");
            }
            return employeeds;
        }

        public IEnumerable<Employeed> GetAllEmployeedPendingAproval()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.EmployeedRepository.GetAllEmployeedPendingAproval();
        }

        public Employeed GetByUserNameEmployeed(string userName)
        {
            using var context = _unitOfWork.Create();
            var employeedReturn = context.Repositories.EmployeedRepository.GetByUserNameEmployeed(userName);
            employeedReturn.Person.ProfilePicture = context.Repositories.CommonRepository.GetUrlImageFromS3(profilePicture: employeedReturn.Person.ProfilePicture ?? "", "perfil", "bucket-users-photos");
            return employeedReturn;
        }

        public IEnumerable<EmployeedDisponibilty> GetSchedulesDisponibility(DateTime dateToConsult, int employeedId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.EmployeedRepository.GetSchedulesDisponibility(dateToConsult, employeedId);
        }
        public Employeed PostAccessSystem(Employeed employeed)
        {
            using var context = _unitOfWork.Create();
            Employeed? employeedReturn = context.Repositories.EmployeedRepository.PostAccessSystem(employeed) ?? throw new InvalidOperationException("No se pudo crear el empleado");
            if (employeedReturn.Person == null)
            {
                throw new InvalidOperationException("No hay información de la persona asociada al empleado");
            }
            else
            {
                employeedReturn.Person.ProfilePicture = context.Repositories.CommonRepository.GetUrlImageFromS3(profilePicture: employeedReturn.Person.ProfilePicture??"", "perfil", "bucket-users-photos");
            }

            return employeedReturn;
        }
        public bool PostRegisterEmployeed(Employeed employeed)
        {
            using var context = _unitOfWork.Create();
            bool insert = context.Repositories.EmployeedRepository.PostRegisterEmployeed(employeed);
            context.SaveChanges();
            return insert;
        }

        public bool PutAppproveContractEmployeed(Employeed employeed)
        {
            using var context = _unitOfWork.Create();
            bool update = context.Repositories.EmployeedRepository.PutAppproveContractEmployeed(employeed);
            context.SaveChanges();
            return update;
        }

        public async Task<bool> UpdateProfilePicture(IFormFile fileData, int id)
        {
            using var context = _unitOfWork.Create();
            string nameProfile = EncryptFileName(fileData.FileName, id) + Path.GetExtension(fileData.FileName);
            bool update = await context.Repositories.CommonRepository.UploadFileS3Async(fileData, "bucket-users-photos", nameProfile);
            //Actualizamos la imagen del trabajador
            bool updateProfile = context.Repositories.EmployeedRepository.PutUpdateProfile(nameProfile, id);
            context.SaveChanges();
            return updateProfile;
        }
        public static string EncryptFileName(string originalFileName, int id)
        {
            string combinedString = originalFileName + id.ToString(); 
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
