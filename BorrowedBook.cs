﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class BorrowedBook
    {
        public Book Book { get; set; }
        public User User { get; set; }
        public DateTime DueDate { get; set; }
    }
}
