using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    [Table("FileDetails")]
    public class FileDetails
    {
        
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        
    }
}
