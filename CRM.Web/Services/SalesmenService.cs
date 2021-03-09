﻿using CRM.Core.Entities;
using CRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Web.Services
{
    public class SalesmenService : ISalesmenService
    {
        private readonly AppDbContext _context;

        public SalesmenService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Salesman>> GetSalesmen(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            var appDbContext = _context.Salesmen.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                appDbContext = appDbContext.Where(s => s.FirstName.Contains(searchString)
                                                    || s.LastName.Contains(searchString)
                                                    || s.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "first_name_desc":
                    appDbContext = appDbContext.OrderByDescending(s => s.FirstName);
                    break;
                case "LastName":
                    appDbContext = appDbContext.OrderBy(s => s.LastName);
                    break;
                case "last_name_desc":
                    appDbContext = appDbContext.OrderByDescending(s => s.LastName);
                    break;
                case "Phone":
                    appDbContext = appDbContext.OrderBy(s => s.Phone);
                    break;
                case "phone_desc":
                    appDbContext = appDbContext.OrderByDescending(s => s.Phone);
                    break;
                case "Email":
                    appDbContext = appDbContext.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    appDbContext = appDbContext.OrderByDescending(s => s.Email);
                    break;
                default:
                    appDbContext = appDbContext.OrderBy(s => s.FirstName);
                    break;
            }

            int pageSize = 10;

            return await PaginatedList<Salesman>.CreateAsync(appDbContext.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        public async Task<Salesman> GetSalesmanlById(int? id)
        {
            return await _context.Salesmen
                .Where(s => s.Id == id)
                .Include(s => s.Activities)
                .Include(s => s.Deals)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateSalesman(Salesman salesman)
        {
            salesman.CreatedDate = DateTime.Now;

            await _context.Salesmen.AddAsync(salesman);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<bool> UpdateSalesman(Salesman salesman)
        {
            salesman.UpdatedDate = DateTime.Now;

            _context.Update(salesman);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<bool> DeleteSalesman(int? id)
        {
            var salesman = await _context.Salesmen.FindAsync(id);

            _context.Salesmen.Remove(salesman);

            var deleteResult = await _context.SaveChangesAsync();

            return deleteResult == 1;
        }

        public async Task<bool> SalesmanExists(int id)
        {
            return await _context.Salesmen.AnyAsync(d => d.Id == id);
        }
    }
}