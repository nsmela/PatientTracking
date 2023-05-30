using Microsoft.Net.Http.Headers;

namespace PatientTracking.Data.Patient {
    public interface IPatientTask {
        public abstract string Label { get; set; }
        public abstract object Type { get; }
        public abstract string Icon { get; }
        public abstract void SetValue(Object value);
    }
}
