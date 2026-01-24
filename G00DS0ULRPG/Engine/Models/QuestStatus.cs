using System.ComponentModel;

namespace Engine.Models
{
    public class QuestStatus : INotifyPropertyChanged
    {
        private bool _isComplete;

        public event PropertyChangedEventHandler? PropertyChanged;
        public Quest PlayerQuest { get; }
        public bool IsComplete 
        { 
            get
            {
                return _isComplete;
            }
            set
            {
                _isComplete = value;
            }
        }

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsComplete = false;
        }
    }
}
