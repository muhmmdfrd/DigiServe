using Repository1;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.OrderPaymentManager
{
    public class OrderPaymentDeleter : AsistanceBase<OrderPaymentAdapter, OrderPayment>
    {
        public OrderPaymentDeleter(OrderPaymentAdapter manager) : base(manager)
        {
            // code here
        }

        public string Delete(long id)
        {
            using (var transac = new TransactionScope())
            {
                var exist = Manager.Query.Value.All().FirstOrDefault(scope => scope.OrderPaymentId == id);

                if (exist == null)
                {
                    throw new Exception("Order doesn't exist");
                }
                else
                {
                    Manager.Database.OrderPayments.Remove(exist);
                    Manager.Database.SaveChanges();
                }

                transac.Complete();

                return "deleted";
            }
        }
    }
}
