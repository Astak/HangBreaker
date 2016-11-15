using DevExpress.Xpo;

namespace HangBreaker.Services {
    public interface IXpoService {
        Session GetSession();
        UnitOfWork GetUnitOfWork();
    }
}
