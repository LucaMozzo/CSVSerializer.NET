using CsvSerializer;
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
                new User { Username = "super user", Email = "superuser@example.com", Age = 54, DateOfBirth = DateTime.UtcNow.AddYears(-50) },
                new User { Username = "user", Email = "user@example.com", Age = 20, DateOfBirth = DateTime.UtcNow.AddYears(-20) }
            };

            // without object mapper
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer serializer = new Serializer();
                await serializer.SerializeObjects(users, memoryStream).ConfigureAwait(true);

                Console.WriteLine(Encoding.UTF8.GetString(memoryStream.ToArray()));
                Console.ReadLine();
            }

            // with object mapper
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var mapper = new ObjectMapper<User>();
                mapper.AddMap(u => u.Username, 1, "Name")
                    .AddMap(u => u.Age, 0)
                    .AddMap(u => u.Email)
                    .AddMap(u => u.DateOfBirth, header: "Date of birth", transformFunction: dob => dob.ToString("yyyy-mm-dd"));

                Serializer serializer = new Serializer();
                await serializer.SerializeObjects(users, memoryStream, mapper).ConfigureAwait(true);

                Console.WriteLine(Encoding.UTF8.GetString(memoryStream.ToArray()));
                Console.ReadLine();
            }
        }
    }
}
