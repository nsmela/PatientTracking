namespace PatientTracking.Data.Patient {
    public class Patient {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public uint Id { get; set; } = 0;
        public DateOnly StartDate { get; set; } = new DateOnly();
        public uint Status { get; set; } = 0; //started = 0, planning = 1, treating = 2, completed = 3;
        public List<IPatientTask> Tasks { get; set; } = new List<IPatientTask>();
        public List<PatientTaskGroup> TaskGroups { get; set; } = new List<PatientTaskGroup>();
    }
}
