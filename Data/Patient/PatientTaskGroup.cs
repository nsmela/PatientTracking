namespace PatientTracking.Data.Patient {
    public class PatientTaskGroup {
        public string Label { get; set; }
        public Patient Parent { get; set; }
        public List<IPatientTask> Tasks { get; set; }

        public PatientTaskGroup(string label, List<IPatientTask> tasks) {
            Label = label;
            Tasks = tasks;
            foreach(var task in Tasks) task.Parent = this;
        }
    }
}
