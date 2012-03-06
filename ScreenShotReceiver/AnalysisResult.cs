using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScreenShotReceiver
{
    public class AnalysisResult
    {
        private List<string> _text = new List<string>();

        private Dictionary<string, List<WordConfidence>> _faults = new Dictionary<string, List<WordConfidence>>();

        /// <summary>
        /// add a word found by the OCR
        /// </summary>
        /// <param name="word">word found by OCR</param>
        public void AddWord(string word)
        {
            _text.Add(word);
        }

        /// <summary>
        /// add the filterword and the text that contained the filter word
        /// </summary>
        /// <param name="fault">word to filter</param>
        /// <param name="word">text containing word to filter</param>
        public void AddFault(string fault, string word, int confidence)
        {
            if (!_faults.Keys.Contains(fault))
            {
                _faults[fault] = new List<WordConfidence>();

            }
            _faults[fault].Add(new WordConfidence(word, confidence));

        }

        /// <summary>
        /// List of all words found through OCR
        /// </summary>
        public List<string> Text
        {
            get
            {
                return _text.ToList();
            }
        }

        /// <summary>
        /// List of filter words that were found
        /// </summary>
        public List<string> FaultsFound
        {
            get
            {
                return _faults.Keys.ToList();
            }
        }
        
        /// <summary>
        /// List of words that contained filtered words
        /// </summary>
        public Dictionary<string, List<WordConfidence>> FaultedWords
        {
            get
            {
                return _faults;
            }
        }

        /// <summary>
        /// Determines if a word to filter was found
        /// </summary>
        public bool FaultFound
        {
            get
            {
                return _faults.Keys.Count > 0;
            }
        }

        public class WordConfidence
        {
            public string Word { get; set; }
            public int Confidence { get; set; }
            public WordConfidence(string word, int confidence)
            {
                Word = word;
                Confidence = confidence;
            }
        }
    }
}