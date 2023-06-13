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

            var lineCount = 0;
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
                    if (i % 100000 == 0)
                    {
                        Console.Error.Write($"\r");
                        Console.Error.Write(string.Format("{0:N0}", i));
                        Console.Error.Write($"/");
                        Console.Error.Write(string.Format("{0:N0}", lineCount));
                        if (f > 0)
                        {
                            Console.Error.Write(" found: " + f);
                        }
                    }

                    if (h.Contains(line[..line.IndexOf(' ')]))
                    {
                        Console.WriteLine($"\r" + line);
                        Console.Beep();
                        f++;
                    }

                    i++;
                }
            }

            Console.Error.Write($"\r");
            Console.Error.Write(string.Format("{0:N0}", i));
            Console.Error.Write($"/");
            Console.Error.Write(string.Format("{0:N0}", lineCount));
            if (f > 0)
            {
                Console.Error.Write(" found: " + f);
            }
            Console.WriteLine();
            Console.Beep();
            Console.Error.WriteLine("End of program");
            return 0;
        }
    }
}
