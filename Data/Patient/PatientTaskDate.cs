using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskDate : IPatientTask {
        TaskType IPatientTask.Type => TaskType.Date;
        public DateTime? LastestCommentDate { get; set; }
        public string Comments { get; set; }

        public PatientTaskGroup Parent { get; set; }
        public string Icon => Icons.Material.Filled.CalendarMonth;
        public string Label { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string ToString() => Date.ToShortDateString();
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(DateTime)) return;
            Date = (DateTime)value;
        }
    }
}
