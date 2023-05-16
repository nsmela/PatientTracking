namespace PatientTracking.Data.Templates {
    public class TaskItem {
        public string Label { get; set; }
    }

    public class TaskGroup {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<TaskItem> Tasks { get; set; }
        public int EditableTaskIndex { get; set; } = -1;

        public TaskGroup Copy() {
            var tasks = new List<TaskItem>();
            Tasks.ForEach(t => tasks.Add(new TaskItem { Label = t.Label }));

            return new TaskGroup {
                Id = this.Id,
                Label = this.Label,
                Tasks = tasks
            };

        }
    }

    public class Template {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<TaskGroup> Groups { get; set; }
    }

    public class TemplateService {
        private List<Template> _templates { get; set; }
        private TaskGroup[] _groupTemplates { get; set; }

        public TemplateService() {
            _groupTemplates = new TaskGroup[]{
                new TaskGroup{Id = 0, Label = "General Tasks", Tasks = new List<TaskItem>{
                    new TaskItem{Label = "Approved by Oncologist"},
                    new TaskItem{Label = "Approved by Physics"},
                    new TaskItem{Label = "Approved by Chemotherapy"}}
                },
                new TaskGroup{Id = 1, Label = "Physics Planning", Tasks = new List<TaskItem>{
                    new TaskItem{Label = "Dry Run Completed"},
                    new TaskItem{Label = "VMAT QA Required"},
                }},
                new TaskGroup{Id = 2, Label = "Brachytherapy", Tasks = new List<TaskItem>{
                    new TaskItem{Label = "Seed Supply Checked"},
                    new TaskItem{Label = "Applicator Size Verified"},
                    new TaskItem{Label = "Patient Education Completed"},
                }}
            };

            _templates = new List<Template>{
                new Template {Id = 0, Label = "Standard Patient", Groups = new List<TaskGroup>{
                    _groupTemplates[0].Copy(), _groupTemplates[1].Copy()}
                },
                new Template {Id = 1, Label = "Brachy Patient", Groups = new List<TaskGroup>{
                    _groupTemplates[0].Copy(), _groupTemplates[2].Copy()}
                }
            };
        }

        public async Task<List<Template>> GetAllTemplates() => _templates;

        public async Task<Template> GetTemplate(int index) => _templates[index];

        public async Task AddTemplate(Template template) => _templates.Add(template);
        public async Task UpdateTemplate(Template template) {
            int index = _templates.IndexOf(template);
            if (index < 0) return;

            _templates[index] = template;
        }
        public async Task RemoveTemplate(int index) {
            if (index < 0) return;
            _templates.RemoveAt(index);
        }
    }
}
