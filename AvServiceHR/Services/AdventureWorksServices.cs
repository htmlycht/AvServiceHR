using AvServiceHR.infrastructure.Models;

using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AvServiceHR.Services
{
    public class AdventureWorksServices
    {

        private readonly AdventureWorks2017Context _advContext;

        string connectionString;

        public AdventureWorksServices(AdventureWorks2017Context context)
        {
            _advContext = context;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = configuration.GetConnectionString("AdventureWorksConnection");

        }


        /// <summary>
        /// IMPLEMENTARE CON DAPPER
        /// restituire tutti i prodotti con quantita' maggiori  100

        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductIdMiniInfo>> GetProductsQtyMaggioreDi100(int locationId)
        {
            
            var connection = new SqlConnection(connectionString);

            string sqlSelect = $@"  
                             SELECT p.ProductID, p.Name, p.ProductNumber, l.Name AS LocationName, SUM(s.Quantity) AS Quantity
                             FROM Production.Product p
                             JOIN Production.TransactionHistory s ON p.ProductID = s.ProductID
                             JOIN Production.Location l ON s.LocationID = l.LocationID
                             WHERE s.Quantity > 100 AND s.LocationID = @LocationID
                             GROUP BY p.ProductID, p.Name, p.ProductNumber, l.Name
                           ";
            var preGiorn = (await connection.QueryAsync<ProductIdMiniInfo>(sqlSelect, new { LocationID = locationId })).ToList();
            return preGiorn;

        }


        /// IMPLEMENTARE CON DAPPER
        /// <summary>
        /// Restituisce tutti i prodotti di quella  determinata locationId
        /// </summary>
        /// 
        /// <returns></returns>
        public async Task<List<ProductIdMiniInfo>> GetProductsQtyLocationId(int locationId)
        {

            var connection = new SqlConnection(connectionString);

            string sqlSelect = $@"  
                           SELECT p.ProductID, p.Name, p.ProductNumber, l.Name AS LocationName, SUM(s.Quantity) AS Quantity
                           FROM Production.Product p
                           JOIN Production.TransactionHistory s ON p.ProductID = s.ProductID
                           JOIN Production.Location l ON s.LocationID = l.LocationID
                           WHERE s.LocationID = @LocationID
                           GROUP BY p.ProductID, p.Name, p.ProductNumber, l.Name
                           ";
            var preGiorn = (await connection.QueryAsync<ProductIdMiniInfo>(sqlSelect, new { LocationID = locationId })).ToList();
            return preGiorn;

        }



        /// IMPLEMENTARE CON EF
        /// </summary>
        /// <returns></returns>
        public async Task<List<Person>> SearchPerson(string firstName, string lastName)
        {
            //_advContext db context

            // LINQ
            return await _advContext.Person
                .Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName))
                .ToListAsync();

        }



        /// IMPLEMENTARE CON EF
        /// </summary>
        /// <returns></returns>
        public async Task<List<Person>> GetPersonTypeSc()
        {
            return await _advContext.Person.Where(w => w.PersonType == "SC").ToListAsync();        

        }


        /// IMPLEMENTARE CON EF
        /// </summary>
        /// <returns></returns>
        public async Task<List<Person>> GetPersonFilter(string nome, string cognome)
        {
            return await _advContext.Person
                .Where(p => p.FirstName.Equals(nome) && p.LastName.Equals(cognome))
                .ToListAsync();

        }

    }
}

public class ProductIdMiniInfo
{
    public int ProductId { get; set; }
    public int Name { get; set; }

    public int ProductNumber { get; set; }

    public string LocationName { get; set; }
    public int Quantity { get; set; }
}
