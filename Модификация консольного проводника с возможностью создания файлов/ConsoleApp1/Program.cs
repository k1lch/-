using System;
using System.IO;
using System.Linq;

class DiskManager
{
    private static string currentDirectory;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        currentDirectory = Directory.GetCurrentDirectory();

        while (true)
        {
            Console.Clear();
            DisplayHeader();
            DisplayMainMenu();

            var choice = Console.ReadKey().Key;
            Console.WriteLine();

            switch (choice)
            {
                case ConsoleKey.D1:
                    ListAllDrives();
                    break;
                case ConsoleKey.D2:
                    NavigateFileSystem();
                    break;
                case ConsoleKey.D3:
                    CreateNewItem();
                    break;
                case ConsoleKey.D4:
                    DeleteItem();
                    break;
                case ConsoleKey.D5:
                    CopyOrMoveItem();
                    break;
                case ConsoleKey.D6:
                    SearchFiles();
                    break;
                case ConsoleKey.D7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    WaitForKey();
                    break;
            }
        }
    }

    static void DisplayHeader()
    {
        Console.WriteLine("=== Управление дисками и файловой системой ===");
        Console.WriteLine($"Текущее расположение: {currentDirectory}\n");
    }

    static void DisplayMainMenu()
    {
        Console.WriteLine("1. Просмотреть все диски");
        Console.WriteLine("2. Навигация по файловой системе");
        Console.WriteLine("3. Создать файл/папку");
        Console.WriteLine("4. Удалить файл/папку");
        Console.WriteLine("5. Копировать/переместить файл/папку");
        Console.WriteLine("6. Поиск файлов");
        Console.WriteLine("7. Выход");
        Console.Write("Выберите действие: ");
    }

    static void ListAllDrives()
    {
        Console.Clear();
        Console.WriteLine("=== Доступные диски ===");

        try
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                Console.WriteLine("\n--------------------------------");
                Console.WriteLine($"Диск: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");

                if (drive.IsReady)
                {
                    Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                    Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                    Console.WriteLine($"Общий размер: {BytesToGB(drive.TotalSize)} GB");
                    Console.WriteLine($"Свободно: {BytesToGB(drive.TotalFreeSpace)} GB");
                    Console.WriteLine($"Доступно: {BytesToGB(drive.AvailableFreeSpace)} GB");
                }
                else
                {
                    Console.WriteLine("Диск не готов");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении информации о дисках: {ex.Message}");
        }

        WaitForKey();
    }

    static double BytesToGB(long bytes)
    {
        return Math.Round(bytes / 1024.0 / 1024 / 1024, 2);
    }

    static void NavigateFileSystem()
    {
        while (true)
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("=== Навигация по файловой системе ===");
            Console.WriteLine($"Текущая папка: {currentDirectory}\n");

            try
            {
                // Показать родительскую директорию
                var parentDir = Directory.GetParent(currentDirectory);
                if (parentDir != null)
                {
                    Console.WriteLine("[0] На уровень выше");
                }

                // Показать поддиректории
                var dirs = Directory.GetDirectories(currentDirectory);
                for (int i = 0; i < dirs.Length; i++)
                {
                    var dirInfo = new DirectoryInfo(dirs[i]);
                    Console.WriteLine($"[{i + 1}] {dirInfo.Name} [Папка]");
                }

                // Показать файлы
                var files = Directory.GetFiles(currentDirectory);
                for (int i = 0; i < files.Length; i++)
                {
                    var fileInfo = new FileInfo(files[i]);
                    Console.WriteLine($"[{i + dirs.Length + 1}] {fileInfo.Name} ({BytesToKB(fileInfo.Length)} KB)");
                }

                Console.WriteLine("\n[X] Вернуться в главное меню");
                Console.Write("Выберите элемент для навигации или действия: ");

                var input = Console.ReadLine();

                if (input.ToUpper() == "X")
                {
                    return;
                }

                if (input == "0" && parentDir != null)
                {
                    currentDirectory = parentDir.FullName;
                    continue;
                }

                if (int.TryParse(input, out int selection))
                {
                    selection--; // так как мы начинаем с 1

                    if (selection >= 0 && selection < dirs.Length)
                    {
                        currentDirectory = dirs[selection];
                    }
                    else if (selection >= dirs.Length && selection < dirs.Length + files.Length)
                    {
                        int fileIndex = selection - dirs.Length;
                        OpenFile(files[fileIndex]);
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор.");
                        WaitForKey();
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод.");
                    WaitForKey();
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Ошибка: Нет доступа к этой папке.");
                WaitForKey();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                WaitForKey();
                return;
            }
        }
    }

    static double BytesToKB(long bytes)
    {
        return Math.Round(bytes / 1024.0, 2);
    }

    static void OpenFile(string filePath)
    {
        try
        {
            Console.Clear();
            Console.WriteLine($"Содержимое файла: {Path.GetFileName(filePath)}\n");

            string extension = Path.GetExtension(filePath).ToLower();

            if (extension == ".txt" || extension == ".log" || extension == ".csv" || extension == ".json")
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Этот тип файла не может быть отображен как текст.");
                Console.WriteLine($"Размер файла: {BytesToKB(new FileInfo(filePath).Length)} KB");
                Console.WriteLine($"Последнее изменение: {File.GetLastWriteTime(filePath)}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
        }

        WaitForKey();
    }

    static void CreateNewItem()
    {
        Console.Clear();
        DisplayHeader();
        Console.WriteLine("=== Создание нового элемента ===");
        Console.WriteLine("1. Создать папку");
        Console.WriteLine("2. Создать файл");
        Console.WriteLine("3. Отмена");
        Console.Write("Выберите действие: ");

        var choice = Console.ReadKey().Key;
        Console.WriteLine();

        switch (choice)
        {
            case ConsoleKey.D1:
                CreateDirectory();
                break;
            case ConsoleKey.D2:
                CreateFile();
                break;
            case ConsoleKey.D3:
                return;
            default:
                Console.WriteLine("Неверный выбор.");
                WaitForKey();
                break;
        }
    }

    static void CreateDirectory()
    {
        Console.Write("Введите имя новой папки: ");
        string dirName = Console.ReadLine();
        string dirPath = Path.Combine(currentDirectory, dirName);

        try
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                Console.WriteLine($"Папка '{dirName}' успешно создана.");
            }
            else
            {
                Console.WriteLine("Папка с таким именем уже существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании папки: {ex.Message}");
        }

        WaitForKey();
    }

    static void CreateFile()
    {
        Console.Write("Введите имя нового файла: ");
        string fileName = Console.ReadLine();
        string filePath = Path.Combine(currentDirectory, fileName);

        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Введите содержимое файла (нажмите Enter для завершения):");
                string content = Console.ReadLine();
                File.WriteAllText(filePath, content);
                Console.WriteLine($"Файл '{fileName}' успешно создан.");
            }
            else
            {
                Console.WriteLine("Файл с таким именем уже существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
        }

        WaitForKey();
    }

    static void DeleteItem()
    {
        Console.Clear();
        DisplayHeader();
        Console.WriteLine("=== Удаление элемента ===");
        Console.WriteLine("1. Удалить файл");
        Console.WriteLine("2. Удалить папку");
        Console.WriteLine("3. Отмена");
        Console.Write("Выберите действие: ");

        var choice = Console.ReadKey().Key;
        Console.WriteLine();

        switch (choice)
        {
            case ConsoleKey.D1:
                DeleteFile();
                break;
            case ConsoleKey.D2:
                DeleteDirectory();
                break;
            case ConsoleKey.D3:
                return;
            default:
                Console.WriteLine("Неверный выбор.");
                WaitForKey();
                break;
        }
    }

    static void DeleteFile()
    {
        Console.Write("Введите имя файла для удаления: ");
        string fileName = Console.ReadLine();
        string filePath = Path.Combine(currentDirectory, fileName);

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Файл '{fileName}' успешно удален.");
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
        }

        WaitForKey();
    }

    static void DeleteDirectory()
    {
        Console.Write("Введите имя папки для удаления: ");
        string dirName = Console.ReadLine();
        string dirPath = Path.Combine(currentDirectory, dirName);

        try
        {
            if (Directory.Exists(dirPath))
            {
                Console.Write("Удалить папку со всем содержимым? (Y/N): ");
                var confirm = Console.ReadKey().Key;
                Console.WriteLine();

                if (confirm == ConsoleKey.Y)
                {
                    Directory.Delete(dirPath, true);
                    Console.WriteLine($"Папка '{dirName}' и все её содержимое успешно удалены.");
                }
                else
                {
                    Console.WriteLine("Удаление отменено.");
                }
            }
            else
            {
                Console.WriteLine("Папка не найдена.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении папки: {ex.Message}");
        }

        WaitForKey();
    }

    static void CopyOrMoveItem()
    {
        Console.Clear();
        DisplayHeader();
        Console.WriteLine("=== Копирование/перемещение элемента ===");
        Console.WriteLine("1. Копировать файл/папку");
        Console.WriteLine("2. Переместить файл/папку");
        Console.WriteLine("3. Отмена");
        Console.Write("Выберите действие: ");

        var choice = Console.ReadKey().Key;
        Console.WriteLine();

        switch (choice)
        {
            case ConsoleKey.D1:
                CopyItem();
                break;
            case ConsoleKey.D2:
                MoveItem();
                break;
            case ConsoleKey.D3:
                return;
            default:
                Console.WriteLine("Неверный выбор.");
                WaitForKey();
                break;
        }
    }

    static void CopyItem()
    {
        Console.Write("Введите имя файла/папки для копирования: ");
        string sourceName = Console.ReadLine();
        string sourcePath = Path.Combine(currentDirectory, sourceName);

        Console.Write("Введите путь назначения: ");
        string destPath = Console.ReadLine();

        try
        {
            if (File.Exists(sourcePath))
            {
                if (!Path.IsPathRooted(destPath))
                {
                    destPath = Path.Combine(currentDirectory, destPath);
                }

                File.Copy(sourcePath, destPath, true);
                Console.WriteLine("Файл успешно скопирован.");
            }
            else if (Directory.Exists(sourcePath))
            {
                if (!Path.IsPathRooted(destPath))
                {
                    destPath = Path.Combine(currentDirectory, destPath);
                }

                CopyDirectory(sourcePath, destPath);
                Console.WriteLine("Папка успешно скопирована.");
            }
            else
            {
                Console.WriteLine("Указанный файл или папка не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при копировании: {ex.Message}");
        }

        WaitForKey();
    }

    static void CopyDirectory(string sourceDir, string destDir)
    {
        Directory.CreateDirectory(destDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string destFile = Path.Combine(destDir, Path.GetFileName(file));
            File.Copy(file, destFile, true);
        }

        foreach (string subDir in Directory.GetDirectories(sourceDir))
        {
            string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
            CopyDirectory(subDir, destSubDir);
        }
    }

    static void MoveItem()
    {
        Console.Write("Введите имя файла/папки для перемещения: ");
        string sourceName = Console.ReadLine();
        string sourcePath = Path.Combine(currentDirectory, sourceName);

        Console.Write("Введите путь назначения: ");
        string destPath = Console.ReadLine();

        try
        {
            if (File.Exists(sourcePath))
            {
                if (!Path.IsPathRooted(destPath))
                {
                    destPath = Path.Combine(currentDirectory, destPath);
                }

                File.Move(sourcePath, destPath);
                Console.WriteLine("Файл успешно перемещен.");
            }
            else if (Directory.Exists(sourcePath))
            {
                if (!Path.IsPathRooted(destPath))
                {
                    destPath = Path.Combine(currentDirectory, destPath);
                }

                Directory.Move(sourcePath, destPath);
                Console.WriteLine("Папка успешно перемещена.");
            }
            else
            {
                Console.WriteLine("Указанный файл или папка не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при перемещении: {ex.Message}");
        }

        WaitForKey();
    }

    static void SearchFiles()
    {
        Console.Clear();
        DisplayHeader();
        Console.WriteLine("=== Поиск файлов ===");
        Console.Write("Введите маску поиска (например, *.txt или имя файла): ");
        string searchPattern = Console.ReadLine();

        Console.Write("Искать в подпапках? (Y/N): ");
        bool recursive = Console.ReadKey().Key == ConsoleKey.Y;
        Console.WriteLine();

        try
        {
            var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var files = Directory.GetFiles(currentDirectory, searchPattern, searchOption);

            Console.WriteLine("\nРезультаты поиска:");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine($"\nНайдено файлов: {files.Length}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при поиске: {ex.Message}");
        }

        WaitForKey();
    }

    static void WaitForKey()
    {
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}