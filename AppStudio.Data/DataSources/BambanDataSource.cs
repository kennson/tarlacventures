using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class BambanDataSource : DataSourceBase<BambanSchema>
    {
        private const string _file = "/Assets/Data/BambanDataSource.json";

        protected override string CacheKey
        {
            get { return "BambanDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<BambanSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<BambanSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("BambanDataSource.LoadData", ex.ToString());
                return new BambanSchema[0];
            }
        }
    }
}
