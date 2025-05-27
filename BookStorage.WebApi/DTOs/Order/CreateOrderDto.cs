namespace BookStorage.WebApi.DTOs.Order
{
	public class CreateOrderDto
	{
		public DateTime OrderDate { get; set; }
		public Guid CustomerId { get; set; }
	}
}
