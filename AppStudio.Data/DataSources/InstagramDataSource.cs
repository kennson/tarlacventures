using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class InstagramDataSource : DataSourceBase<InstagramSchema>
    {
        private const long OAuthKey = 2257;
        private const string QueryType = "tag";
        private const string Query = "TARLACECOTOURISM";

        protected override string CacheKey
        {
            get { return "InstagramDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<InstagramSchema>> LoadDataAsync()
        {
            try
            {
                var instagramProvider = new InstagramDataProvider(QueryType,Query,OAuthTokensRepository.GetTokens(OAuthKey));
                return await instagramProvider.Load();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("InstagramDataSourceDataSource.LoadData", ex.ToString());
                return new InstagramSchema[0];
            }
        }
    }
}
