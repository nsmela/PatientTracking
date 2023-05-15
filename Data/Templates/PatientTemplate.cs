namespace PatientTracking.Data.Templates {
    using PatientTracking.Data.Patient;
    using System.Reflection.Emit;

    public interface ITemplate {
        public int Id { get; set; }
        public string Label { get; set; }
    }

    public class TaskGroupsTemplate : ITemplate {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<PatientTaskGroup> Groups {get; set;}

        public TaskGroupsTemplate(int id, string label, List<PatientTaskGroup> template) {
            Id = id;
            Label = label;
            Groups = template;
        }
    }

    public class TasksTemplate : ITemplate {
        public int Id { get; set; }
        public string Label { get; set; }
        public PatientTaskGroup Tasks {get;set;}

        public TasksTemplate(int id, string label, PatientTaskGroup template) {
            Id = id;
            Label = label;
            Tasks = template;
        }
    }
}

