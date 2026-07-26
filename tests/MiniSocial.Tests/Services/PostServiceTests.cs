using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;
using MiniSocial.DTOs.Post;
using MiniSocial.Models;
using MiniSocial.Services;
using Shouldly;

namespace MiniSocial.Tests.Services;

public class PostServiceTests
{
    [Fact]
    public async Task LikePost_IfUserDidNotLiked_ShouldAddLike()
    {
        //arrange (preparaçao)
        //criar as options pra um db in memory
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        //instancia o db
        using var context = new AppDbContext(options);

        //instancia o service
        var service = new PostService(context);

        //dados q eu uso no metodo
        var userId = new Guid("019f9a69-f34a-7013-8ff2-899013d864bf");
        var likeRequest = new LikeRequestDto { PostId = new Guid("019f9a6b-abc7-7ad1-a7ed-7334d67e08c7") };

        //cria o post e o perfil (com lista de likes vazia)
        context.Posts.Add(new Post("a", "a", 0, userId.ToString())
        {
            Id = likeRequest.PostId
        });
        context.Profiles.Add(new Profile("a", "a", [], userId.ToString())
        {
            Id = userId,
        });
        await context.SaveChangesAsync();

        //act (execuçao)
        //invoca o metodo passando os dados
        await service.LikePost(likeRequest, userId.ToString());

        //assert (validaçao)
        //1ª validaçao - usuario existe
        var userProfile = await context.Profiles.
        AsNoTracking().
        FirstOrDefaultAsync(p => p.Id == userId);
        userProfile.ShouldNotBeNull();

        //2ª validaçao - se o post foi adicionado nas curtidas
        userProfile.LikedPosts.ShouldContain(likeRequest.PostId);
    }

    [Fact]
    public async Task LikePost_IfUserAlreadyLiked_ShouldRemoveLike()
    {
        //arrange (preparaçao)
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new AppDbContext(options);
        var service = new PostService(context);

        var userId = new Guid("019f9a69-f34a-7013-8ff2-899013d864bf");
        var likeRequest = new LikeRequestDto { PostId = new Guid("019f9a6b-abc7-7ad1-a7ed-7334d67e08c7") };

        //cria o post e o perfil (com o post ja curtido)
        context.Posts.Add(new Post("a", "a", 0, userId.ToString())
        {
            Id = likeRequest.PostId
        });
        context.Profiles.Add(new Profile("a", "a", [likeRequest.PostId], userId.ToString())
        {
            Id = userId,
        });
        await context.SaveChangesAsync();

        //act (execuçao)
        await service.LikePost(likeRequest, userId.ToString());

        //assert (validaçao)
        //1ª validaçao - usuario existe
        var userProfile = await context.Profiles.
        AsNoTracking().
        FirstOrDefaultAsync(p => p.Id == userId);
        userProfile.ShouldNotBeNull();

        //2ª validaçao - se o post foi removido das curtidas
        userProfile.LikedPosts.ShouldNotContain(likeRequest.PostId);
    }
}
