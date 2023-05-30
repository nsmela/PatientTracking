namespace PatientTracking.Data.Patient {
    public class PatientTaskList : IPatientTask {
        public string Label { get; set; }
        public List<string> Values { get; set; }
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(List<string>)) return;
            Values = (List<string>)value;
        }
    }
}
