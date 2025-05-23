﻿using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<Order[]> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Order>().Where(i => i.UserId == userId).ToArrayAsync(cancellationToken: cancellationToken);
        }

        public Task<Order?> GetByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Order>().Where(i => i.PaymentIntentId == paymentIntentId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
