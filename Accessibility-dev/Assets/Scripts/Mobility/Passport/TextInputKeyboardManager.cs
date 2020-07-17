using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Mobility.Passport.Utils;

namespace Mobility.Passport {
    public class TextInputKeyboardManager {
        [Range(1, 10)]
        [SerializeField]
        private int badCharacterOccurence = 3;

        public int charNumber { get; private set; } = 0;

        private Keyboard keyboard { get; set; }

        public TextInputKeyboardManager(Keyboard keyboard = null){
            this.keyboard = keyboard == null ? new QwertyKeyboard() : keyboard;
        }

        public char OnNewCharacter(char c) {
            charNumber++;
            var key = keyboard[c];

            if (charNumber > badCharacterOccurence && key != null) {
                charNumber = 0;
                var characters = key.surroundingKeys;
                var item = UnityEngine.Random.Range(0, characters.Count());
                var character = characters.ElementAt(item).keyCode;
                return Char.IsUpper(c) ? character : Char.ToLower(character);
            }

            return c;
        }

        public bool OnNewCharacter(char c, TextInputField field) {
            var character = OnNewCharacter(c);
            if (character != c) field.InputText += character;
            return character != c;
        }
    }
}
