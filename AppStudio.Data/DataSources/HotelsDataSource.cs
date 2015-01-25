using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class HotelsDataSource : DataSourceBase<HotelsSchema>
    {
        private const string _file = "/Assets/Data/HotelsDataSource.json";

        protected override string CacheKey
        {
            get { return "HotelsDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<HotelsSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<HotelsSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("HotelsDataSource.LoadData", ex.ToString());
                return new HotelsSchema[0];
            }
        }
    }
}
