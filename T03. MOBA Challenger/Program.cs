using System;
using System.Collections.Generic;
using System.Linq;

namespace T03._MOBA_Challenger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, int>> allCompetitors =
                new Dictionary<string, Dictionary<string, int>>();

            string command;

            while ((command = Console.ReadLine()) != "Season end")
            {
                string[] cmdArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (cmdArgs[1] == "->")
                {
                    // We have to add info for the current player. "{player} -> {position} -> {skill}"
                    string playerName = cmdArgs[0];
                    string playerPosition = cmdArgs[2];
                    int skillPoints = int.Parse(cmdArgs[4]);

                    if (!allCompetitors.ContainsKey(playerName))
                    {
                        allCompetitors.Add(playerName, new Dictionary<string, int>
                        {
                            {playerPosition, skillPoints}
                        });
                    }
                    else
                    {
                        // We have the player name and stats:
                        if (allCompetitors[playerName].ContainsKey(playerPosition))
                        {
                            if (allCompetitors[playerName][playerPosition] < skillPoints)
                            {
                                allCompetitors[playerName][playerPosition] = skillPoints;
                            }
                        }
                        else
                        {
                            allCompetitors[playerName].Add(playerPosition, skillPoints);
                        }
                    }
                }
                else
                {
                    // Players are in battle. "{player} vs {player}"
                    string playerOne = cmdArgs[0];
                    string playerTwo = cmdArgs[2];

                    if (!(allCompetitors.ContainsKey(playerOne) && allCompetitors.ContainsKey(playerTwo)))
                    {
                        continue;
                    }

                    foreach (var playerOneData in allCompetitors[playerOne])
                    {
                        foreach (var playerTwoData in allCompetitors[playerTwo])
                        {
                            if (playerOneData.Key == playerTwoData.Key)
                            {
                                // Safe check:
                                if (playerOneData.Value == playerTwoData.Value)
                                {
                                    continue;
                                }

                                // GOT THE BUG!!!
                                if (playerOneData.Value > playerTwoData.Value)
                                {
                                    allCompetitors.Remove(playerTwo);
                                }
                                else
                                {
                                    allCompetitors.Remove(playerOne);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var player in allCompetitors.OrderByDescending(x => x.Value.Values.Sum()).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{player.Key}: {player.Value.Values.Sum()} skill");

                foreach (var playerStats in player.Value.OrderByDescending(x => x.Value).ThenBy(p => p.Key))
                {
                    Console.WriteLine($"- {playerStats.Key} <::> {playerStats.Value}");
                }
            }
        }
    }
}
