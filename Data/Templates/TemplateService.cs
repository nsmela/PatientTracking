namespace PatientTracking.Data.Templates {
    public class TaskItem {
        public string Label { get; set; }
        public Object Type { get; set; } = typeof(bool);
    }

    public class TaskGroup {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<TaskItem> Tasks { get; set; }
        public int EditableTaskIndex { get; set; } = -1;

        public TaskGroup Copy() {
            var tasks = new List<TaskItem>();
            Tasks.ForEach(t => tasks.Add(new TaskItem { Label = t.Label, Type = t.Type }));

            return new TaskGroup {
                Id = this.Id,
                Label = this.Label,
                Tasks = tasks
            };

        }
    }

    public class TemplateItem {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<TaskGroup> Groups { get; set; }
        public int EditableGroupIndex { get; set; } = -1;
        public TemplateStatus Status { get; set; } = TemplateStatus.Active;
        public TemplateItem Copy() {
            var template = new TemplateItem { Label = this.Label, Id= this.Id, EditableGroupIndex = this.EditableGroupIndex, Groups = new List<TaskGroup>() };
            Groups.ForEach(g => template.Groups.Add(g.Copy()));
            return template;
        }
    }

    public enum TemplateStatus { New, Active, Retired, Delete }

    public class TemplateService {
        private List<TemplateItem> _templates { get; set; }
        private TaskGroup[] _groupTemplates { get; set; }

        public TemplateService() {
            _groupTemplates = new TaskGroup[]{
                new TaskGroup{Id = 0, Label = "General Tasks", Tasks = new List<TaskItem>{
                    new TaskItem{Label = "Approved by Oncologist", Type=typeof(bool) },
                    new TaskItem{Label = "Approved by Physics", Type=typeof(string)},
                    new TaskItem{Label = "Approved by Chemotherapy", Type=typeof(bool)}}
                },
                new TaskGroup{Id = 1, Label = "Physics Planning", Tasks = new List<TaskItem>{
                    new TaskItem{Label = "Dry Run Completed", Type = typeof(List<string>)},
                    new TaskItem{Label = "VMAT QA Required", Type = typeof(double)},
                }},
                new TaskGroup{Id = 2, Label = "Brachytherapy", Tasks = new List<TaskItem>{
                    new TaskItem{Label = "Seed Supply Checked", Type = typeof(bool)},
                    new TaskItem{Label = "Applicator Size Verified", Type = typeof(bool)},
                    new TaskItem{Label = "Patient Education Completed", Type = typeof(bool)},
                }}
            };

            _templates = new List<TemplateItem>{
                new TemplateItem {Id = 0, Label = "Standard Patient", Groups = new List<TaskGroup>{
                    _groupTemplates[0].Copy(), _groupTemplates[1].Copy()}
                },
                new TemplateItem {Id = 1, Label = "Brachy Patient", Groups = new List<TaskGroup>{
                    _groupTemplates[0].Copy(), _groupTemplates[2].Copy()}
                }
            };
        }

        public async Task<List<TemplateItem>> GetAllTemplates() => _templates;
        public async Task<List<TaskGroup>> GetAllGroupTemplates() => _groupTemplates.ToList();
        public async Task<TemplateItem> GetTemplate(int index) => _templates[index];
        public async Task<TaskGroup> GetGroup(int index) => _groupTemplates[index];
        public async Task AddTemplate(TemplateItem template) => _templates.Add(template);
        public async Task UpdateTemplate(TemplateItem template) {
            _templates[template.Id] = template;
        }
        public async Task RemoveTemplate(int index) {
            if (index < 0) return;
            _templates.RemoveAt(index);
        }
    }
}
