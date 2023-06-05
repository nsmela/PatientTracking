using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskText : IPatientTask {
        TaskType IPatientTask.Type => TaskType.Text;
        public DateTime? LastestCommentDate { get; set; }
        public string Comments { get; set; }

        public PatientTaskGroup Parent { get; set; }
        public string Icon => Icons.Material.Filled.TextSnippet;
        public string Label { get; set; }
        public string Text { get; set; } = string.Empty;
        public string ToString() => Text;

        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(string)) return;
            Text = (string)value;
        }
    }
}
