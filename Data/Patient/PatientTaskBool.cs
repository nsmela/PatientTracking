using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskBool : IPatientTask {
        public PatientTaskGroup Parent { get; set; }
        public string Icon => Icons.Material.Filled.CheckBox;
        public string Label { get; set; }
        public string ToString() => Checked ? "Checked" : "Pending";
        public bool Checked { get; set; } = false;

        TaskType IPatientTask.Type => TaskType.Checkbox;

        public DateTime? LastestCommentDate { get; set; }
        public string Comments { get; set; }

        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(bool)) return;
            Checked = (bool)value;
        }
    }
}
