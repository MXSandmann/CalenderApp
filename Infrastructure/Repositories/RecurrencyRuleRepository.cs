using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories.Contracts;

namespace Infrastructure.Repositories
{
    public class RecurrencyRuleRepository : IRecurrencyRuleRepository
    {
        public Task<Guid> Add(RecurrencyRule recurrencyRule)
        {
            throw new NotImplementedException();
        }

        public Task<RecurrencyRule> GetById(Guid recurrencyRuleId)
        {
            throw new NotImplementedException();
        }
    }
}
