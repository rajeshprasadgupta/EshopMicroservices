
using Catalog.API.Products.DeleteProduct;

namespace Catalog.API.Products.CreateProduct
{
	//Represents the ProductCommand data to create the product
	//Inherits ICommand interface with CreateProductResult as the result
	public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
		: ICommand<CreateProductResult>;

	//Represents the ProductCommand result after creating the product
	public record CreateProductResult(Guid Id);

	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
	}

	//Responsible for handling the CQRS Command for creating the product
	//In order to trigger the handler, we need a mediator for abstraction of the Command and Query, so it implements IRequestHandler
	internal class CreateProductCommandHandler(IDocumentSession session)
		: ICommandHandler<CreateProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
		{
			//create Product from command
			var product = new Product
			{
				Name = command.Name,
				Category = command.Category,
				Description = command.Description,
				ImageFile = command.ImageFile,
				Price = command.Price
			};
			//save to db
			session.Store(product);
			await session.SaveChangesAsync(cancellationToken);
			//return result
			return new CreateProductResult(product.Id);
		}
	}
}
