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

        public async Task<RecurrencyRule> GetById(Guid recurrencyRuleId)
        {
            var toUpdate = await _context.RecurrencyRules.FindAsync(recurrencyRuleId);
            ArgumentNullException.ThrowIfNull(toUpdate);
            return toUpdate;
        }

        public async Task Update(RecurrencyRule recurrencyRule)
        {
            var toUpdate = await _context.RecurrencyRules.FindAsync(recurrencyRule.Id);
            ArgumentNullException.ThrowIfNull(toUpdate);

            toUpdate.Recurrency = recurrencyRule.Recurrency;
            toUpdate.WeekOfMonth = recurrencyRule.WeekOfMonth;
            toUpdate.DayOfWeek = recurrencyRule.DayOfWeek;

            await _context.SaveChangesAsync();
        }
    }
}
