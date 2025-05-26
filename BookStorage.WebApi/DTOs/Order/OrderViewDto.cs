namespace BookStorage.WebApi.DTOs.Order
{
	public class OrderViewDto
	{
		public Guid Id { get; set; }
		public DateTime OrderDate { get; set; }
		public Guid CustomerId { get; set; }
	}
}
