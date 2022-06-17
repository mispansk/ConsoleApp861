using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp861
{
    static public class MyWorkWithFiles
    {
        /// <summary>
        /// Возвращает истина, когда  можно удалить папку, т.к. сама она не изменилась в наш интервал времени, и вложенные в нее файлы и папки на любом 
        /// уровне ниже тоже не изменялись и были удалены
        /// При вызове метода пробегая всю иерархию папок внизу- удаляем папки и файлы подходящие под условия удаления
        /// </summary>
        /// <param name="dir"> текущая папка, изначально та, которую задали  </param>
        /// <returns></returns>
        static public Boolean DeleteDirByTime(DirectoryInfo dir)
        {
            bool HasNewFile = false; // есть ли не удаляемые файлы в папке dir (только на этом уровне) по правилу
            bool HasNewDir = false; // есть ли не удаляемые папки в папке dir по правилу (только на этом уровне)
            bool result;
            try
            {

                foreach (FileInfo file in dir.GetFiles())
                {
                    if (DateTime.Now - file.LastAccessTime > Constants.IntervalMinutes)
                    {
                        Console.WriteLine("Удаление файла {0}", file);
                        file.Delete();
                    }
                    else
                    {
                        Console.WriteLine("Файл {0} не удаляет, т.к. он актуален", file);
                        HasNewFile = true;
                    }
                }

                foreach (DirectoryInfo curentDir in dir.GetDirectories())
                {
                    bool res = DeleteDirByTime(curentDir);

                    if (res)
                    {
                        Console.WriteLine("Удаляем папку {0}", curentDir);
                        curentDir.Delete(true);
                    }
                    else
                    {
                        Console.WriteLine("Папку {0} не удаляем, т.к. она актуальна", curentDir);
                    }
                    HasNewDir = ((HasNewDir) || (res));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e}");
            }
            result = (DateTime.Now - dir.LastAccessTime > Constants.IntervalMinutes) & (!HasNewFile) & (!HasNewDir);
            return result;
            }           
        }
    }

