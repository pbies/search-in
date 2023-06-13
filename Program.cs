using System;
using System.Collections.Generic;
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
                Console.WriteLine("Use: search-in patterns.txt search-in.txt | tee -a result.txt");
                return 1;
            }

            var patternsFile = args[0];
            var searchFile = args[1];
            var pat = File.ReadAllLines(patternsFile, Encoding.UTF8);

            var h = new HashSet<string>(pat);

            long i = 0, f = 0;

            using (var reader = new StreamReader(searchFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {

                    if (h.Contains(line[..line.IndexOf(' ')]))
                    {
                        Console.WriteLine($"\r" + line);
                        Console.Beep();
                        f++;
                    }

                    if (i % 10000 == 0)
                    {
                        Console.Error.Write($"\r");
                        Console.Error.Write(string.Format("{0:N0}", i));
                        if (f > 0)
                        {
                            Console.Error.Write(" found: " + f);
                        }
                    }

                    i++;
                }
            }

            Console.Error.Write($"\r");
            Console.Error.WriteLine(string.Format("{0:N0}", i));
            Console.Beep();
            Console.Error.WriteLine("End of program");
            return 0;
        }
    }
}
