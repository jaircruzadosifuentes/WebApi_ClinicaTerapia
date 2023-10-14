﻿using Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServer: IUnitOfWork
    {
        public IUnitOfWorkAdapter Create()
        {
            var connectionString = Parameters.ConnectionString;
            return new UnitOfWorkSqlServerAdapter(connectionString);
        }
    }
}
