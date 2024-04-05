using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lab04Shvachka.Models;

namespace Lab04Shvachka.Repositories
{
    public class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShvachkaLab");

        public FileRepository() 
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public async Task AddOrUpdateAsync(BindingList<Person> obj)
        {
            var stringObj = JsonSerializer.Serialize(obj);
            using (StreamWriter sw  = new StreamWriter(Path.Combine(BaseFolder, "Users"), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public async Task<BindingList<Person>> GetAsync()
        {
            string stringObj = null;

            if(!File.Exists(Path.Combine(BaseFolder, "Users")))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(Path.Combine(BaseFolder, "Users")))
            {
                stringObj = await sr.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<BindingList<Person>>(stringObj);
        }
    }
}
