using Dialogue;

namespace NPC
{
    class NonPlayableCharacter : INonPlayableCharacter
    {
        public string Id { get; }

        public string Name { get; }

        public bool Hostile { get; }

        public int HP
        {
            get => _hp;
        }

        private int _hp = 100;

        public string SceneDescription { get; }

        public List<IDialogue> Dialogues { get; }

        public IDialogue CurrentDialogue { get; }

        public void Heal(int health)
        {
            _hp += health;
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
        }

        public NonPlayableCharacter(
            string id,
            string name,
            bool hostile,
            int hp,
            string sceneDescription,
            List<IDialogue> dialogues,
            IDialogue currentDialogue
        )
        {
            Id = id;
            Name = name;
            Hostile = hostile;
            _hp = hp;
            SceneDescription = sceneDescription;
            Dialogues = dialogues;
            CurrentDialogue = currentDialogue;
        }
    }
}
