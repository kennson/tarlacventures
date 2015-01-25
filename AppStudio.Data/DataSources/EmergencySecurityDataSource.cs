using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class EmergencySecurityDataSource : DataSourceBase<EmergencySecuritySchema>
    {
        private const string _file = "/Assets/Data/EmergencySecurityDataSource.json";

        protected override string CacheKey
        {
            get { return "EmergencySecurityDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<EmergencySecuritySchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<EmergencySecuritySchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("EmergencySecurityDataSource.LoadData", ex.ToString());
                return new EmergencySecuritySchema[0];
            }
        }
    }
}
