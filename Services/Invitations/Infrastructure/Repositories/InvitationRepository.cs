using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly InvitationDataContext _context;

        public InvitationRepository(InvitationDataContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Invitation invitation)
        {
            await _context.Invitations.AddAsync(invitation);
            await _context.SaveChangesAsync();
            return invitation.Id;
        }

        public async Task<IEnumerable<Invitation>> GetAll()
        {
            return await _context.Invitations.ToListAsync();
        }

        public async Task<Invitation> GetById(Guid id)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            ArgumentNullException.ThrowIfNull(invitation);
            return invitation;
        }
    }
}
