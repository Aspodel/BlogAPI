﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models.BlogModels
{
    public class Paragraph
    {
        public int ParagraphId { get; set; }
        public string Type { get; set; }
        public string ParagraphContent { get; set; }

        //One to Many relationship
        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}
