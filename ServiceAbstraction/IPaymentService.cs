using Shared.Dtos;

namespace ServiceAbstraction;

public interface IPaymentService
{
    Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto request);
    Task<PaymentStatusDto> GetPaymentStatusAsync(string paymentId);
    Task<bool> RefundPaymentAsync(Guid bookingId); 
}