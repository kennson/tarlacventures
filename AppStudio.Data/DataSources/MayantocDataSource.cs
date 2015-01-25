using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class MayantocDataSource : DataSourceBase<MayantocSchema>
    {
        private const string _file = "/Assets/Data/MayantocDataSource.json";

        protected override string CacheKey
        {
            get { return "MayantocDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<MayantocSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<MayantocSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("MayantocDataSource.LoadData", ex.ToString());
                return new MayantocSchema[0];
            }
        }
    }
}
