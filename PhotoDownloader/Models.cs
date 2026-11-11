using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoDownloader
{
    public class Root
    {
        public List<PersonInfo> People { get; set; }
    }

    public class PersonInfo
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Pol { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
    }
}
