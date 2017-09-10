using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanReadableCodeService.Services.Storage.Impl
{
    public class HumanReadableCodeGenerator : IHumanReadableCodeGenerator
    {
        private static Random _random = new Random();
        private readonly EnvironmentInfo _options;

        public readonly List<string> Records;
        public int SymbolsLength { get; set; }
        public int RecordsAmount { get; set; }
        public string Prefix { get; set; }

        public HumanReadableCodeGenerator(IOptions<EnvironmentInfo> options)
        {
            _options = options.Value;
            Prefix = _options.PrefixFormat;
            SymbolsLength = _options.SymbolsLength;
            RecordsAmount = _options.RecordsAmount;
            Records = GenerateRecords();
        }

        //public HumanReadableCodeGenerator(string prefix, int symbolsLength, int recordsAmount)
        //{
        //    Prefix = prefix;
        //    Records = GenerateRecords();
        //}

        public List<string> GetAllRecords()
        {
            return Records;
        }

        public List<string> GenerateRecords()
        {
            // RD1708-IRAMIM
            //string prefix = "RB";
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString().Substring(2);

            string firstPart = "";

            if (month.Length == 1)
                month = "0" + month;
            if (year.Length == 1)
                year = "0" + year;

            firstPart += Prefix + year + month + '-';

            var records = new List<string>();
            var vowels = new char[] { 'a', 'e', 'i', 'o', 'u', };
            var consonants = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };

            for (int i = 0; i < RecordsAmount; i++)
            {

                var humanReadablePart = new StringBuilder();
                int randNumber = 0;
                for (int j = 0; j < SymbolsLength; j++)
                {
                    if (j % 2 == 0)
                    {
                        randNumber = _random.Next(vowels.Length);
                        humanReadablePart.Append(vowels[randNumber]);
                    }
                    else
                    {
                        randNumber = _random.Next(consonants.Length);
                        humanReadablePart.Append(consonants[randNumber]);
                    }
                }
                if (records.Contains(humanReadablePart.ToString()))
                    i--;
                else
                    records.Add(firstPart + humanReadablePart.ToString().ToUpper());
            }

            return records;
        }

        public string GetRecordById(int id)
        {
            return Records.ElementAt(id);
        }
    }
}
