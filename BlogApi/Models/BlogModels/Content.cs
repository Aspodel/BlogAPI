using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models.BlogModels
{
    public class Content
    {
        public int ContentId { get; set; }

        //One to Many relationship
        public ICollection<Paragraph> Paragraphs { get; set; }
    }
}
