using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace sem7Jaramillo
{
    public interface Database
    {
        SQLiteAsyncConnection GetConnection();
    }
}
