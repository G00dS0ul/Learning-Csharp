namespace Engine.Models
{
    public class QuestStatus
    {
        public Quest PlayerQuest { get; set; }
        public bool IsComplete { get; set; }

        public QuestStatus(Quest quest)
        {
            this.PlayerQuest = quest;
            this.IsComplete = false;
        }
    }
}
