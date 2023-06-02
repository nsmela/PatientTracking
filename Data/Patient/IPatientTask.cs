
namespace PatientTracking.Data.Patient {
    public interface IPatientTask {
        public abstract string Label { get; set; }
        public abstract object Type { get; }
        public abstract PatientTaskGroup Parent { get; set; }
        public abstract void SetValue(Object value);
    }
}
