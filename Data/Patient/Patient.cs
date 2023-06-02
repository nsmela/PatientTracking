namespace PatientTracking.Data.Patient {
    public class Patient {
        public string Id { get; set; } = "000000";
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; } = new DateTime();
        public uint Status { get; set; } = 0; //started = 0, planning = 1, treating = 2, completed = 3;
        public List<PatientTaskGroup> TaskGroups { get; set; } = new List<PatientTaskGroup>();

        public Patient(string id, string firstName, string lastName, List<PatientTaskGroup> taskGroups) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            TaskGroups = taskGroups;
            foreach (var group in TaskGroups) group.Parent = this;

        }

    }
}
