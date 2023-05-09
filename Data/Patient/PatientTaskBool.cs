namespace PatientTracking.Data.Patient {
    public class PatientTaskBool : IPatientTask {
        public string Label { get; set; }
        public bool Checked { get; set; }
        public void SetValue(object value) {
            if (value.GetType() != typeof(bool)) return;
            Checked = (bool)value;
        }
    }
}
