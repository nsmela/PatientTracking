using Microsoft.Net.Http.Headers;

namespace PatientTracking.Data.Patient {
    public interface IPatientTask {
        public abstract string Label { get; set; }
        public abstract void SetValue(Object value);
    }
}
