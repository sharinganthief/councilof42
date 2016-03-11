using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplicationData.Shiritori
{
    public class ShiritoriRepository
    {
        public List<string> SearchDict(Regex regex)
        {
            List<string> matchingWords = new List<string>();
            string comDictPath = string.Format("{0}\\content\\resources\\shiritori\\eng_words_final.dic",
                AppDomain.CurrentDomain.BaseDirectory);

            using (StreamReader r = new StreamReader(comDictPath))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Match m = regex.Match(line);
                    if (m.Success)
                    {
                        matchingWords.Add(line);
                    }
                }
            }

            return matchingWords;
        }
    }
}
