using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
     class Service
    {
        Repository repository = new Repository();

        public void DisplayAllWorkers()
        {
            Worker[] workers = repository.GetAllWorkers();
            DisplayWorkers(workers);
        }

        public void DisplayOneWorker()
        {
            Console.Write("Введите ID записи: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Worker worker = repository.GetWorkerById(id);

                if (worker.Id != 0)
                {
                    DisplayWorkers(new[] { worker });
                }
                else
                {
                    Console.WriteLine("Запись не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ID.");
            }
        }

        public void CreateNewWorker()
        {
            Console.Write("Введите Ф.И.О. сотрудника: ");
            string fio = Console.ReadLine();

            Console.Write("Введите возраст сотрудника: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                Console.Write("Введите рост сотрудника: ");
                if (int.TryParse(Console.ReadLine(), out int height))
                {
                    Console.Write("Введите дату рождения сотрудника (дд.мм.гггг): ");
                    if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
                    {
                        Console.Write("Введите место рождения сотрудника: ");
                        string birthPlace = Console.ReadLine();

                        Worker newWorker = new Worker
                        {
                            FIO = fio,
                            Age = age,
                            Height = height,
                            BirthDate = birthDate,
                            BirthPlace = birthPlace
                        };

                        repository.AddWorker(newWorker);
                        Console.WriteLine("Запись успешно добавлена.");
                    }
                    else
                    {
                        Console.WriteLine("Некорректный формат даты рождения.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный рост.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный возраст.");
            }
        }

        public void DeleteWorker()
        {
            Console.Write("Введите ID записи для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                repository.DeleteWorker(id);
                Console.WriteLine("Запись успешно удалена.");
            }
            else
            {
                Console.WriteLine("Некорректный ID.");
            }
        }

        public void LoadWorkersInDateRange()
        {
            Console.Write("Введите начальную дату (дд.мм.гггг): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateFrom))
            {
                Console.Write("Введите конечную дату (дд.мм.гггг): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateTo))
                {
                    Worker[] workersInRange = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);
                    DisplayWorkers(workersInRange);
                }
                else
                {
                    Console.WriteLine("Некорректный формат конечной даты.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный формат начальной даты.");
            }
        }

        public void DisplayWorkers(Worker[] workers)
        {
            if (workers.Length > 0)
            {
                Console.WriteLine("ID\tДата создания\t\tФ.И.О.\t\t\t\tВозраст\tРост\tДата рождения\t\tМесто рождения");
                foreach (Worker worker in workers)
                {
                    Console.WriteLine($"{worker.Id}\t{worker.CreationDate:dd.MM.yyyy HH:mm}\t{worker.FIO,-30}\t{worker.Age}\t{worker.Height}\t{worker.BirthDate:dd.MM.yyyy}\t\t{worker.BirthPlace}");
                }
            }
            else
            {
                Console.WriteLine("Нет данных для отображения.");
            }
        }
    }
}
