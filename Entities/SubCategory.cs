﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SubCategory
    {
        public int Value { get; set; }
        public string? Label { get; set; }
        public CategoryEntity? Category { get; set; }

    }
}
