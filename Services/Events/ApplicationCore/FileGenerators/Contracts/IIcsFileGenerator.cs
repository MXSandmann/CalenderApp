using ApplicationCore.Models.Entities;

namespace ApplicationCore.FileGenerators.Contracts
{
    public interface IIcsFileGenerator
    {
        string Generate(UserEvent userEvent);
    }
}
