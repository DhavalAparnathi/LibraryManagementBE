﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Provider.WorkContext
{
    public interface IWorkContext
    {
        int CurrentUserId { get; }
    }
}
