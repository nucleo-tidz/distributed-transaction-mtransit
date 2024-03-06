
A sample project that showcases the use of MassTransit Courier for creating and executing distributed transactions with fault compensation, following the Routing Slip pattern. This project exemplifies how MassTransit Courier can effectively manage the flow of tasks and ensure fault tolerance in distributed transactions by adhering to the Routing Slip pattern


The project has been designed with a typical online shopping flow in mind, incorporating the following microservices:

Order Service: Responsible for creating orders.
Payment Service: Manages payment transactions.
Invoice Service: Handles the creation of invoices.
Initiator Service: Initiates the creation of orders, payments, and invoices. In case of a failure in any of these processes, it triggers compensation mechanisms to revert the transaction and ensure data consistency.
