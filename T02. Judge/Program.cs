using System;
using System.Collections.Generic;
using System.Linq;

namespace T02._Judge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, int>> contestsAndStudentsWithPoints =
                new Dictionary<string, Dictionary<string, int>>();
            
            Dictionary<string, int> individualScore = new Dictionary<string, int>();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            string command;

            while ((command = Console.ReadLine()) != "no more time")
            {
                string[] contestData = command.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);

                string studentName = contestData[0];
                string taskName = contestData[1];
                int points = int.Parse(contestData[2]);
                
                if (contestsAndStudentsWithPoints.ContainsKey(taskName))
                {
                    if (contestsAndStudentsWithPoints[taskName].ContainsKey(studentName))
                    {
                        if (contestsAndStudentsWithPoints[taskName][studentName] < points)
                        {
                            contestsAndStudentsWithPoints[taskName][studentName] = points;
                        }
                    }
                    else
                    {
                        contestsAndStudentsWithPoints[taskName].Add(studentName, points);
                    }
                }
                else 
                {
                    contestsAndStudentsWithPoints.Add(taskName, new Dictionary<string, int> { { studentName, points } });
                }
            }

            foreach (var kvp in contestsAndStudentsWithPoints)
            {
                string contestName = kvp.Key;

                int countOfParticipants = kvp.Value.Keys.Count;

                Console.WriteLine($"{contestName}: {countOfParticipants} participants");

                Dictionary<string, int> studentsResultsInCurrentContest = kvp.Value;
                int postition = 1;

                foreach (var student in studentsResultsInCurrentContest.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    Console.WriteLine($"{postition}. {student.Key} <::> {student.Value}");
                    postition++;
                }
            }

            Console.WriteLine("Individual standings:");

            foreach (var student in contestsAndStudentsWithPoints)
            {
                Dictionary<string, int> studentsResultsInCurrentContest = student.Value;

                foreach (var st in studentsResultsInCurrentContest)
                {
                    string studentName = st.Key;
                    int studentPoints = st.Value;

                    if (!individualScore.ContainsKey(studentName))
                    {
                        individualScore[studentName] = 0;
                    }

                    individualScore[studentName] += studentPoints;
                }
            }

            int counter = 1;
            foreach (KeyValuePair<string, int> participant in individualScore.OrderByDescending(kvp => kvp.Value).ThenBy(s => s.Key))
            {
                Console.WriteLine($"{counter}. {participant.Key} -> {participant.Value}");
                counter++;
            }
        }
    }
}
