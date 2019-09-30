﻿using EasyHttpClient.Attributes;
using System.Threading.Tasks;
using PixivApi.Model.Response;
using PixivApi.Model;
using EasyHttpClient;
using PixivApi.API.Attributes;
using System.IO;

namespace PixivApi.Api
{
    //refer to https://github.com/upbit/pixivpy/wiki

    public interface IPixivApiClient
    {
        [HttpGet]
        [Route("")]
        Task<IHttpResult<Stream>> Download([Uri]string uri);

        [HttpGet]
        [Authorize]
        [Route("v1/search/illust")]
        Task<IllustsListingResponse> SearchIllust(string word, [EnumCastString] Sort sort = Sort.date_desc, [EnumCastString]SearchTag search_target = SearchTag.partial_match_for_tags, string filter = "for_ios", int? offset = null);

        [HttpGet]
        [Authorize]
        [Route("v2/illust/related")]
        Task<IllustsListingResponse> IllustRelated(int illust_id, string filter = "for_ios");

        [HttpGet]
        [Authorize]
        [Route("v2/illust/follow")]
        Task<IllustsListingResponse> IllusFollow(string restrict = "public", int? offset = null);

        [HttpGet]
        [Authorize]
        [Route("v1/illust/detail")]
        Task<Illusts> IllustDetail(int illust_id);

        [HttpGet]
        [Authorize]
        [Route("v1/illust/recommended")]
        Task<IllustsListingResponse> IllustRecommended(string content_type = "illust", bool include_ranking_label = true, string filter = "for_ios");

        [HttpGet]
        [Authorize]
        [Route("v1/illust/ranking")]
        Task<IllustsListingResponse> IllustRanking([EnumCastString]RankingMode mode = RankingMode.day, string filter = "for_ios", string date = null, int? offset = null);

        [HttpPost]
        [Authorize]
        [Route("v1/illust/bookmark/add")]
        Task<IHttpResult<string>> IllustBookmarkAdd(int illust_id, string restrict = "public");

        [HttpPost]
        [Authorize]
        [Route("v1/illust/bookmark/delete ")]
        Task<IHttpResult<string>> IllustBookmarkDelete(int illust_id);

        [HttpGet]
        [Authorize]
        [Route("v1/user/following")]
        Task<UserListingResponse> UserFollowing(int user_id, string restrict = "public", int? offset = null);

        [HttpGet]
        [Authorize]
        [Route("v1/user/detail")]
        Task<UserDetailResponse> UserDetail(int user_id, string filter = "for_ios");

        [HttpGet]
        [Authorize]
        [Route("v1/user/recommended")]
        Task<UserListingResponse> UserRecommended(string filter = "for_ios", int? offset = null);

        [HttpGet]
        [Authorize]
        [Route("v1/user/bookmarks/illust")]
        Task<IllustsListingResponse> UserBookmarkIllust(int user_id, string restrict = "public");

        [HttpPost]
        [Authorize]
        [Route("v1/user/follow/add")]
        Task<IHttpResult<string>> UserFollowAdd(int user_id, string restrict = "public");

        [HttpPost]
        [Authorize]
        [Route("v1/user/follow/delete")]
        Task<IHttpResult<string>> UserFollowDelete(int user_id);
    }
}
