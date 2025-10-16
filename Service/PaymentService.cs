using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class PaymentService : IPaymentService
{
    public Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentStatusDto> GetPaymentStatusAsync(string paymentId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RefundPaymentAsync(Guid bookingId)
    {
        throw new NotImplementedException();
    }
}