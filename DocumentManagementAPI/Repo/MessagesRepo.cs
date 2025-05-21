using DocumentManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementAPI.Repo
{
    public class MessagesRepo(DocumentDbContext dbContext)
    {
        public async Task SaveNewMessage(Message message)
        {
            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Message?> GetMessageById(int messageId)
        {
            return await dbContext.Messages.Include(u=>u.User).FirstOrDefaultAsync(m=> m.Id == messageId);
        }

        public async Task DeleteMessage(Message message)
        {
            dbContext?.Messages.Remove(message);
            await dbContext.SaveChangesAsync();
        }
    }
}
