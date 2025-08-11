using AutoMapper;
using DocumentManagementAPI.Dtos;
using DocumentManagementAPI.ExceptionHandling;
using DocumentManagementAPI.Repo;

namespace DocumentManagementAPI.Service
{
    public class ProfileService(ProfileRepo profileRepo, IUserRepo userRepo,IMapper mapper)
    {
        public async Task<string> addnewProfile(AddProfileDto profileDto,int userId)
        {
            if (await profileRepo.CheckHasProfileAsync(userId))
            {
                throw new BadRequestException("You have a profile");
            }
            var mapProfile = mapper.Map<DocumentManagement.Data.Models.Profile>(profileDto);
            
            var user =await userRepo.GetUserById(userId);

            mapProfile.Id = userId;

            var numbersAndEmail =await profileRepo.CheckEmailAndPhoneNumbers();

            // Flatten it to a List<string?>
            var allValues = numbersAndEmail.SelectMany(x => x).ToList();

            if (allValues.Contains(mapProfile.Email))
            {
                throw new Exception("The Email is already in use.");
            }
             if (allValues.Contains(mapProfile.PhoneNumber))
            {
                throw new Exception("The PhoneNumber is already in use.");
            }

            user.profile = mapProfile;
            await userRepo.SaveChangeUser();
            return "Add Profile Successfully";
        }

        public async Task<ProfileDto?> GetProfile(int userId)
        {
            return mapper.Map<ProfileDto>(await profileRepo.GetProfileByUserd(userId));
        }


        public async Task<string> UpdateProfile(UpdateProfileDto updateProfile,int userId)
        {
            var profile =await profileRepo.GetProfileByUserd(userId);

            var check=await profileRepo.CheckEmailAndPhoneNumbers();

            var allValues = check.SelectMany(x => x).ToList();

            if (allValues.Contains(updateProfile.Email) && profile.Email != updateProfile.Email)
            {
                throw new Exception("The Email is already in use.");

            }

            if (allValues.Contains(updateProfile.PhoneNumber) && profile.PhoneNumber != updateProfile.PhoneNumber)
            {
                throw new Exception("The PhoneNumber is already in use.");

            }

            //Map values from DTO into the existing profile
            mapper.Map(updateProfile, profile);
            await profileRepo.SaveProfilechange(profile);

            return "Profile updated successfully.";
        }


    }
}
