using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


namespace bus
{
    struct bus
    {
        public int nym_bus;
        public int nym_rout;
        public bool condit;
        public string name_driv;

        
        public bus(int nym_bus, int nym_rout, bool condit, string name_driv)
        {
            this.nym_bus = nym_bus;
            this.nym_rout = nym_rout;
            this.name_driv = name_driv;
            this.condit = condit;
        }

        public string[] ToStringArray()
        {
            return new[] 
            { 
                name_driv, 
                nym_bus.ToString(), 
                nym_rout.ToString(), 
                condit ? "на маршруте" : "в парке"
            };
        }
        public void DisplayInfo()
        {
            if (condit == true)
            {
                Console.WriteLine(
                    "Имя: {0} \n Номер автобуса: {1} \n Номер маршрута: {2} \n Состояние: на маршруте \n",
                    name_driv, nym_bus, nym_rout);
            }
            else
            {
                Console.WriteLine(
                    "Имя: {0} \n Номер автобуса: {1} \n Номер маршрута: {2} \n Состояние: в парке \n",
                    name_driv, nym_bus, nym_rout);
            }
        }

    }

    class Program
    {
        public static void lineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }
        static void showStatus(string fileName, string status)
        {
            int lineIndex = 0;
            string driver = string.Empty;
            foreach (string line in File.ReadLines(@"F:\test.txt"))
            {
                switch (lineIndex % 4)
                {
                    case 0:
                        driver = line;
                        break;
                    case 3:
                        if (line == status)
                        {
                            Console.WriteLine(driver);
                        }
                        break;
                }
                ++lineIndex;
            }
        }
        static void CatFileFamily(string fileName)
        {
            int lineIndex = 0;
            foreach (string line in File.ReadLines(@"F:\test.txt"))
            {
                switch (lineIndex % 4)
                {
                    case 0:
                        Console.WriteLine($"\n Запись №{lineIndex / 4}");
                        Console.WriteLine($"ФИО: {line.Substring(0, 2)}");
                        break;
                    case 1:
                        Console.WriteLine($"№ автобуса: {line}");
                        break;
                    case 2:
                        Console.WriteLine($"№ маршрута: {line}");
                        break;
                    case 3:
                        Console.WriteLine($"Статус: {line}");
                        Console.WriteLine("(конец записи)");
                        break;
                }
                ++lineIndex;
            }
        }
        public static void add_card(ref bus ptr)
        {
            string path = @"F:\test.txt";
            string x;
            Console.Write("\nВведите ФИО водителя автобуса: ");
            ptr.name_driv = Console.ReadLine();
            Console.WriteLine("\nВведите номер автобуса: ");
            ptr.nym_bus = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите номер маршрута: ");
            ptr.nym_rout = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nСостояние (введите 'в парке' или 'на маршруте'): ");
            x = Console.ReadLine();
            if (x == "в парке")
            {
                ptr.condit = false;
            }
            else
            {
                ptr.condit = true;    
            }
            File.AppendAllLines(path, ptr.ToStringArray());
            ptr.DisplayInfo();
        }

        static void CatFile(string fileName)
        {
            int lineIndex = 0;
            foreach (string line in File.ReadLines(@"F:\test.txt"))
            {
                switch (lineIndex % 4)
                {
                    case 0:
                        Console.WriteLine($"\nЗапись №{lineIndex / 4}");
                        Console.WriteLine($"ФИО: {line}");
                        break;
                    case 1:
                        Console.WriteLine($"№ автобуса: {line}");
                        break;
                    case 2:
                        Console.WriteLine($"№ маршрута: {line}");
                        break;
                    case 3:
                        Console.WriteLine($"Статус: {line}");
                        Console.WriteLine("(конец записи)");
                        break;
                }
                ++lineIndex;
            }
        }

        static void DeleteEntry(string fileName, int entryIndex)
        {
            string[] input = File.ReadAllLines(fileName);
            string[] output = new string[input.Length - 4];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (i / 4 < entryIndex) ? input[i] : input[i + 4];
            }
            File.WriteAllLines(fileName, output);
        }
        
        
        static void Main()
        {
            string path = @"F:\test.txt";
            int x,d;
            string NewText;
            bus ptr = new bus();
            add_card(ref ptr);
            CatFile(path);
            Console.WriteLine("\nВведите какую запись удалить (начинается 0): ");
            x = int.Parse(Console.ReadLine());
            DeleteEntry(path, x);
            CatFile(path);
            
            Console.WriteLine("\nВведите строку, которую нужно отредактировать: ");
            d = int.Parse(Console.ReadLine());
            Console.WriteLine("\nВведите измения: ");
            NewText = Console.ReadLine();
            lineChanger(NewText, path, d);
                
            Console.WriteLine("\n Выводим информацию о водителях по шаблону: ");
            CatFileFamily(path);
            Console.WriteLine("\n ");

            string proverka = "на маршруте";
            Console.WriteLine("\n Выводим список водителей автобусов, находящихся на маршруте: ");
            showStatus(path, proverka);
        }
    }
}
