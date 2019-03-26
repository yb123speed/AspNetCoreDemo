using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResolveConcurrencyCconflictsWithEFCore.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishTime { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
