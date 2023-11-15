namespace OOP
{
    struct Worker
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string FIO { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
    }
    class Repository
    {
        private string fileName = "workers.txt";

        public Worker[] GetAllWorkers()
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close(); // Если файла нет,то создаем новый
            }

            string[] lines = File.ReadAllLines(fileName);
            return lines.Select(ParseWorker).ToArray();
        }

        public Worker GetWorkerById(int id)
        {
            Worker[] workers = GetAllWorkers();
            return workers.FirstOrDefault(worker => worker.Id == id);
        }

        public void DeleteWorker(int id)
        {
            Worker[] workers = GetAllWorkers();
            workers = workers.Where(worker => worker.Id != id).ToArray();
            SaveWorkersToFile(workers);
        }

        public void AddWorker(Worker worker)
        {
            worker.Id = GetNextId();
            worker.CreationDate = DateTime.Now;

            string workerData = $"{worker.Id}#{worker.CreationDate:dd.MM.yyyy HH:mm}#{worker.FIO}#{worker.Age}#{worker.Height}#{worker.BirthDate:dd.MM.yyyy}#{worker.BirthPlace}";

            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(workerData);
            }
        }

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            Worker[] workers = GetAllWorkers();
            Console.WriteLine($"Всего записей: {workers.Length}");
            Console.WriteLine($"Диапазон дат: {dateFrom:dd.MM.yyyy} - {dateTo:dd.MM.yyyy}");

            var filteredWorkers = workers
        .Where(worker =>
        {
            Console.WriteLine($"Сравниваем запись {worker.BirthDate:dd.MM.yyyy HH:mm:ss} в диапазоне с {dateFrom:dd.MM.yyyy} - {dateTo:dd.MM.yyyy}");
            Console.WriteLine($"начальная дата {worker.BirthDate.Date.CompareTo(dateFrom.Date) >= 0}");
            Console.WriteLine($"конечная дата {worker.BirthDate.Date.CompareTo(dateTo.Date) <= 0}");

            return worker.BirthDate.Date.CompareTo(dateFrom.Date) >= 0 && worker.BirthDate.Date.CompareTo(dateTo.Date) <= 0;
        })
        .ToArray();

            Console.WriteLine($"Отфильтрованные записи: {filteredWorkers.Length}");

            return filteredWorkers;
        }

        private Worker ParseWorker(string line)
        {
            string[] parts = line.Split('#');
            return new Worker
            {
                Id = int.Parse(parts[0]),
                CreationDate = DateTime.ParseExact(parts[1], "dd.MM.yyyy HH:mm", null),
                FIO = parts[2],
                Age = int.Parse(parts[3]),
                Height = int.Parse(parts[4]),
                BirthDate = DateTime.ParseExact(parts[5], "dd.MM.yyyy", null),
                BirthPlace = parts[6]
            };
        }

        private void SaveWorkersToFile(Worker[] workers)
        {
            File.WriteAllLines(fileName, workers.Select(worker =>
                $"{worker.Id}#{worker.CreationDate:dd.MM.yyyy HH:mm}#{worker.FIO}#{worker.Age}#{worker.Height}#{worker.BirthDate:dd.MM.yyyy}#{worker.BirthPlace}"));
        }

        private int GetNextId()
        {
            Worker[] workers = GetAllWorkers();

            if (workers.Length > 0)
            {
                return workers.Max(worker => worker.Id) + 1;
            }

            return 1;
        }
    }
}
