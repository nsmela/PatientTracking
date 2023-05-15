namespace PatientTracking.Data.Patient {
    public class PatientTaskDate : IPatientTask {
        public string Label { get; set; }
        public DateOnly Date { get; set; }
        public void SetValue(object value) {
            if (value.GetType() != typeof(DateOnly)) return;
            Date = (DateOnly)value;
        }
    }
}
