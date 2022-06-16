using System;
using System.IO;

namespace ConsoleApp861
{
    class Program
    {
     
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var dirName = new DirectoryInfo(@"C:\Work\skillFactori\123"); // Прописываем путь к директории 

            if (dirName.Exists)
            {
                bool res = MyWorkWithFiles.DeleteDirByTime(dirName);
                // если res = true , тогда папка пустая (изначальная , которую задавали), т.е. если нужно, то можно прописать удаление...  
            }
            else
            {
                Console.WriteLine("Некорректно указан путь");
            }
            Console.ReadKey();
        } 
    }
}
