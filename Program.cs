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

            var h = new HashSet<string>(File.ReadAllLines(args[0], Encoding.UTF8));

            long lineCount = 0;
            using (var reader = File.OpenText(args[1]))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }

            long i = 0, f = 0;

            using (var reader = new StreamReader(args[1]))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (h.Contains(line[..line.IndexOf(' ')]))
                    {
                        Console.Error.Write("\r");
                        Console.WriteLine(line);
                        Console.Beep();
                        f++;
                    }

                    if (i % 1000000 == 0)
                    {
                        Console.Error.Write(string.Format("\r{0:N0}/{1:N0} found: {2}", i, lineCount, f));
                    }
                    i++;
                }
            }

            Console.Error.Write(string.Format("\r{0:N0}/{1:N0} found: {2}", i, lineCount, f));
            Console.Error.WriteLine();
            Console.Beep();
            Console.Error.WriteLine("End of program");
            return 0;
        }
    }
}
