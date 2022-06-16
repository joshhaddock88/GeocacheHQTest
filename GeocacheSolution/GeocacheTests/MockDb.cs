using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeocacheSolution.Data;
using GeocacheSolution.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GeocacheTests
{
    public class MockDb : IDisposable
    {
        public readonly SqliteConnection _connection;
        public readonly GeocacheContext _context;

        public MockDb()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _context = new GeocacheContext(
                new DbContextOptionsBuilder<GeocacheContext>()
                    .UseSqlite(_connection)
                    .Options);

            _context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _context.Dispose();
            _connection?.Dispose();
        }
    }
}
