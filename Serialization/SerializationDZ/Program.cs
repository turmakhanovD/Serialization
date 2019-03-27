using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationDZ
{
    class Program
    {
        static void Main(string[] args)
        {

            User user = new User
            {
                Login = "Islam",
                Password = "2001002isl"
            };
            string driveName = "";
            DriveInfo[] drives = DriveInfo.GetDrives();

            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{i}.{drives[i].Name}");
            }

            Console.WriteLine("Введите номер диска, на который будет записан файл");

            string driveNumberAsString = Console.ReadLine();
            string login, password;

            int driveNumber = 0;
            if (!int.TryParse(driveNumberAsString, out driveNumber))
            {
                Console.WriteLine("Ошибка ввода, будет произведена запись на первый указанный диск.");
            }
            driveName = drives[driveNumber].Name;

            Console.WriteLine("Введите логин:");
            login = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            password = Console.ReadLine();

            BinaryFormatter formatter = new BinaryFormatter();
            if (!Directory.Exists(driveName + @"/data"))
            {
                Directory.CreateDirectory(driveName + @"/data");
            }
            using (FileStream stream = File.Create(driveName + @"/data/Users.bin"))
            {
                formatter.Serialize(stream, user);
            }

            using (FileStream stream = File.OpenRead(driveName + @"/data/Users.bin"))
            {
                var resultPerson = formatter.Deserialize(stream) as User;
                if (resultPerson.Login == login && resultPerson.Password == password)
                {
                    Console.WriteLine("Вы зашли в систему");
                }
                else
                {
                    Console.WriteLine("Не коректный ввод");
                }
            }


            Console.ReadLine();
        }
    }
}