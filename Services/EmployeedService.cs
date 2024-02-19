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
        Employeed? PostAccessSystem(Employeed employeed);
        bool PostRegisterEmployeed(Employeed employeed);
        IEnumerable<Employeed> GetAllEmployeedPendingAproval();
        bool PutAppproveContractEmployeed(Employeed employeed);
        Employeed? GetByUserNameEmployeed(string userName);
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
            var employees = context.Repositories.EmployeedRepository.GetAllEmployeed();

            return employees.Select(employeed =>
            {
                employeed.Person!.ProfilePicture = context.Repositories.CommonRepository.GetUrlImageFromS3(
                    profilePicture: employeed.Person.ProfilePicture ?? "",
                    employeed.FileName ?? "",
                    employeed.BucketName ?? ""
                );
                return employeed;
            }).ToList();
        }

        public IEnumerable<Employeed> GetAllEmployeedPendingAproval()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.EmployeedRepository.GetAllEmployeedPendingAproval();
        }

        public Employeed? GetByUserNameEmployeed(string userName)
        {
            try
            {
                using var context = _unitOfWork.Create();
                var employeed = context.Repositories.EmployeedRepository.GetByUserNameEmployeed(userName);

                if (employeed != null)
                {
                    employeed = GetUrlImageProfile(context, employeed);
                }

                return employeed;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static Employeed? GetUrlImageProfile(IUnitOfWorkAdapter context, Employeed? employeed)
        {
            string profilePicture = employeed?.Person?.ProfilePicture ?? "";
            string fileName = employeed?.FileName ?? "";
            string bucketName = employeed?.BucketName ?? "";

            if (employeed != null)
            {
                employeed.Person!.ProfilePicture = context.Repositories.CommonRepository.GetUrlImageFromS3(profilePicture, fileName, bucketName);
            }
            return employeed;
        }
        public IEnumerable<EmployeedDisponibilty> GetSchedulesDisponibility(DateTime dateToConsult, int employeedId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.EmployeedRepository.GetSchedulesDisponibility(dateToConsult, employeedId);
        }
        public Employeed? PostAccessSystem(Employeed employeed)
        {
            using var context = _unitOfWork.Create();
            Employeed? createdEmployee = context.Repositories.EmployeedRepository.PostAccessSystem(employeed) ?? throw new InvalidOperationException("No se pudo crear el empleado");
            if (createdEmployee != null)
            {
                createdEmployee = GetUrlImageProfile(context, createdEmployee);
                return createdEmployee;
            }
            throw new InvalidOperationException("No hay información de la persona asociada al empleado");
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

            try
            {
                string nameProfile = context.Repositories.CommonRepository.EncryptFileNameYId(fileData.FileName, id) + Path.GetExtension(fileData.FileName);
                bool update = await context.Repositories.CommonRepository.UploadFileS3Async(fileData, "bucket-users-photos", nameProfile);

                if (update)
                {
                    bool updateProfile = context.Repositories.EmployeedRepository.PutUpdateProfile(nameProfile, id);
                    if (updateProfile)
                    {
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
      
    }
}
