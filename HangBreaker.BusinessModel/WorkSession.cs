using DevExpress.Xpo;
using System;

namespace HangBreaker.BusinessModel {
    public class WorkSession :XPObject {
        public WorkSession(Session session) : base(session) { }

        public string TicketID {
            get { return GetPropertyValue<string>("TicketID"); }
            set { SetPropertyValue<string>("TicketID", value); }
        }

        public WorkSessionStatus? Status {
            get { return GetPropertyValue<WorkSessionStatus?>("Status"); }
            set { SetPropertyValue<WorkSessionStatus?>("Status", value); }
        }

        public TimeSpan Duration {
            get { return GetPropertyValue<TimeSpan>("Duration"); }
            set { SetPropertyValue<TimeSpan>("Duration", value); }
        }

        public DateTime StartTime {
            get { return GetPropertyValue<DateTime>("StartTime"); }
            set { SetPropertyValue<DateTime>("StartTime", value); }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();
            StartTime = DateTime.Now;
        }
    }

    public enum WorkSessionStatus { NeedAnswer, NeedExample, NeedToDiscuss, Complete }
}
