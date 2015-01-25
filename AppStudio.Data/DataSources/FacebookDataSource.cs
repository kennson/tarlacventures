using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class FacebookDataSource : DataSourceBase<FacebookSchema>
    {
        private const string _userID = "666994530038506";

        protected override string CacheKey
        {
            get { return "FacebookDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<FacebookSchema>> LoadDataAsync()
        {
            try
            {
                var facebookDataProvider = new FacebookDataProvider(_userID);
                return await facebookDataProvider.Load();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("FacebookDataSourceDataSource.LoadData", ex.ToString());
                return new FacebookSchema[0];
            }
        }
    }
}
