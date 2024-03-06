using Domain;
using MassTransit;
using domain = Domain;

namespace Invoice
{
    public class CreateInvoiceActivity : IActivity<domain.Invoice, domain.InvoiceLog>
    {
        public async Task<CompensationResult> Compensate(CompensateContext<InvoiceLog> context)
        {
            Console.WriteLine($"Reverting invoice creation {context.Log.OrderId}");
            return await Task.FromResult(context.Compensated());
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<domain.Invoice> context)
        {
           Console.WriteLine($"Invoice creation completed {context.Arguments.OrderId}");
            await Task.CompletedTask;
           // return await Task.FromResult(context.FaultedWithVariables(new Exception(""), new InvoiceLog { OrderId = "OD123" }));
            return await Task.FromResult(context.Completed<InvoiceLog>(new InvoiceLog { OrderId = context.Arguments.OrderId, Status = "Created", Description = "Order has been created" }));
        }
    }
}
