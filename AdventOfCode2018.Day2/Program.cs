using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day2
{
    class Program
    {
        static void part1()
        {
            string s;
            Dictionary<char, int> chars = new Dictionary<char, int>();
            int twoCount = 0, threeCount = 0;
            bool checkTwo = true, checkThree = true;
            using (var streamReader = File.OpenText("../../in.txt"))
            {
                while ((s = streamReader.ReadLine()) != null)
                {
                    foreach (char c in s)
                    {
                        chars[c] = chars.ContainsKey(c) ? chars[c] + 1 : 1;
                    }

                    foreach (char c in chars.Keys)
                    {
                        if (checkTwo && chars[c] == 2)
                        {
                            checkTwo = false;
                            twoCount++;
                        }
                        if (checkThree && chars[c] == 3)
                        {
                            checkThree = false;
                            threeCount++;
                        }
                    }
                    checkTwo = checkThree = true;
                    chars.Clear();
                }
                Console.WriteLine($"Checksum: {twoCount * threeCount}");
            }
        }

        static void part2()
        {
            string s;
            List<string> lines = new List<string>();
            int lineLength;
            using (var streamReader = File.OpenText("../../in.txt"))
            {
                while ((s = streamReader.ReadLine()) != null)
                {
                    lines.Add(s);
                }
                lineLength = lines[0].Length;

                foreach(string str1 in lines)
                {
                    foreach(string str2 in lines)
                    {
                        if(str1 == str2)
                        {
                            continue;
                        }
                        string common = getSimilarChars(str1, str2);
                        if(common.Length == lineLength - 1)
                        {
                            Console.WriteLine($"Found matching IDs: {common}");
                            break;
                        }
                        
                    }
                }
            }
        }

        static string getSimilarChars(string str1, string str2)
        {
            StringBuilder s = new StringBuilder();
            for(int i = 0; i < str1.Length; i++)
            {
                if(str1[i] == str2[i])
                {
                    s.Append(str1[i]);
                }
            }
            return s.ToString();
        }

        static void Main(string[] args)
        {
            part1();
            part2();
            Console.ReadLine();
        }
    }
}
