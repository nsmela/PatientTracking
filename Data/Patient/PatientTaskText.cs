﻿namespace PatientTracking.Data.Patient {
    public class PatientTaskText : IPatientTask {
        public string Label { get; set; }
        public string Text { get; set; }
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(string)) return;
            Text = (string)value;
        }
    }
}
