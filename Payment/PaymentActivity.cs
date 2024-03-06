using Domain;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = Domain;

namespace Payment
{
    public class PaymentActivity : IActivity<domain.Payment, domain.PaymentLog>
    {
        public async Task<CompensationResult> Compensate(CompensateContext<PaymentLog> context)
        {
             Console.WriteLine( $"Reverting payment for order {context.Log.OrderId}"  );
            return await Task.FromResult(context.Compensated());
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<domain.Payment> context)
        {
            Console.WriteLine(context.Arguments.OrderId);
            Console.WriteLine($"Payment processed for {context.Arguments.OrderId}");
            // return await Task.FromResult( context.FaultedWithVariables(new Exception(""),new PaymentLog { OrderId="OD123"}));
            return context.Completed(new domain.PaymentLog { OrderId = context.Arguments.OrderId, PaymentId = "PAY100", Status = "Created", Description = "Order has been created" });
        }
    }
}
