using CSVSerializer;
using Demo.CS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CS.Examples
{
    public static class SerializeObject
    {
        public async static Task Serialize()
        {
            // create some data
            List<User> users = new List<User>()
            {
                new User { Username = "super user", Email = "superuser@example.com", Age = 54 },
                new User { Username = "user", Email = "user@example.com", Age = 20 }
            };

            // without object mapper
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer serializer = new Serializer();
                await serializer.SerializeObjects(users, memoryStream).ConfigureAwait(true);

                Console.WriteLine(Encoding.UTF8.GetString(memoryStream.ToArray()));
                Console.ReadLine();
            }
        }
    }
}
