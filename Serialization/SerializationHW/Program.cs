using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BookSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
           
            DateTime dateBook1 = new DateTime(1888, 11, 12);
            DateTime dateBook2 = new DateTime(1914, 10, 10);
            DateTime dateBook3 = new DateTime(2025, 2, 03);
            

            Book book = new Book
            {
                Name = "О чем думает Пушкин?",
                Price = 4500,
                Author = "А.Пушкин",
                Year = dateBook1
            };
            Book book2 = new Book
            {
                Name = "Ержан, вставай!",
                Price = 15000,
                Author = "М.Нурмаш",
                Year = dateBook2
            };
            Book book3 = new Book
            {
                Name = "Белый клык",
                Price = 2000,
                Author = "Д.Шелк",
                Year = dateBook3
            };

            List<Book> books = new List<Book>();
            books.Add(book);
            books.Add(book2);
            books.Add(book3);

            
            DriveInfo[] drives = DriveInfo.GetDrives();


            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{i}.{drives[i].Name}");
            }

            Console.WriteLine("Введите номер диска, на который будет записан файл");

            string driveNumberAsString = Console.ReadLine();

            int driveNumber = 0;
            if (!int.TryParse(driveNumberAsString, out driveNumber))
            {
                Console.WriteLine("Ошибка ввода, будет произведена запись на первый указанный диск.");
            }
            string driveName = drives[driveNumber].Name;

            BinaryFormatter formatter = new BinaryFormatter();
            if (!Directory.Exists(driveName + @"/data"))
            {
                Directory.CreateDirectory(driveName + @"/data");
            }
            using (FileStream stream = File.Create(driveName + @"/data/book.bin"))
            {
                formatter.Serialize(stream, books);
            }

            using (FileStream stream = File.OpenRead(driveName + @"/data/book.bin"))
            {
                var resultPerson = formatter.Deserialize(stream) as List<Book>;
                int count = 1;
                foreach (var element in resultPerson)
                {
                    Console.WriteLine(count + ". Название книги: " + element.Name);
                    Console.WriteLine("   Цена книги: " + element.Price);
                    Console.WriteLine("   Автор книги: " + element.Author);
                    Console.WriteLine("   Год книги: " + element.Year.Year + "\n");
                    count++;
                }

            }

            Console.ReadLine();
        }
    }
}