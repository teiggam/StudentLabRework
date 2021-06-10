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

                string number = GetUserInput("\nWhich student would you like to learn about?");
                try
                {
                    int index = int.Parse(number);

                    bool moreInfo = true;
                    while (moreInfo == true)
                    {

                        try
                        {
                            string additional = GetUserInput($"What additional information would you like about {allStudents[index].Name}? (Hometown, State, FavoriteFood)");
                            if (additional == "hometown")
                            {
                                Console.WriteLine($"{allStudents[index].Name}'s hometown is {allStudents[index].HomeTown}.");
                            }
                            else if (additional == "favoritefood")
                            {
                                Console.WriteLine($"{allStudents[index].Name}'s favorite food is {allStudents[index].FavoriteFood}.");
                            }
                            else if (additional == "state")
                            {
                                Console.WriteLine($"{allStudents[index].Name}'s state is {allStudents[index].State}.");
                            }
                            else
                            {
                                Console.WriteLine("I don't understand, please try again.");
                                break;
                            }

                            moreInfo = GetContinue($"Would you like to know more?");
                        }
                        catch
                        {
                            Console.WriteLine("I don't understand, please start over.");
                        }
                    }
                    goOn = GetContinue("Would you like to ask about someone else? (Y/N)");


                }
                catch
                {
                    Console.WriteLine("I don't understand, please start over.\n");
                }

                bool AddAnother = true;
                while (AddAnother == true)
                {
                    reader.Close();
                    string addStudent = (GetUserInput($"Would you like to add a student to the list? (Y/N)"));
                    if (addStudent == "y")
                    {
                        AddStudent();
                        GetContinue("Would you like to add another student?");
                       
                    }
                    else
                    {
                        Console.WriteLine("Goodbye!");
                    }
                    AddAnother = false;
                }
            }
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
            writer.Write($"{original}\n{line}");

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
            string input = Console.ReadLine().ToLower();
            return input;
        }

        public static bool GetContinue(string a)
        {
            Console.WriteLine(a);
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                return true;
            }
            else if (input.ToLower() == "n")
            {
                return false;
            }
            else
            {
                //Console.WriteLine("I don't understand that input. Please try again.");
                return GetContinue("I don't understand that input. Please try again.");
            }
        }

        //public static bool GetMoreInfo(string would)
        //{
        //    //Console.WriteLine($"Would you like to know more?");
        //    string input = Console.ReadLine().ToLower();
        //    try
        //    {

        //        if (input.ToLower() == "y")
        //        {
        //            return true;
        //        }
        //        else if (input.ToLower() == "n")
        //        {
        //            return false;
        //        }
        //        return GetMoreInfo();
        //    }
        //    catch
        //    {
        //        Console.WriteLine("I don't understand that input. Please try again.");
        //        return GetMoreInfo();
        //    }
        //}
    }

}
