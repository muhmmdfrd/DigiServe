using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager
{
    public class OrderDetailDeleter : AsistanceBase<OrderDetailAdapter, OrderDetail>
    {
        public OrderDetailDeleter(OrderDetailAdapter manager) : base(manager)
        {
            // code here
        }

        public OrderDetail Delete(long id)
        {
            using (var transac = new TransactionScope())
            {
                var existing = Manager.Query.Value.All();
                var exist = existing.FirstOrDefault(scope => scope.OrderDetailId == id);

                if (exist == null)
                {
                    throw new Exception("Order doesn't exist");
                }
                else
                {
                    Manager.Database.OrderDetails.Remove(exist);
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return exist;
            }
        }
    }
}
