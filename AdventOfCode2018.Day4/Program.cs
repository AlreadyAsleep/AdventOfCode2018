using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day4
{
    class Entry
    {
        public DateTime Date { get; set; }
        public string EntryData { get; set; }

        public override string ToString()
        {
            return $"{Date.ToString()} {EntryData}";
        }
    }

    class Night
    {
        public List<Entry> Entries { get; set; }
        public int Id { get; set; }
        public HashSet<int> MinutesAsleep { get; set; }
        public int TimeAsleep
        {
            get
            {
                return MinutesAsleep.Count();
            }
        }

        public Night()
        {
            Entries = new List<Entry>();
            MinutesAsleep = new HashSet<int>();
        }
    }

    class Program
    {


        static void Main()
        {
            List<Entry> entries = new List<Entry>();
            List<Night> nights = new List<Night>();
            string s;
            using (var streamReader = File.OpenText("../../in.txt"))
            {
                while ((s = streamReader.ReadLine()) != null)
                {
                    var entry = new Entry();
                    int index = s.IndexOf("]");
                    string temp = s.Substring(1, index - 1);
                    entry.Date = DateTime.Parse(temp);
                    s = s.Substring(index + 1).Trim();
                    entry.EntryData = s;

                    entries.Add(entry);
                }

                entries.Sort((x, y) => x.Date > y.Date ? 1 : -1);

                var id = 0;
                Night night = new Night();
                for (int i = 0; i < entries.Count(); i++)
                {
                    if (entries[i].EntryData[0] == 'G')
                    {
                        string str = entries[i].EntryData.Substring(entries[i].EntryData.IndexOf('#') + 1);
                        str = str.Remove(str.IndexOf(' '));
                        int.TryParse(str, out int temp);
                        id = temp;

                        nights.Add(night);
                        night = new Night();
                        night.Id = id;

                    }
                    if (entries[i].EntryData[0] == 'w')
                    {
                        for (int j = night.Entries.Last().Date.Minute; j < entries[i].Date.Minute; j++)
                        {
                            night.MinutesAsleep.Add(j);
                        }
                    }
                    night.Entries.Add(entries[i]);
                }




                //Option 1

                Dictionary<int, int> guardSleepTimes = new Dictionary<int, int>();
                foreach (Night n in nights)
                {
                    if (guardSleepTimes.ContainsKey(n.Id))
                    {
                        guardSleepTimes[n.Id] += n.TimeAsleep;
                    }
                    else
                    {
                        guardSleepTimes[n.Id] = n.TimeAsleep;
                    }
                }

                id = guardSleepTimes.OrderByDescending(x => x.Value).First().Key;

                Console.WriteLine($"Best guard: #{id}");

                var orderedNights = nights
                    .Where(x => x.Id == id)
                    .OrderBy(x => x.TimeAsleep);

                int bestMinute = 0, highest = 0;
                for(int i = 0; i < 60; i++)
                {
                    int containsCount = 0;
                    foreach (Night n in orderedNights)
                    {
                        if (n.MinutesAsleep.Contains(i))
                        {
                            containsCount++;
                        }
                    }
                    if(containsCount > highest)
                    {
                        bestMinute = i;
                        highest = containsCount;
                    }
                }

                Console.WriteLine($"Best Minute: {bestMinute}");

                //Option 2

                Dictionary<int, Tuple<int, int>> guardHighestMinutes = new Dictionary<int, Tuple<int, int>>();
                Dictionary<int, List<Night>> guards = new Dictionary<int, List<Night>>();

                nights.ForEach(x =>
                {
                    if (guards.ContainsKey(x.Id))
                    {
                        guards[x.Id].Add(x);
                    }
                    else
                    {
                        guards[x.Id] = new List<Night>();
                        guards[x.Id].Add(x);
                    }
                });
                
                foreach(int key in guards.Keys)
                {
                    guardHighestMinutes[key] = new Tuple<int, int>(0, 0);
                    for (int i = 0; i < 60; i++)
                    {
                        int containsCount = 0;
                        foreach(Night n in guards[key])
                        {
                            if (n.MinutesAsleep.Contains(i))
                            {
                                containsCount++;
                            }
                        }
                        if(containsCount > guardHighestMinutes[key].Item2)
                        {

                            guardHighestMinutes[key] = new Tuple<int, int>(i, containsCount);
                        }
                    }
                }

                var highestGuard = guardHighestMinutes.OrderByDescending(x => x.Value.Item2).First();
                Console.WriteLine($"Best Guard: {highestGuard.Key} Asleep {highestGuard.Value.Item2} times on minute {highestGuard.Value.Item1}");

            }
            Console.ReadLine();
        }
    }
}
