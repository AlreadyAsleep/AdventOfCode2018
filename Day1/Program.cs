using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            int frequency = 0;
            var intSet = new HashSet<int>();
            string s;
            int temp;
            bool duplicate = false;
            using (var streamReader = File.OpenText("../../in1.txt"))
            {
                while((s = streamReader.ReadLine()) != null)
                {
                    intSet.Add(frequency);
                    int.TryParse(s, out temp);
                    frequency += temp;
                }
            }
            Console.WriteLine($"Final Frequency: {frequency}");
            while (!duplicate)
            {
                using (var streamReader = File.OpenText("../../in1.txt"))
                {
                    while ((s = streamReader.ReadLine()) != null)
                    {
                        if (intSet.Contains(frequency))
                        {
                            duplicate = true;
                            Console.WriteLine($"Duplicate Detected {frequency}");
                            break;
                        }
                        intSet.Add(frequency);
                        int.TryParse(s, out temp);
                        frequency += temp;
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
