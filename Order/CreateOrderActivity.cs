using Domain;
using MassTransit;
using domain = Domain;

namespace Order
{
    public class CreateOrderActivity : IActivity<domain.Order, domain.OrderLog>
    {
        public async Task<CompensationResult> Compensate(CompensateContext<OrderLog> context)
        {
            Console.WriteLine($"Reverting order creation {context.Log.OrderId}");
            return await Task.FromResult(context.Compensated());
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<domain.Order> context)
        {
            Console.WriteLine($"Order created for {context.Arguments.OrderId}");
            await Task.CompletedTask;
            return await Task.FromResult(context.Completed<OrderLog>(new OrderLog { OrderId = context.Arguments.OrderId, Status = "Created", Description = "Order has been created" }));
        }
    }
}
