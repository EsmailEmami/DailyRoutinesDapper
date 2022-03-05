using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.DTOs.Routine;
using DailyRoutines.Domain.Entities.Routine;
using DailyRoutines.Domain.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DailyRoutines.Application.Extensions;
using Action = DailyRoutines.Domain.Entities.Routine.Action;

namespace DailyRoutines.Infrastructure.Repositories;

public class RoutinesRepository : IRoutineRepository
{
    private readonly IDbConnection _db;

    public RoutinesRepository(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DailyRoutinesDbConnection"));
    }

    public List<CategoriesListDTO> GetUserCategories(Guid userId, int skip, int take, string orderBy, string filter)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        parameters.Add("@Skip", skip);
        parameters.Add("@Take", take);
        parameters.Add("@Search", filter);
        parameters.Add("@OrderBy", orderBy.Fixed());

        return _db.Query<CategoriesListDTO>("[User].[uspGetUserCategories]", parameters,
            commandType: CommandType.StoredProcedure).ToList();
    }

    public List<CategoriesListDTO> GetUserRecycleCategories(Guid userId, int skip, int take, string orderBy, string filter)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        parameters.Add("@Skip", skip);
        parameters.Add("@Take", take);
        parameters.Add("@Search", filter);
        parameters.Add("@OrderBy", orderBy);

        return _db.Query<CategoriesListDTO>("[User].[uspGetUserRecycleCategories]", parameters,
            commandType: CommandType.StoredProcedure).ToList();
    }

    public EditCategoryDTO GetCategoryForEdit(Guid categoryId)
    {
        string query = "SELECT [CategoryId],[CategoryTitle] " +
                       "FROM [User].[Categories] " +
                       "WHERE [CategoryId] = @CategoryId;";

        return _db.QuerySingleOrDefault<EditCategoryDTO>(query, new
        {
            categoryId
        });
    }

    public Category GetCategoryById(Guid categoryId)
    {
        string query = "SELECT [CategoryId],[CategoryTitle],[UserId],[CreateDate],[LastUpdateDate],[IsDelete] " +
                       "FROM [User].[Categories] " +
                       "WHERE [CategoryId] = @CategoryId;";

        return _db.QuerySingleOrDefault<Category>(query, new
        {
            categoryId
        });
    }

    public void DeleteCategory(Guid categoryId)
    {
        string query = "DELETE FROM [User].[Categories] " +
                       "WHERE [CategoryId] = @CategoryId;";

        _db.Execute(query, new
        {
            categoryId
        });
    }

    public Category AddCategory(Category category)
    {
        string query = "INSERT INTO [User].[Categories] ([CategoryTitle],[UserId],[IsDelete],[CreateDate],[LastUpdateDate]) " +
                       "OUTPUT CAST([Inserted].[CategoryId] AS UNIQUEIDENTIFIER) AS [CategoryId]" +
                       "VALUES (@CategoryTitle,@UserId,@IsDelete,@CreateDate,@LastUpdateDate)";


        var categoryId = _db.QuerySingle<Guid>(query, new
        {
            category.CategoryTitle,
            category.UserId,
            category.IsDelete,
            category.CreateDate,
            category.LastUpdateDate
        });

        category.CategoryId = categoryId;

        return category;
    }

    public void UpdateCategory(Category category)
    {
        string query =
            "UPDATE [User].[Categories] SET " +
            "[CategoryTitle] = @CategoryTitle, " +
            "[LastUpdateDate] = @LastUpdateDate, " +
            "[IsDelete] = @IsDelete " +
            "WHERE [CategoryId] = @CategoryId;";

        _db.Execute(query, new
        {
            category.CategoryTitle,
            category.LastUpdateDate,
            category.IsDelete,
            category.CategoryId
        });
    }

    public List<ItemsForSelectDTO> GetUserCategoriesForSelect(Guid userId)
    {
        string query =
            "SELECT [CategoryId] AS [Value], [CategoryTitle] AS [Name] " +
            "FROM [User].[Categories] " +
            "WHERE [UserId] = @UserId";

        return _db.Query<ItemsForSelectDTO>(query, new
        {
            userId
        }).ToList();
    }

    public Guid GetUserIdOfCategory(Guid categoryId)
    {
        string query =
            "SELECT [UserId] " +
            "FROM [User].[Categories] " +
            "WHERE [CategoryId] = @CategoryId";

        return _db.QuerySingleOrDefault<Guid>(query, new
        {
            categoryId
        });
    }

    public int GetCategoriesCount(Guid userId, string type, string filter)
    {
        string query = "SELECT COUNT(*) FROM [User].[Categories] " +
                       "WHERE ([UserId] = @UserId) ";

        query += type switch
        {
            "active" => "AND ([IsDelete] = 0)",
            "recycle" => "AND ([IsDelete] = 1)",
            _ => "AND ([IsDelete] = 0)"
        };

        if (!string.IsNullOrEmpty(filter))
        {
            query += " AND ([CategoryTitle] LIKE N'%@Search%')";
        }

        return _db.QuerySingleOrDefault<int>(query, new
        {
            userId,
            @Search = filter
        });
    }

    public List<ActionsListDTO> GetLastUserActions(Guid userId, int skip, int take, string filter, int year, int month, int day)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        parameters.Add("@Skip", skip);
        parameters.Add("@Take", take);
        parameters.Add("@Search", filter);
        parameters.Add("@Year", year);
        parameters.Add("@Month", month);
        parameters.Add("@Day", day);

        return _db.Query<ActionsListDTO>("[User].[uspGetUserActions]", parameters,
            commandType: CommandType.StoredProcedure).ToList();
    }

    public List<ActionsListDTO> GetActionsOfCategory(Guid categoryId, int skip, int take, string filter, int year, int month, int day)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CategoryId", categoryId);
        parameters.Add("@Skip", skip);
        parameters.Add("@Take", take);
        parameters.Add("@Search", filter);
        parameters.Add("@Year", year);
        parameters.Add("@Month", month);
        parameters.Add("@Day", day);

        return _db.Query<ActionsListDTO>("[User].[uspGetCategoryActions]", parameters,
            commandType: CommandType.StoredProcedure).ToList();
    }

    public List<DatesOfCategoryActionsDTO> GetActionsMonthOfCategory(Guid categoryId, int year)
    {
        string query =
            "SELECT [CreatePersianMonth] AS [Value],COUNT(*) AS [ActionsCount] " +
            "FROM [User].[Actions] " +
            "WHERE ([CategoryId] = @CategoryId) AND ([CreatePersianYear] = @Year)" +
            "GROUP BY [CreatePersianMonth];";

        return _db.Query<DatesOfCategoryActionsDTO>(query, new
        {
            categoryId,
            year
        }).ToList();
    }

    public int GetUserActionsCount(Guid userId, int year, int month, int day, string filter)
    {
        string query = "SELECT COUNT(*) FROM [User].[Actions] " +
                       "INNER JOIN[User].[Categories] " +
                       "ON[User].[Actions].[CategoryId] = [User].[Categories].[CategoryId] " +
                       "WHERE([Categories].[UserId] = @UserId)";

        if (year > 0)
        {
            query += "  AND ([Actions].[CreatePersianYear] = @Year)";


            if (month > 0)
            {
                query += "  AND ([Actions].[CreatePersianMonth] = @Month)";

                if (day > 0)
                {
                    query += "  AND ([Actions].[CreatePersianDay] = @Day)";
                }
            }
        }

        if (!string.IsNullOrEmpty(filter))
        {
            query += " AND ([Actions].[ActionTitle] LIKE N'%@Search%') OR " +
                     "([Actions].[ActionDescription] LIKE N'%@Search%')";
        }

        return _db.QuerySingleOrDefault<int>(query, new
        {
            userId,
            year,
            month,
            day,
            @Search = filter
        });
    }

    public int GetCategoryActionsCount(Guid categoryId, int year, int month, int day, string filter)
    {
        string query = "SELECT COUNT(*) FROM [User].[Actions] " +
                       "WHERE([CategoryId] = @CategoryId)";

        if (year > 0)
        {
            query += "  AND ([CreatePersianYear] = @Year)";


            if (month > 0)
            {
                query += "  AND ([CreatePersianMonth] = @Month)";

                if (day > 0)
                {
                    query += "  AND ([CreatePersianDay] = @Day)";
                }
            }
        }

        if (!string.IsNullOrEmpty(filter))
        {
            query += " AND ([ActionTitle] LIKE N'%@Search%') OR " +
                     "([ActionDescription] LIKE N'%@Search%')";
        }

        return _db.QuerySingleOrDefault<int>(query, new
        {
            categoryId,
            year,
            month,
            day,
            @Search = filter
        });
    }

    public List<int> GetYearsOfCategoryActions(Guid categoryId)
    {
        string query =
            "SELECT DISTINCT [CreatePersianYear] " +
            "FROM [User].[Actions] " +
            "WHERE [CategoryId] = @CategoryId;";

        return _db.Query<int>(query, new
        {
            categoryId,
        }).ToList();
    }

    public List<int> GetYearsOfActions(Guid userId)
    {
        string query =
            "SELECT DISTINCT [CreatePersianYear] " +
            "FROM [User].[Actions] " +
            "INNER JOIN [User].[Categories] " +
            "ON [User].[Actions].[CategoryId] = [User].[Categories].[CategoryId] " +
            "WHERE [User].[Categories].[UserId] = @UserId;";

        return _db.Query<int>(query, new
        {
            userId,
        }).ToList();
    }


    public ActionDetailDTO GetActionDetail(Guid actionId)
    {
        string query = "SELECT [ActionId],[ActionTitle],[ActionDescription],[CreateDate],[LastUpdateDate] " +
                       "FROM[User].[Actions] " +
                       "WHERE[ActionId] = @ActionId;";


        return _db.QuerySingleOrDefault<ActionDetailDTO>(query, new
        {
            actionId,
        });
    }

    public EditActionDTO GetActionForEdit(Guid actionId)
    {
        string query = "SELECT [ActionId],[ActionTitle],[ActionDescription] " +
                       "FROM[User].[Actions] " +
                       "WHERE[ActionId] = @ActionId;";


        return _db.QuerySingleOrDefault<EditActionDTO>(query, new
        {
            actionId,
        });
    }

    public Action AddAction(Action action)
    {
        string query =
            "INSERT INTO [User].[Actions] (" +
            "[CategoryId]," +
            "[ActionTitle]," +
            "[ActionDescription]," +
            "[CreatePersianYear]," +
            "[CreatePersianMonth]," +
            "[CreatePersianDay]," +
            "[CreateDate]," +
            "[LastUpdateDate])" +
            "OUTPUT CAST([Inserted].[ActionId] AS UNIQUEIDENTIFIER) AS [ActionId]" +
            "VALUES (@CategoryId," +
            "@ActionTitle," +
            "@ActionDescription," +
            "@CreatePersianYear," +
            "@CreatePersianMonth," +
            "@CreatePersianDay," +
            "@CreateDate," +
            "@LastUpdateDate);";


        var actionId = _db.QuerySingle<Guid>(query, new
        {
            action.CategoryId,
            action.ActionTitle,
            action.ActionDescription,
            action.CreatePersianYear,
            action.CreatePersianMonth,
            action.CreatePersianDay,
            action.CreateDate,
            action.LastUpdateDate
        });

        action.ActionId = actionId;

        return action;
    }

    public void UpdateAction(Action action)
    {
        string query =
            "UPDATE [User].[Actions] SET" +
            "[ActionTitle] = @ActionTitle, " +
            "[ActionDescription] = @ActionDescription, " +
            "[LastUpdateDate] = @LastUpdateDate " +
            "WHERE [ActionId] = @ActionId";


        _db.Execute(query, new
        {
            action.ActionTitle,
            action.ActionDescription,
            action.LastUpdateDate,
            action.ActionId
        });
    }

    public void DeleteAction(Guid actionId)
    {
        string query = "DELETE FROM [User].[Actions] " +
                       "WHERE [ActionId] = @ActionId;";

        _db.Execute(query, new
        {
            actionId
        });
    }

    public Action GetActionById(Guid actionId)
    {
        string query = "SELECT [ActionId],[CategoryId],[ActionTitle],[ActionDescription]," +
                       "[CreatePersianYear],[CreatePersianMonth],[CreatePersianDay]," +
                       "[CreateDate],[LastUpdateDate] " +
                       "FROM[User].[Actions] " +
                       "WHERE[ActionId] = @ActionId;";


        return _db.QuerySingleOrDefault<Action>(query, new
        {
            actionId,
        });
    }

    public bool IsCategoryExist(Guid categoryId)
    {
        string query = "SELECT (CASE WHEN EXISTS(" +
                       "SELECT NULL " +
                       "FROM [User].[Categories] " +
                       "WHERE [CategoryId] = @CategoryId) " +
                       "THEN 1 ELSE 0 END) AS[Value]";

        return _db.QuerySingleOrDefault<bool>(query, new
        {
            categoryId
        });
    }

    public bool IsUserCategoryExist(Guid userId, Guid categoryId)
    {
        string query = "SELECT (CASE WHEN EXISTS(" +
                       "SELECT NULL " +
                       "FROM [User].[Categories] " +
                       "WHERE ([CategoryId] = @CategoryId) AND ([UserId] = @UserId)) " +
                       "THEN 1 ELSE 0 END) AS[Value]";

        return _db.QuerySingleOrDefault<bool>(query, new
        {
            categoryId,
            userId
        });
    }

    public bool IsUserActionExist(Guid userId, Guid actionId)
    {
        string query = "SELECT (CASE WHEN EXISTS(" +
                       "SELECT NULL " +
                       "FROM [User].[Actions] " +
                       "INNER JOIN [User].[Categories]" +
                       "ON [User].[Actions].[CategoryId] = [User].[Categories].[CategoryId]" +
                       "WHERE ([User].[Actions].[ActionId] = @ActionId) AND ([User].[Categories].[UserId] = @UserId)) " +
                       "THEN 1 ELSE 0 END) AS[Value]";

        return _db.QuerySingleOrDefault<bool>(query, new
        {
            actionId,
            userId
        });
    }

    public CategoryDetailDTO GetCategoryDetail(Guid categoryId)
    {
        string query = "SELECT " +
                       "[Categories].[CategoryId], " +
                       "[Categories].[CategoryTitle], " +
                       "dbo.PersianDateTime([Categories].[LastUpdateDate]) AS [LastUpdate], " +
                       "(SELECT COUNT(*) " +
                       "FROM [User].[Actions] " +
                       "WHERE [CategoryId] = @CategoryId) AS [ActionsCount] " +
                       "FROM [User].[Categories] " +
                       "WHERE [User].[Categories].[CategoryId] = @CategoryId;";


        return _db.QuerySingleOrDefault<CategoryDetailDTO>(query, new
        {
            categoryId
        });
    }

    public CategoryDetailForAdminDTO GetCategoryDetailForAdmin(Guid categoryId)
    {
        string query = "SELECT [Users].[UserId], " +
                       "CONCAT([Users].[FirstName], ' ', [Users].[LastName]) AS [FullName], " +
                       "[Categories].[CategoryId], " +
                       "[Categories].[CategoryTitle], " +
                       "dbo.PersianDateTime([Categories].[LastUpdateDate]) AS [LastUpdate], " +
                       "[Categories].[IsDelete], " +
                       "(SELECT COUNT(*) " +
                       "FROM [User].[Actions] " +
                       "WHERE [CategoryId] = @CategoryId) AS [ActionsCount] " +
                       "FROM [User].[Categories] " +
                       "INNER JOIN [User].[Users] " +
                       "ON [User].[Categories].[UserId] = [User].[Users].[UserId] " +
                       "WHERE [User].[Categories].[CategoryId] = @CategoryId;";


        return _db.QuerySingleOrDefault<CategoryDetailForAdminDTO>(query, new
        {
            categoryId
        });
    }

    public List<Action> GetActionsOfCategory(Guid categoryId)
    {
        string query = "SELECT [ActionId] " +
                       ",[CategoryId] " +
                       ",[ActionTitle] " +
                       ",[ActionDescription] " +
                       ",[CreatePersianYear] " +
                       ",[CreatePersianMonth] " +
                       ",[CreatePersianDay] " +
                       ",[CreateDate] " +
                       ",[LastUpdateDate] " +
                       "FROM [User].[Actions] " +
                       "WHERE [CategoryId] = @CategoryId;";

        return _db.Query<Action>(query, new
        {
            categoryId
        }).ToList();
    }
}
