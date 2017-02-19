using DevExpress.Xpo;
using System.Configuration;
using DevExpress.Xpo.DB;

namespace HangBreaker.Services {
    public sealed class XpoService : IXpoService {
        private readonly string ConnectionStringName;

        public XpoService(string connectionStringName) {
            ConnectionStringName = connectionStringName;
        }

        private IDataLayer fDataLayer;
        private IDataLayer DataLayer {
            get {
                if (fDataLayer == null) {
                    fDataLayer = GetDataLayer();
                }
                return fDataLayer;
            }
        }

        private IDataLayer GetDataLayer() {
            XpoDefault.Session = null;
            string conn = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            return XpoDefault.GetDataLayer(conn, AutoCreateOption.DatabaseAndSchema);
        }
        #region IXpoService
        Session IXpoService.GetSession() {
            return new Session(DataLayer);
        }

        UnitOfWork IXpoService.GetUnitOfWork() {
            return new UnitOfWork(DataLayer);
        }
        #endregion
    }
}
