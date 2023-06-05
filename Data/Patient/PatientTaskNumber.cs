using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskNumber : IPatientTask {
        TaskType IPatientTask.Type => TaskType.Number;
        public DateTime? LastestCommentDate { get; set; }
        public string Comments { get; set; }

        public PatientTaskGroup Parent { get; set; }
        public string Icon => Icons.Material.Filled.TextSnippet;
        public string Label { get; set; }
        public double Value { get; set; } = 0.0f;
        public string ToString() => Value.ToString("0.000");
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(double)) return;
            Value = (double)value;
        }
    }
}
