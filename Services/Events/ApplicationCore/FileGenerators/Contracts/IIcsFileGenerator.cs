using ApplicationCore.Models.Entities;

namespace ApplicationCore.FileGenerators.Contracts
{
    public interface IIcsFileGenerator
    {
        string GenerateFromSingleEvent(UserEvent userEvent);
        string GenerateFromManyEvents(IEnumerable<UserEvent> userEvents);
    }
}
