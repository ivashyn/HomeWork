using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanReadableCodeService.Services.Storage
{
    public interface IHumanReadableCodeGenerator
    {
        //List<string> Records { get; set; }
        List<string> GetAllRecords();
        List<string> GenerateRecords();
        string GetRecordById(int id);
        
    }
}
