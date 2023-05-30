using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskDate : IPatientTask {
        public object Type => typeof(DateTime);
        public string Icon => Icons.Material.Filled.CalendarMonth;
        public string Label { get; set; }
        public DateTime Date { get; set; }
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(DateTime)) return;
            Date = (DateTime)value;
        }
    }
}
