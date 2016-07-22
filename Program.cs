using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace RailwayWars.SampleBot
{
    internal class Program
    {
        static class Settings
        {
            public static string ServerHost => ConfigurationManager.AppSettings["ServerHost"] ?? "localhost";
            public static int ServerPort => Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"] ?? "9977");
            public static string LoginId => ConfigurationManager.AppSettings["LoginId"] ?? "PL1";
        }

        private static void Main(string[] args)
        {
            var loginId = (args.Length > 0) ? args[0] : Settings.LoginId;
            try
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.NoDelay = true;
                    Console.WriteLine($"Connecting to {Settings.ServerHost}:{Settings.ServerPort}");
                    tcpClient.Connect(Settings.ServerHost, Settings.ServerPort);
                    using (var reader = new StreamReader(tcpClient.GetStream()))
                    {
                        using (var writer = new StreamWriter(tcpClient.GetStream()))
                        {
                            writer.AutoFlush = true;

                            // Server should start with "ID" line.
                            if (reader.ReadLine() != "ID")
                                throw new InvalidDataException("Server didn't ask for my identity.");

                            // Send our login id.
                            writer.WriteLine(loginId);
                            // Read our snake id.
                            var myId = reader.ReadLine().Trim();
                            Console.WriteLine("My id: {0}", myId);

                            var botEngine = new BotEngine(myId);

                            // Loop till key press.
                            while (!Console.KeyAvailable && !reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var gameBoardState =
                                    new GameBoardState(JsonConvert.DeserializeObject<GameStateDTO>(line), myId);
                                var offers = botEngine.GetNextMove(gameBoardState).ToList();
                                if (offers.Any())
                                {
                                    offers.ForEach(offer =>
                                    {
                                        Console.WriteLine("Sending command {0}", offer.Command);
                                        writer.WriteLine(offer.Command);
                                    });
                                }
                                else
                                {
                                    Console.WriteLine("No command to send!");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
        }
    }
}
