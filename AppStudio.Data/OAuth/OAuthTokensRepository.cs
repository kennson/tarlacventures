using System.Collections.Generic;

namespace AppStudio.Data
{
    public static class OAuthTokensRepository
    {
        private static Dictionary<long, OAuthTokens> Tokens { get; set; }

        static OAuthTokensRepository()
        {
            Tokens = new Dictionary<long, OAuthTokens>();


            Tokens.Add(2257, new OAuthTokens
                            {
                                { "ClientId", "7b89306d4c164252bcb2962a5341391d" }
                            });

            Tokens.Add(2259, new OAuthTokens
                            {
                                { "ConsumerKey", "d5eDuK65RU1Q8MkiGG88ubrMv" },
                                { "ConsumerSecret", "SdwvxOBoCKGmQacRWR9slnEBO121RLc5PLaf2Tp3XWCCte0x3r" },
                                { "AccessToken", "134397845-wiYEZUYHvdnGF8CkSBEFa30ZgrGh8WK2p34Jeem4" },
                                { "AccessTokenSecret", "gzAih5Og97TtlkANKRec0LkpJLoLho4PNhkl9KPlEo8uV" }
                            });

        }

        public static OAuthTokens GetTokens(long key)
        {
            if (Tokens.ContainsKey(key))
            {
                return Tokens[key];
            }
            return null;
        }

    }
}
