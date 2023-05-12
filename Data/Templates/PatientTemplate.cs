namespace PatientTracking.Data.Templates {
    using PatientTracking.Data.Patient;

    public interface ITemplate {
        public int Id { get; set; }
        public Object Template { get; init; }
        public Object Type { get; }
    }

    public class PatientTemplate : ITemplate {
        public int Id { get; set; }
        public object Template { get; init; }
        public Object Type => typeof(Patient);

        public PatientTemplate(int id, Patient template) {
            Id = id;
            Template = template;
        }
    }

    public class TasksTemplate : ITemplate {
        public int Id { get; set; }
        public object Template { get; init; }
        public Object Type => typeof(PatientTaskGroup);

        public TasksTemplate(int id, PatientTaskGroup template) {
            Id = id;
            Template = template;
        }
    }
}

