using PersonManagementApi.Models;

namespace PersonManagementApi.Services
{
    public interface IPersonManagerService
    {
        Task SavePersons(List<PersonModel> personsList);
        Task<List<PersonModel>> GetPersons();
    }
}
