
namespace PatientTracking.Data.Patient {
    public interface IPatientTask {
        public abstract string Label { get; set; }
        public abstract TaskType Type { get; }
        public abstract PatientTaskGroup Parent { get; set; }
        public abstract void SetValue(Object value);
        public abstract string ToString();

        //list of edits
        //date, type, value

        //section for comments
        public DateTime? LastestCommentDate { get; set; }
        public string Comments { get; set; }
    }

    public enum TaskType { Checkbox, Text, Number, Date, List, Formula };

}
