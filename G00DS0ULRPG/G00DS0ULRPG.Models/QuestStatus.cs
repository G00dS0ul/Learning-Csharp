using System.ComponentModel;

namespace G00DS0ULRPG.Models
{
    public class QuestStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Quest PlayerQuest { get; }
        public bool IsComplete { get; set; }

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsComplete = false;
        }
    }
}
