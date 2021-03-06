﻿using EasyHttpClient;
using PixivApi.Net.API.Attributes;
using PixivApi.Net.OAuth;
using System;

namespace PixivApi.Net
{
    public class PixivApiClientFactory
    {
        private readonly EasyHttpClientFactory _innerFactory;

        public PixivApiClientFactory(string username, string password)
            : this(username, password, false, null)
        {

        }

        public PixivApiClientFactory(string username, string password, bool enableDirectConnect)
         : this(username, password, enableDirectConnect, null)
        {

        }

        public PixivApiClientFactory(string username, string password, bool enableDirectConnect, Action<EasyClientConfig> configure) : base()
        {
            var config = new EasyClientConfig();
            var httpClientProvider = new PixivHttpClientProvier(enableDirectConnect);

            config.Host = new Uri("https://app-api.pixiv.net");
            config.HttpClientProvider = httpClientProvider;
            config.HttpClientSettings.Timeout = TimeSpan.FromSeconds(120);
            config.HttpClientSettings.ActionFilters.Add(new ResultActionFilterAttribute());
            config.HttpClientSettings.OAuth2ClientHandler = new PixivOAuthHandler(
                "https://oauth.secure.pixiv.net/",
                new PixivOAuthRequest
                {
                    ClientId = "MOBrBDS8blbauoSck0ZfDbtuzpyT",
                    ClientSecret = "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj",
                    GrantType = "password",
                    GetSecureUrl = "1",
                    Username = username,
                    Password = password,
                },
                new JsonFileAuthStore(),
                httpClientProvider
            );

            configure?.Invoke(config);

            _innerFactory = new EasyHttpClientFactory
            {
                Config = config
            };
        }

        public T Create<T>()
        {
            return _innerFactory.Create<T>();
        }
    }

}
