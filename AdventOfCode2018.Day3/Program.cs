using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day3
{
    class Program
    {
        class ClaimData
        {
            public int Id { get; set; }
            public int StartX { get; set; }
            public int StartY { get; set; }
            public int SizeX { get; set; }
            public int SizeY { get; set; }

            public ClaimData(string s)
            {
                int temp, index = s.IndexOf('@');
                int.TryParse(s.Substring(1, index - 1).Trim(), out temp);
                Id = temp;
                s = s.Substring(index);

                index = s.IndexOf(',');
                int.TryParse(s.Substring(1, index - 1).Trim(), out temp);
                StartX = temp;
                s = s.Substring(index);

                index = s.IndexOf(':');
                int.TryParse(s.Substring(1, index - 1).Trim(), out temp);
                StartY = temp;
                s = s.Substring(index);

                index = s.IndexOf('x');
                int.TryParse(s.Substring(1, index - 1).Trim(), out temp);
                SizeX = temp;
                s = s.Substring(index + 1);

                int.TryParse(s, out temp);
                SizeY = temp;
            }
        }

        static void Main(string[] args)
        {
            bool[,] adjacency = new bool[1000, 1000];
            bool[,] collisions = new bool[1000, 1000];
            string s;
            List<ClaimData> claims = new List<ClaimData>();
            using (var streamReader = File.OpenText("../../in.txt"))
            {
                while ((s = streamReader.ReadLine()) != null)
                {
                    var c = new ClaimData(s);
                    for(int i = c.StartX; i < c.StartX + c.SizeX; i++)
                    {
                        for(int j = c.StartY; j < c.StartY + c.SizeY; j++)
                        {
                            if(adjacency[i, j])
                            {
                                collisions[i, j] = true;
                            }
                            adjacency[i, j] = true;
                        }
                    }
                    claims.Add(c);
                }
            }

            foreach(ClaimData c in claims)
            {
                bool overlap = false;
                for (int i = c.StartX; i < c.StartX + c.SizeX; i++)
                {
                    for (int j = c.StartY; j < c.StartY + c.SizeY; j++)
                    {
                        if (collisions[i, j])
                        {
                            overlap = true;
                        }
                    }
                }
                if (!overlap)
                {
                    Console.WriteLine($"Only non-overlapper: {c.Id}");
                }
            }

            int sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (collisions[i, j])
                    {
                        sum++;
                    }
                }
            }

            Console.WriteLine($"Area Overlap: {sum}");
            Console.ReadLine();

        }
    }
}
