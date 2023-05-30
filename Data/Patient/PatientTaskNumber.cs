namespace PatientTracking.Data.Patient {
    public class PatientTaskNumber : IPatientTask {
        public string Label { get; set; }
        public double Value { get; set; }
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(double)) return;
            Value = (double)value;
        }
    }
}
