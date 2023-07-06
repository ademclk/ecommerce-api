namespace ECommerceAPI.Application.RequestParameters;

public record Pagination
{
    public int Page { get; init; } = 0;
    public int Size { get; init; } = 5;
}