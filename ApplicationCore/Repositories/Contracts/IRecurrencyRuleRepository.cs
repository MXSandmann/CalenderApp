using ApplicationCore.Models.Entities;

namespace ApplicationCore.Repositories.Contracts
{
    public interface IRecurrencyRuleRepository
    {
        Task<Guid> Add(RecurrencyRule recurrencyRule);
        Task<RecurrencyRule> GetById(Guid recurrencyRuleId);
        Task Update(RecurrencyRule recurrencyRule);
    }
}
