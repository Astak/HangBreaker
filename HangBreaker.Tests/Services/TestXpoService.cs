using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using HangBreaker.Services;
using System;

namespace HangBreaker.Tests.Services {
    public sealed class TestXpoService :IXpoService {
        private readonly static object LockObject = new object();
        private static volatile IDataLayer fDataLayer;
        private static IDataLayer DataLayer {
            get {
                if (fDataLayer == null) {
                    lock (LockObject) {
                        if (fDataLayer == null)
                            fDataLayer = GetDataLayer();
                    }
                }
                return fDataLayer;
            }
        }

        private static IDataLayer GetDataLayer() {
            XpoDefault.Session = null;
            var prov = new InMemoryDataStore();
            return new SimpleDataLayer(prov);
        }

        public void Cleanup() {
            ((SimpleDataLayer)DataLayer).ClearDatabase();
        }

        Session IXpoService.GetSession() {
            return new Session(DataLayer);
        }

        UnitOfWork IXpoService.GetUnitOfWork() {
            return new UnitOfWork(DataLayer);
        }
    }
}
