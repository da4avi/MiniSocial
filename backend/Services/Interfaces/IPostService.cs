using MiniSocial.DTOs.Post;

namespace MiniSocial.Services.Interfaces;

public interface IPostService
{
    public Task<List<PostResponseDto>> GetPosts();
    public Task<List<PostResponseDto>> GetMyPosts(string userId);
    public Task<LikeResponseDto> LikePost(LikeRequestDto likeRequest, string userId);
    public Task<PostResponseDto> PostPost(PostRequestDto postRequest, string userId);

}