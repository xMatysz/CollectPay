namespace CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;

public record UpdateBillInfo(string Name, string[] EmailsToAdd, string[] EmailsToRemove);