using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementAPI.Repo
{
    public class ProfileRepo(DocumentDbContext dbContext)
    {

        public async Task addNewProfile(DocumentManagement.Data.Models.Profile profile)
        {
             await dbContext.Profiles.AddAsync(profile);
             await dbContext.SaveChangesAsync();
        }

        public async Task<DocumentManagement.Data.Models.Profile?> GetProfileByUserd(int userId)
        {
            return await dbContext.Profiles.FirstOrDefaultAsync(p => p.Id == userId);
        }

        public async Task<List<string?[]>> CheckEmailAndPhoneNumbers()
        {
            return await dbContext.Profiles
                .Select(p => new[] { p.Email, p.PhoneNumber })
                .ToListAsync();
        }


        public async Task<DocumentManagement.Data.Models.Profile?> GetProfileById(int userId)
        {
            return await dbContext.Profiles.FindAsync(userId);
        }

        public async Task SaveProfilechange(DocumentManagement.Data.Models.Profile profile)
        {
            dbContext.Update(profile);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckHasProfileAsync(int userId)
        {
           return await dbContext.Profiles.AnyAsync(p=>p.Id==userId);
        }

       

    }
}
