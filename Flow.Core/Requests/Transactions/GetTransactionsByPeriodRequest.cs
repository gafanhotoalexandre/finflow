namespace Flow.Core.Requests.Transactions;

public class GetTransactionsByPeriodRequest : PagedRequest
{
    // Se nenhuma data for passada, traz todas as transações do mês atual
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
