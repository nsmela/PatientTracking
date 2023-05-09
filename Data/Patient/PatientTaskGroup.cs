namespace PatientTracking.Data.Patient {
    public class PatientTaskGroup {
        public string Label { get; set; }
        public List<IPatientTask> Tasks { get; set; }
    }
}
