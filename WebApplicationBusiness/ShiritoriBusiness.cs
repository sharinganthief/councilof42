using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplicationData.Shiritori;

namespace WebApplicationBusiness
{
    public class ShiritoriBusiness
    {
        private ShiritoriRepository repo;

        public ShiritoriBusiness()
        {
            this.repo = new ShiritoriRepository();
        }
        public List<string> GetFourLetterList(string baseString)
        {
            if (string.IsNullOrWhiteSpace(baseString) || baseString.Length < 4) return new List<string>();

            Regex regex = new Regex(string.Format("^{0}[a-zA-Z]*", baseString));
            List<string> words = repo.SearchDict(regex);
            return words;
        }
    }
}
