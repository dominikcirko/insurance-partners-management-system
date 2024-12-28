using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using insurance_partners_management_system.Models;
using insurance_partners_management_system.Repository.Interface;

namespace insurance_partners_management_system.Repository.Implementation
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly IDbConnection _connection;

        public PolicyRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Policy> GetAll()
        {
            string sql = "SELECT * FROM Policy";
            return _connection.Query<Policy>(sql).ToList();
        }

        public Policy GetById(int id)
        {
            string sql = "SELECT * FROM Policy WHERE IdPolicy = @Id";
            return _connection.QuerySingleOrDefault<Policy>(sql, new { Id = id });
        }

        public void Add(Policy entity)
        {
            string sql = @"
                INSERT INTO Policy (PolicyNumber, PolicyAmount, PartnerId)
                VALUES (@PolicyNumber, @PolicyAmount, @PartnerId)";
            _connection.Execute(sql, entity);
        }

        public void Update(Policy entity)
        {
            string sql = @"
                UPDATE Policy
                SET 
                    PolicyNumber = @PolicyNumber, 
                    PolicyAmount = @PolicyAmount,
                    PartnerId = @PartnerId
                WHERE IdPolicy = @IdPolicy";
            _connection.Execute(sql, entity);
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Policy WHERE IdPolicy = @Id";
            _connection.Execute(sql, new { Id = id });
        }
    }
}
