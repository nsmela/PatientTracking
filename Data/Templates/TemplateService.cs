namespace PatientTracking.Data.Templates {
    public class TaskItem {
        public string Label { get; set; }
        public Object Type { get; set; } = typeof(bool);
    }

    public class TaskGroup {
        public int? Id { get; set; }
        public string Label { get; set; }
        public List<TaskItem> Tasks { get; set; }
        public int EditableTaskIndex { get; set; } = -1;
        public TemplateStatus Status { get; set; } = TemplateStatus.Active;

        public TaskGroup(string label, List<TaskItem> tasks, int? id) {
            Id = id;
            Label = label;
            Tasks = tasks;
        }

        public TaskGroup Copy() {
            var tasks = new List<TaskItem>();
            Tasks.ForEach(t => tasks.Add(new TaskItem { Label = t.Label, Type = t.Type }));
 
            return new TaskGroup(this.Label, tasks, this.Id);
        }
    }

    public class TemplateItem {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<TaskGroup> Groups { get; set; }
        public int EditableGroupIndex { get; set; } = -1;
        public TemplateStatus Status { get; set; } = TemplateStatus.Active; //used to allow children to delete themselves
        public TemplateItem Copy() {
            var template = new TemplateItem { Label = this.Label, Id= this.Id, EditableGroupIndex = this.EditableGroupIndex, Groups = new List<TaskGroup>() };
            foreach(var group in Groups) {
                template.Groups.Add(group.Copy());
            }
            return template;
        }
    }

    public enum TemplateStatus { New, Active, Retired, Delete }

    public class TemplateService {
        private List<TemplateItem> _templates { get; set; }
        private List<TaskGroup> _groupTemplates { get; set; }

        public TemplateService() {
            _groupTemplates = new List<TaskGroup>{
                new TaskGroup("General Tasks",  new List<TaskItem>{
                    new TaskItem{Label = "Due By:", Type = typeof(DateTime)},
                    new TaskItem{Label = "Approved by Oncologist", Type=typeof(bool) },
                    new TaskItem{Label = "Approved by Physics", Type=typeof(string)},
                    new TaskItem{Label = "Approved by Chemotherapy", Type=typeof(bool)}},
                    0),
                new TaskGroup("Physics Planning", new List<TaskItem>{
                    new TaskItem{Label = "Dry Run Completed", Type = typeof(List<string>)},
                    new TaskItem{Label = "VMAT QA Required", Type = typeof(double)}},
                    1),
                new TaskGroup("Brachytherapy", new List<TaskItem>{
                    new TaskItem{Label = "Seed Supply Checked", Type = typeof(bool)},
                    new TaskItem{Label = "Applicator Size Verified", Type = typeof(bool)},
                    new TaskItem{Label = "Patient Education Completed", Type = typeof(bool)}},
                    2),
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
        public async Task<TaskGroup> GetGroup(int index) => _groupTemplates.FirstOrDefault(x => x.Id == index);
        public async Task AddTemplate(TemplateItem template) => _templates.Add(template);
        public async Task AddGroup(TaskGroup group) {
            group.Id = _groupTemplates.Count;
            _groupTemplates.Add(group);
        }
        public async Task UpdateTemplate(TemplateItem template) {
            _templates[template.Id] = template;
        }
        public async Task UpdateGroup(TaskGroup group) {
            _groupTemplates[(int)group.Id] = group;
        }
        public async Task RemoveTemplate(int index) {
            _templates.RemoveAt((int)index);
        }
        public async Task RemoveGroup(int? index) {
            if (index is null || index < 0) return;
            foreach (var template in _templates) {
                foreach (var group in template.Groups) {
                    if (group.Id == index) {
                        template.Groups.Remove(group);
                        break;
                    }
                }
            }
            _groupTemplates.RemoveAt((int)index);
        }
    }
}
