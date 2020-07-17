namespace Mobility.Passport {
    public class Passport {
        public string name { get; }

        public string placeOfBirth { get; }

        public Passport(string name, string placeOfBirth) {
            this.name = name;
            this.placeOfBirth = placeOfBirth;
        }

        public override string ToString() {
            return $"{name} ({placeOfBirth})";
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj) {
            return this.name == (obj as Passport).name && this.placeOfBirth == (obj as Passport).placeOfBirth;
        }
    }
}
