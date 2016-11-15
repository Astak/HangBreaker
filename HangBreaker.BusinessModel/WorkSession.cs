using DevExpress.Xpo;
using System;

namespace HangBreaker.BusinessModel {
    public class WorkSession :XPObject {
        public WorkSession(Session session) : base(session) { }

        public string TicketID {
            get { return GetPropertyValue<string>("TicketID"); }
            set { SetPropertyValue<string>("TicketID", value); }
        }

        public WorkSessionStatus IntermediateStatus {
            get { return GetPropertyValue<WorkSessionStatus>("IntermediateStatus"); }
            set { SetPropertyValue<WorkSessionStatus>("IntermediateStatus", value); }
        }

        public WorkSessionStatus FinalStatus {
            get { return GetPropertyValue<WorkSessionStatus>("FinalStatus"); }
            set { SetPropertyValue<WorkSessionStatus>("FinalStatus", value); }
        }

        public TimeSpan Duration {
            get { return GetPropertyValue<TimeSpan>("Duration"); }
            set { SetPropertyValue<TimeSpan>("Duration", value); }
        }
    }

    public enum WorkSessionStatus { NeedAnswer, NeedExample, NeedToDiscuss }
}
