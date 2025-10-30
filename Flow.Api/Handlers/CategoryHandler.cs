using Flow.Core.Handlers;
using Flow.Core.Models;
using Flow.Core.Requests.Categories;
using Flow.Core.Responses;
using Flow.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Flow.Api.Handlers;

public class CategoryHandler(FinaDbContext dbContext) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description
        };

        try
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return new Response<Category?>(category, 201, "Categoria criada com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Response<Category?>(null, 500, "Não foi possível criar a categoria");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();

            return new Response<Category?>(category, message: "Categoria deletada com sucesso"); // status code 200 by default
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Response<Category?>(null, 500, "Não foi possível deletar a categoria");
        }
    }

    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = dbContext.Categories
                .AsNoTracking()
                .Where(c => c.UserId == request.UserId)
                .OrderBy(c => c.Title);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>?>(
                categories,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new PagedResponse<List<Category>?>(null, 500, "Não foi possível obter as categorias");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            // use asnotracking for read-only queries
            var category = await dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            return category is not null
                ? new Response<Category?>(category) // status code 200 by default
                : new Response<Category?>(null, 404, "Categoria não encontrada");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Response<Category?>(null, 500, "Não foi possível obter a categoria");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            category.Title = request.Title;
            category.Description = request.Description;

            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();

            return new Response<Category?>(category, message: "Categoria atualizada com sucesso"); // status code 200 by default
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Response<Category?>(null, 500, "Não foi possível alterar a categoria");
        }
    }
}
