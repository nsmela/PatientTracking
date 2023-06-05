using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskList : IPatientTask {
        TaskType IPatientTask.Type => TaskType.List;
        public DateTime? LastestCommentDate { get; set; }
        public string Comments { get; set; }

        public PatientTaskGroup Parent { get; set; }
        public string Icon => Icons.Material.Filled.List;
        public string Label { get; set; }
        public List<string> Options { get; set; } = new();
        public int SelectedOption { get; set; }
        public string ToString() => Options[SelectedOption];
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(int)) return;
            SelectedOption = (int)value;
        }
    }
}
