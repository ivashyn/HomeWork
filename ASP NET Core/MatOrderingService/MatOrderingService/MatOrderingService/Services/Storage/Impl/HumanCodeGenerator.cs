using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MatOrderingService.Services.Storage.Impl
{
    public class HumanCodeGenerator : IHumanCodeGenerator
    {
        public string GetCodeByIdLink { get; set; }
        public string GetAllCodesLink { get; set; }
        private readonly HumanCodeGenerator _options;

        public HumanCodeGenerator()
        {

        }

        public HumanCodeGenerator(IOptions<HumanCodeGenerator> options)
        {
            _options = options.Value;

            GetCodeByIdLink = _options.GetCodeByIdLink;
            GetAllCodesLink = _options.GetAllCodesLink;
        }

        public async Task<string> GetHumanCodeByID(int id)
        {
            string getHumanReadableCode = GetCodeByIdLink + id;

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(getHumanReadableCode))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                string result = await content.ReadAsStringAsync();

                if (result != null)
                {
                    return result;
                }
            }

            return string.Empty;

        }
    }
}
