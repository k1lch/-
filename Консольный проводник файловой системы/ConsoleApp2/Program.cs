using System;
using System.IO;

class FileExplorer
{
    static string currentDirectory = Directory.GetCurrentDirectory();

    static void Main(string[] args)
    {
        Console.WriteLine("Простой файловый проводник");
        Console.WriteLine("Текущая директория: " + currentDirectory);
        
        while (true)
        {
            DisplayMenu();
            var key = Console.ReadKey().Key;
            Console.WriteLine();
            
            switch (key)
            {
                case ConsoleKey.D1:
                    ListDirectoryContents();
                    break;
                case ConsoleKey.D2:
                    NavigateToDirectory();
                    break;
                case ConsoleKey.D3:
                    OpenFile();
                    break;
                case ConsoleKey.D4:
                    CreateDirectory();
                    break;
                case ConsoleKey.D5:
                    CreateFile();
                    break;
                case ConsoleKey.D6:
                    DeleteItem();
                    break;
                case ConsoleKey.D7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\nМеню:");
        Console.WriteLine("1. Просмотреть содержимое текущей директории");
        Console.WriteLine("2. Перейти в директорию");
        Console.WriteLine("3. Открыть файл");
        Console.WriteLine("4. Создать директорию");
        Console.WriteLine("5. Создать файл");
        Console.WriteLine("6. Удалить файл/директорию");
        Console.WriteLine("7. Выход");
        Console.Write("Выберите действие: ");
    }

    static void ListDirectoryContents()
    {
        Console.WriteLine("\nСодержимое директории " + currentDirectory + ":");
        
        try
        {
            // Вывод поддиректорий
            var directories = Directory.GetDirectories(currentDirectory);
            Console.WriteLine("\nДиректории:");
            foreach (var dir in directories)
            {
                var dirInfo = new DirectoryInfo(dir);
                Console.WriteLine($"  [DIR]  {dirInfo.Name} (создана: {dirInfo.CreationTime})");
            }

            // Вывод файлов
            var files = Directory.GetFiles(currentDirectory);
            Console.WriteLine("\nФайлы:");
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                Console.WriteLine($"  [FILE] {fileInfo.Name} (размер: {fileInfo.Length} байт, изменен: {fileInfo.LastWriteTime})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }

    static void NavigateToDirectory()
    {
        Console.Write("\nВведите путь к директории (или .. для перехода на уровень выше): ");
        string path = Console.ReadLine();

        if (path == "..")
        {
            DirectoryInfo parentDir = Directory.GetParent(currentDirectory);
            if (parentDir != null)
            {
                currentDirectory = parentDir.FullName;
            }
            else
            {
                Console.WriteLine("Вы находитесь в корневой директории.");
            }
            return;
        }

        if (!Path.IsPathRooted(path))
        {
            path = Path.Combine(currentDirectory, path);
        }

        try
        {
            if (Directory.Exists(path))
            {
                currentDirectory = Path.GetFullPath(path);
                Console.WriteLine("Текущая директория изменена на: " + currentDirectory);
            }
            else
            {
                Console.WriteLine("Директория не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }

    static void OpenFile()
    {
        Console.Write("\nВведите имя файла для открытия: ");
        string fileName = Console.ReadLine();
        string filePath = Path.Combine(currentDirectory, fileName);

        try
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("\nСодержимое файла " + fileName + ":");
                string content = File.ReadAllText(filePath);
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Файл не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при открытии файла: " + ex.Message);
        }
    }

    static void CreateDirectory()
    {
        Console.Write("\nВведите имя новой директории: ");
        string dirName = Console.ReadLine();
        string dirPath = Path.Combine(currentDirectory, dirName);

        try
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                Console.WriteLine("Директория создана: " + dirPath);
            }
            else
            {
                Console.WriteLine("Директория уже существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при создании директории: " + ex.Message);
        }
    }

    static void CreateFile()
    {
        Console.Write("\nВведите имя нового файла: ");
        string fileName = Console.ReadLine();
        string filePath = Path.Combine(currentDirectory, fileName);

        try
        {
            if (!File.Exists(filePath))
            {
                Console.Write("Введите содержимое файла (нажмите Enter для завершения):\n");
                string content = Console.ReadLine();
                
                File.WriteAllText(filePath, content);
                Console.WriteLine("Файл создан: " + filePath);
            }
            else
            {
                Console.WriteLine("Файл уже существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при создании файла: " + ex.Message);
        }
    }

    static void DeleteItem()
    {
        Console.Write("\nВведите имя файла или директории для удаления: ");
        string itemName = Console.ReadLine();
        string itemPath = Path.Combine(currentDirectory, itemName);

        try
        {
            if (File.Exists(itemPath))
            {
                File.Delete(itemPath);
                Console.WriteLine("Файл удален: " + itemPath);
            }
            else if (Directory.Exists(itemPath))
            {
                Directory.Delete(itemPath, true);
                Console.WriteLine("Директория удалена: " + itemPath);
            }
            else
            {
                Console.WriteLine("Файл или директория не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при удалении: " + ex.Message);
        }
    }
}