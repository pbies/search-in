using System;
using System.IO;
using System.Text;

namespace search_in
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Use: search-in patterns.txt search-in.txt");
                return 1;
            }

            var patternsFile = args[0];
            var searchFile = args[1];

            var pat = File.ReadAllLines(patternsFile, Encoding.UTF8);

            int i = 0;

            using (var reader = new StreamReader(searchFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var p in pat)
                    {
                        if (line.StartsWith(p))
                        {
                            Console.WriteLine(line);
                        }
                    }
                    if (i % 1000 == 0)
                    {
                        Console.Error.Write($"\r");
                        Console.Error.Write(string.Format("{0:N0}", i));
                    }
                    i++;
                }
                Console.Error.Write($"\r");
                Console.Error.Write(string.Format("{0:N0}", i));
                Console.Error.Write($"\a");
            }

            Console.Error.WriteLine("End of program");
            return 0;
        }
    }
}
