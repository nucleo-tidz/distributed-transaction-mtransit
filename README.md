A sample project that showcases the use of [MassTransit ](https://masstransit.io/)Courier for creating and executing distributed transactions with fault compensation, following the Routing Slip pattern. This project exemplifies how MassTransit Courier can effectively manage the flow of tasks and ensure fault tolerance in distributed transactions by adhering to the Routing Slip pattern


The project has been designed with a typical online shopping flow in mind, incorporating the following microservices:

1. **Order Service**: Responsible for creating orders. 
2. **Payment Service**: Manages payment transactions. 
3. **Invoice Service**: Handles the creation of invoices. 
4. **Initiator Service**: Initiates the creation of orders, payments, and invoices. In case of a failure in any of these processes, it triggers compensation mechanisms to revert the transaction and ensure data consistency

_To run the project , spin a rabbit mq server on your machine using below command_ 
`docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management`
