using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GetToKnowClassmates
{
    class Program
    {
        static void Main(string[] args)
        {
            GetStudentListLoop();

                if (GetContinue($"Would you like to add a student to the list? (Y/N)"))
                {
                    AddStudent();
                    GetContinue("Would you like to add another student? (Y/N)");
                }

                if (GetContinue("Would you like to see the student list and learn about the students? (Y/N)"))
                {
                    GetStudentListLoop();
                }

            Console.WriteLine("Thanks for playing!");
        }
        public static void GetStudentListLoop()
        {
            string filePath = @"Students.txt";
            StreamReader reader = new StreamReader(filePath);

            string output = reader.ReadToEnd();
            //Console.WriteLine(output);

            string[] lines = output.Split('\n');
            List<Student> allStudents = new List<Student>();

            foreach (string line in lines)
            {
                Student newSt = ConvertToStudent(line);
                if (newSt != null)
                {
                    allStudents.Add(newSt);
                }
            }
            Console.WriteLine("These are the students in our class.\n");
            bool goOn = true;
            while (goOn == true)
            {
                for (int i = 0; i < allStudents.Count; i++)
                {
                    Console.WriteLine($"{i}: {allStudents[i].Name}");
                }

                int index = GetUserIntInput("\nWhich student would you like to learn about?");


                if (index > allStudents.Count || index < 0)
                {
                    index = GetUserIntInput("That is not a valid number, please try again.");
                }

                bool moreInfo = true;
                while (moreInfo == true)
                {

                    try
                    {
                        string additional = GetUserInput($"What additional information would you like about {allStudents[index].Name}? (Hometown, State, FavoriteFood)");
                        if (additional == "hometown")
                        {
                            Console.WriteLine($"Hometown: {allStudents[index].HomeTown}");
                        }
                        else if (additional == "favoritefood")
                        {
                            Console.WriteLine($"Favorite food: {allStudents[index].FavoriteFood}");
                        }
                        else if (additional == "state")
                        {
                            Console.WriteLine($"State: {allStudents[index].State}");
                        }
                        else
                        {
                            Console.WriteLine("I don't understand, please try again.\n");
                            continue;

                        }

                        moreInfo = GetContinue($"Would you like to know more? (Y/N)");
                    }
                    catch
                    {
                        Console.WriteLine("I don't understand, please start over.");
                    }
                }
                goOn = GetContinue("Would you like to ask about someone else? (Y/N)");
            }
            reader.Close();
        }

        public static string ObjectToString(Student s)
        {
            string output = $"{s.Name}, {s.HomeTown}, {s.State}, {s.FavoriteFood} \n";
            return output;
        }

        public static void AddStudent()
        {
            string filePath = @"Students.txt";
            Student a = new Student();
            Console.WriteLine("Please input the student's name:");
            a.Name = Console.ReadLine();

            Console.WriteLine($"Please input {a.Name}'s hometown:");
            a.HomeTown = Console.ReadLine();

            Console.WriteLine("Please input their state:");
            a.State = Console.ReadLine();

            Console.WriteLine("Please input their favorite food:");
            a.FavoriteFood = Console.ReadLine();

            string line = ObjectToString(a);
            Console.WriteLine(line);

            StreamReader reader = new StreamReader(filePath);
            string original = reader.ReadToEnd();

            reader.Close();


            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(original + line);

            writer.Close();
        }

        public static Student ConvertToStudent(string line)
        {
            string[] properties = line.Split(',');
            Student s = new Student();

            if (properties.Length == 4)
            {
                s.Name = properties[0];
                s.HomeTown = properties[1];
                s.State = properties[2];
                s.FavoriteFood = properties[3];
                return s;
            }

            else
            {
                return null;
            }

        }


        public static string GetUserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine().ToLower();

        }

        public static int GetUserIntInput(string message)
        {
            try
            {
                Console.WriteLine(message);
                return int.Parse(Console.ReadLine().ToLower());
            }
            catch
            {
                return GetUserIntInput("That is not a valid number, please try again.");
            }
        }

        public static bool GetContinue(string a)
        {
            Console.WriteLine(a);
            if (Console.ReadLine().ToLower() == "y")
            {
                return true;
            }
            else if (Console.ReadLine().ToLower() == "n")
            {
                return false;
            }
            else
            {
                return GetContinue("I don't understand that input. Please try again.");
            }
        }
    }

}
