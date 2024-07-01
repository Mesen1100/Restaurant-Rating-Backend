using CleanArchitecture.Core.Wrappers;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods;
using CleanArchitecture.Infrastructure.Repositories;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetFoodById
{
    public class GetFoodByIdQuery:IRequest<Response<GetAllFoodsViewModel>>
    {
        public int Id { get; set; }
}
public class GetFoodByIdQueryHandler : IRequestHandler<GetFoodByIdQuery, Response<GetAllFoodsViewModel>>
{
    private readonly IFoodRepositoryAsync _foodRepositoryAsync;

    public GetFoodByIdQueryHandler(IFoodRepositoryAsync foodRepositoryAsync)
    {
        _foodRepositoryAsync = foodRepositoryAsync;
    }

    public async Task<Response<GetAllFoodsViewModel>> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
    {
        return await _foodRepositoryAsync.GetFoodById(request.Id);
    }
}
}
