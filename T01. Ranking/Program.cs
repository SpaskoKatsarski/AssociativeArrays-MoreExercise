using System;
using System.Collections.Generic;
using System.Linq;

namespace T01._Ranking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Dictionary<string, string> contests = new Dictionary<string, string>();

            //Dictionary<string, Dictionary<string, int>> contestsWithStudentsAndTheirPoints = new Dictionary<string, Dictionary<string, int>>();

            //string command;

            //while ((command = Console.ReadLine()) != "end of contests")
            //{
            //    string[] contestsAndPasswords = command.Split(':', StringSplitOptions.RemoveEmptyEntries);

            //    string contestName = contestsAndPasswords[0];
            //    string contestPassword = contestsAndPasswords[1];

            //    contests.Add(contestName, contestPassword);
            //}

            //string input;

            //while ((input = Console.ReadLine()) != "end of submissions")
            //{
            //    string[] cmdArgs = input.Split("=>", StringSplitOptions.RemoveEmptyEntries);

            //    string currContest = cmdArgs[0];
            //    string currPassword = cmdArgs[1];

            //    if (contests[currContest] != currPassword)
            //    {
            //        continue;
            //    }

            //    string currStudentName = cmdArgs[2];
            //    int points = int.Parse(cmdArgs[3]);

            //    Dictionary<string, int> currStudentDic = new Dictionary<string, int> { { currStudentName, points } };

            //    // CONTEST -> DICTIONARY (key: STUDENT,  value: POINTS)

            //    if (contestsWithStudentsAndTheirPoints[currContest].ContainsKey(currStudentName)) // getting the info about the currentStudent
            //    {
            //        contestsWithStudentsAndTheirPoints[currContest][currStudentName] += points;
            //    }
            //    else
            //    {
            //        contestsWithStudentsAndTheirPoints.Add(currContest, currStudentDic);
            //    }
            //}

            Dictionary<string, string> contestAndPassword = new Dictionary<string, string>();

            SortedDictionary<string, Dictionary<string, int>> nameAndContesstWithPoints = new SortedDictionary<string, Dictionary<string, int>>();

            string inputContest = string.Empty;
            string[] separator = { "=>" };

            while ((inputContest = Console.ReadLine()) != "end of contests")
            {
                string[] str = inputContest.Split(':');
                string contest = str[0];
                string password = str[1];
                contestAndPassword.Add(contest, password);
            }
            string inputCollection = string.Empty;
            while ((inputCollection = Console.ReadLine()) != "end of submissions")
            {
                string[] str2 = inputCollection.Split(separator, StringSplitOptions.None);
                string contestFromCollection = str2[0];
                string passwordFromCollection = str2[1];
                string nameCollection = str2[2];
                int pointFromCollection = int.Parse(str2[3]);
                if (contestAndPassword.ContainsKey(contestFromCollection)
                    && contestAndPassword.ContainsValue(passwordFromCollection))
                {
                    if (nameAndContesstWithPoints.ContainsKey(nameCollection) == false)
                    {
                        nameAndContesstWithPoints.Add(nameCollection, new Dictionary<string, int>());
                        nameAndContesstWithPoints[nameCollection].Add(contestFromCollection, pointFromCollection);
                    }
                    if (nameAndContesstWithPoints[nameCollection].ContainsKey(contestFromCollection))
                    {
                        if (nameAndContesstWithPoints[nameCollection][contestFromCollection] < pointFromCollection)
                        {
                            nameAndContesstWithPoints[nameCollection][contestFromCollection] = pointFromCollection;
                        }
                    }
                    else
                    {
                        nameAndContesstWithPoints[nameCollection].Add(contestFromCollection, pointFromCollection);
                    }
                }

            }
            Dictionary<string, int> usernameTotalPoints = new Dictionary<string, int>();
            foreach (var kvp in nameAndContesstWithPoints)
            {
                usernameTotalPoints[kvp.Key] = kvp.Value.Values.Sum();
            }
            string bestName = usernameTotalPoints
                .Keys
                .Max();
            int bestPoints = usernameTotalPoints
                .Values
                .Max();

            foreach (var kvp in usernameTotalPoints)
            {
                if (kvp.Value == bestPoints)
                {
                    Console.WriteLine($"Best candidate is {kvp.Key} with total {kvp.Value} points.");

                }
            }
            Console.WriteLine("Ranking:");
            foreach (var name in nameAndContesstWithPoints)
            {
                Dictionary<string, int> dict = name.Value;
                dict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                Console.WriteLine("{0}", name.Key);
                foreach (var kvp in dict)
                {
                    Console.WriteLine("#  {0} -> {1}", kvp.Key, kvp.Value);
                }

            }
        }
    }
}
