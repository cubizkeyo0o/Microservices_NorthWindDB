using MediatR;
using SalePayment.Application.Response;
using SalePayment.Domain.Entities;

namespace SalePayment.Application.Queries
{
    public class GetInvoicesQuery :IRequest<InvoiceResponse>
    {
        public int Id { get; set; }
        public GetInvoicesQuery(int id)
        {
            Id = id;
        }
    }
}
