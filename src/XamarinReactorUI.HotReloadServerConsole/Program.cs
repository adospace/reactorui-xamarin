using CommandLine;
using System;

namespace XamarinReactorUI.HotReloadServerConsole
{
    class Program
    {
        public class Options
        {
            [Option('f', "folder", Required = true, HelpText = "Local folder to monitor.")]
            public string Folder { get; set; }

            [Option('p', "port", Required = false, HelpText = "Local port to listen for connections.")]
            public int Port { get; set; } = 23891;
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {



                   });
        }
    }
}
