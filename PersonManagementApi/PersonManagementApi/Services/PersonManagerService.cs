using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PersonManagementApi.Models;

namespace PersonManagementApi.Services
{
    public class PersonManagerService: IPersonManagerService
    {
        private static object _syncLock = new object();
        private readonly AppSettingsOption _appsettings;

        public PersonManagerService(IOptions<AppSettingsOption> appsettings)
        {
            _appsettings = appsettings.Value;
        }

        public async Task<List<PersonModel>> GetPersons()
        {
            var data = File.ReadAllText(_appsettings.DataSource);
            return await Task.FromResult(JsonConvert.DeserializeObject<List<PersonModel>>(data));
        }

        public async Task SavePersons(List<PersonModel> personsList)
        {
            //We are using file based data source. To prevent concurrency issues(if any), using lock
           lock(_syncLock)
            {
                File.WriteAllText(_appsettings.DataSource,JsonConvert.SerializeObject(personsList));
            }
            await Task.Delay(1); //To mimic async operation, also we cannot use await in lock statement. Otherwise need to use WriteAllTextAsync
        }
    }
}
