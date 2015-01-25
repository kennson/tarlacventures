using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class SanJoseDataSource : DataSourceBase<SanJoseSchema>
    {
        private const string _file = "/Assets/Data/SanJoseDataSource.json";

        protected override string CacheKey
        {
            get { return "SanJoseDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<SanJoseSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<SanJoseSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("SanJoseDataSource.LoadData", ex.ToString());
                return new SanJoseSchema[0];
            }
        }
    }
}
