using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using insurance_partners_management_system.Models;
using insurance_partners_management_system.Repository.Interface;

namespace insurance_partners_management_system.Repository.Implementation
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly IDbConnection _connection;

        public PartnerRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Partner> GetAll()
        {
            string sql = @"
                SELECT 
                    IdPartner, 
                    CONCAT(FirstName, ' ', LastName) AS FullName, 
                    IsForeign, 
                    PartnerTypeId, 
                    PartnerNumber, 
                    CreatedAtUtc 
                FROM Partner
                ORDER BY CreatedAtUtc DESC";

            return _connection.Query<Partner>(sql).ToList();
        }

        public Partner GetById(int id)
        {
            string sql = "SELECT * FROM Partner WHERE IdPartner = @Id";
            return _connection.QuerySingleOrDefault<Partner>(sql, new { Id = id });
        }

        public void Add(Partner partner)
        {
            string sql = @"
                INSERT INTO Partner (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreatedAtUtc, CreateByUser, IsForeign, ExternalCode, Gender)
                VALUES (@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPin, @PartnerTypeId, @CreatedAtUtc, @CreateByUser, @IsForeign, @ExternalCode, @Gender)";
            _connection.Execute(sql, partner);
        }

        public void Update(Partner partner)
        {
            string sql = @"
                UPDATE Partner
                SET 
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                    Address = @Address, 
                    PartnerNumber = @PartnerNumber, 
                    CroatianPIN = @CroatianPin, 
                    PartnerTypeId = @PartnerTypeId, 
                    CreateByUser = @CreateByUser, 
                    IsForeign = @IsForeign, 
                    ExternalCode = @ExternalCode, 
                    Gender = @Gender
                WHERE IdPartner = @IdPartner";
            _connection.Execute(sql, partner);
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Partner WHERE IdPartner = @Id";
            _connection.Execute(sql, new { Id = id });
        }
    }
}