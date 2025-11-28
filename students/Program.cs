namespace Students;

struct Student
{
    public int Id;
    public string Name;
    public string Group;
    public byte Age;

    public static void PrintHeader()
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("| ID | Name                 | Group  | Age |");
        Console.WriteLine("+----+----------------------+--------+-----+");
    }

    public void Print()
    {
        Console.WriteLine($"| {Id,-2} | {Name,-20} | {Group,-6} | {Age,-3} |");
    }
}

class App
{
    public static void Main(string[] args)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "students.csv");

        string[] studentsFromFile;
        try
        {
            studentsFromFile = File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка читання файлу: " + ex.Message);
            return;
        }

        Student[] students = new Student[studentsFromFile.Length - 1];
        int index = 0;

        for (int i = 1; i < studentsFromFile.Length; i++)
        {
            string line = studentsFromFile[i];
            string[] parts = line.Split(',');

            // перевірка кількості елементів
            if (parts.Length < 4)
            {
                Console.WriteLine($"Помилка: неправильний формат рядка \"{line}\"");
                continue;
            }

            Student newStudent = new Student();

            // перевірка ID
            if (!int.TryParse(parts[0], out newStudent.Id))
            {
                Console.WriteLine($"Помилка: ID не є числом у рядку \"{line}\"");
                continue;
            }

            newStudent.Name = parts[1];
            newStudent.Group = parts[2];

            // перевірка віку
            if (!byte.TryParse(parts[3], out newStudent.Age))
            {
                Console.WriteLine($"Помилка: Age не є числом у рядку \"{line}\"");
                continue;
            }

            students[index++] = newStudent;
        }


        Student.PrintHeader();
        for (int i = 0; i < index; i++)
        {
            students[i].Print();
        }
        Console.WriteLine("+----+----------------------+--------+-----+");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
