using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober.Dialogue
{
    [Serializable]
    public class SerializedDialogueFile
    {
        [Serializable]
        public class SerializedText
        {
            [SerializeField] private string id;
            [SerializeField] private string text;
            [SerializeField] private string type;
            [SerializeField] private int value;

            public string ID => id;
            
            public string Text => text;
            public string Type => type;
            public int Value => value;
        }
        
        [Serializable]
        public class SerializedGroup
        {
            [SerializeField] private string group;
            [SerializeField] private SerializedText[] texts;

            public string Group => group;
            
            public SerializedText[] Texts => texts;
        }
        
        [SerializeField] private SerializedGroup[] groups;
        [SerializeField] private SerializedText[] texts;

        public SerializedGroup[] Groups => groups;
        public SerializedText[] Texts => texts;
    }
}
