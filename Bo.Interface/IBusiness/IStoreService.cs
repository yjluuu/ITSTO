﻿using Routine.Models.ApiEntityRequest;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface IStoreService
    {
        Store GetSores(RequestStore requestStore);

    }
}
