using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.OrderManager
{
    public class OrderDeleter : AsistanceBase<OrderAdapter, Order>
    {
        public OrderDeleter(OrderAdapter manager) : base(manager)
        {
            // code here
        }

        public string Delete(long id)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.Get().FirstOrDefault(scope => scope.OrderId == id);

                if (exist == null)
                {
                    throw new Exception("Order doesn't exist");
                }
                else
                {
                    Manager.Database.Orders.Remove(exist);
                    Manager.Database.SaveChanges();
                }

                transac.Complete();
                
                return "deleted";
            }
        }
    }
}
