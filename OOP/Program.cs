namespace OOP
{
    using System;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            Service service = new Service();
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Просмотреть все записи");
                Console.WriteLine("2 - Просмотреть одну запись");
                Console.WriteLine("3 - Создать новую запись");
                Console.WriteLine("4 - Удалить запись");
                Console.WriteLine("5 - Загрузить записи в выбранном диапазоне дат");
                Console.WriteLine("6 - Выход");
                
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        service.DisplayAllWorkers();
                        break;
                    case "2":
                        service.DisplayOneWorker();
                        break;
                    case "3":
                        service.CreateNewWorker();
                        break;
                    case "4":
                        service.DeleteWorker();
                        break;
                    case "5":
                        service.LoadWorkersInDateRange();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

    }

}