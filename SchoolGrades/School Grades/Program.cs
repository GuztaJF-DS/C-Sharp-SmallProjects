namespace School_Grades
{
    internal class Program
    {
        public static T ValidateInput<T>(Func<string, bool> condition, Func<string, T> onSuccess, Action<string> onFailure)
        {
            bool isValid = false;
            T? result = default(T);

            while (!isValid)
            {
                string? input = Console.ReadLine();

                if (condition(input))
                {
                    result = onSuccess(input);
                    isValid = true;
                }
                else
                {
                    onFailure(input);
                }
            }

            return result;
        }
    class Student
        {

            private decimal _score;
            private decimal _questionsCorrect;
            private decimal TotalQuestions;
            public Student(decimal totalQuestions) {
                TotalQuestions = totalQuestions;
            }
            public string Name { get; set; } = string.Empty;
            public string Gender { get; set; } = string.Empty;
            public int Age { get; set; }
            public decimal QuestionsCorrect
            { get => _questionsCorrect; 
                set { 
                    _score = Decimal.Round((value / TotalQuestions) * 100);
                    _questionsCorrect = value; 
                }
            }
            public decimal Score { get { return _score; } }

        }

        static void PrintTable(Student[] SClass){
            for(int x = 0; x < SClass.Length; x++)
            {
                if (SClass[x].Score >= 50)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine($"Name: {SClass[x].Name}; Age: {SClass[x].Age}; Gender: {SClass[x].Gender}; Score: {SClass[x].Score}%");
                Console.ResetColor();
            }
        }

        static void Main(string[] args)
        {
            Student [] firstClasss = new Student[1];
            Console.WriteLine("Enter the Total of questions the Test have: ");

            int total = ValidateInput<int>(
                condition: input => int.TryParse(input, out _),
                onSuccess: input => int.Parse(input),
                onFailure: input => Console.WriteLine("Not a valid value, Input Again")
            );

            for (int x = 0; x < firstClasss.Length; x++)
            {
                Student apprentice = new Student(total);
                Console.WriteLine($"Enter the Name of the Student N°{x+1}: ");
                apprentice.Name = Console.ReadLine() ?? $"Student {x + 1}";
                Console.WriteLine($"Enter the Age of {apprentice.Name}: ");
                int age = ValidateInput<int>(
                    condition: input => int.TryParse(input, out _),
                    onSuccess: input => int.Parse(input),
                    onFailure: input => Console.WriteLine("Not a valid Age, Input Again")
                );

               apprentice.Age = age;

               apprentice.Gender = x % 2 == 0 ? "Male" : "Female";
                
               Console.WriteLine($"Enter the amount of Correct awnsers that {apprentice.Name} Had: ");
               decimal QuestionsCorrect = ValidateInput<decimal>(
                   condition: input => !decimal.TryParse(input, out decimal Validator) || (Validator <= total && Validator >= 0),
                   onSuccess: input => decimal.Parse(input),
                   onFailure: input => Console.WriteLine("Not a valid value, Input Again")
               );

               apprentice.QuestionsCorrect = QuestionsCorrect;
               firstClasss[x] = apprentice;
            }
            PrintTable(firstClasss);
        }
    }
}