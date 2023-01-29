using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories.Contracts;
using Infrastructure.DataContext;

namespace Infrastructure.Repositories
{
    public class RecurrencyRuleRepository : IRecurrencyRuleRepository
    {
        private readonly UserEventDataContext _context;
        public RecurrencyRuleRepository(UserEventDataContext context)
        {
            _context = context;
        }
        public async Task<Guid> Add(RecurrencyRule recurrencyRule)
        {
            await _context.AddAsync(recurrencyRule);
            await _context.SaveChangesAsync();
            return recurrencyRule.Id;
        }

        public Task<RecurrencyRule> GetById(Guid recurrencyRuleId)
        {
            throw new NotImplementedException();
        }
    }
}
