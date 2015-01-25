using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class CapasDataSource : DataSourceBase<CapasSchema>
    {
        private const string _file = "/Assets/Data/CapasDataSource.json";

        protected override string CacheKey
        {
            get { return "CapasDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<CapasSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<CapasSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("CapasDataSource.LoadData", ex.ToString());
                return new CapasSchema[0];
            }
        }
    }
}
