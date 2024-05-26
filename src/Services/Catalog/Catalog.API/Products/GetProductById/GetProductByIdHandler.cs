
namespace Catalog.API.Products.GetProductById;

public record GetProductsByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductsByIdQuery> logger) 
    : IRequestHandler<GetProductsByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if(product is null)
            throw new ProductNotFoundException(query.Id);

        return new GetProductByIdResult(product);
    }
}
