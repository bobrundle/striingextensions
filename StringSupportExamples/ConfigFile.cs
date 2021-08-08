using StringSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSupportExamples
{
    public struct BoardSize
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public override string ToString()
        {
            return $"({Width},{Height})";
        }
        public static bool TryParse(string s, out BoardSize bs)
        {
            string[] ss = s?.Split(new char[] { '(', ',', ')' });
            if (ss.Length == 4)
            {
                bs = new BoardSize()
                {
                    Width = ss[1].GetValue<int>(40),
                    Height = ss[2].GetValue<int>(40)
                };
                return s == bs.ToString();
            }
            else
            {
                bs = new BoardSize() { Width = 40, Height = 40 };
                return false;
            }
        }
        public static BoardSize Parse(string s)
        {
            if (TryParse(s, out BoardSize bs))
                return bs;
            else
                throw new FormatException();
        }
    }

    public class ConfigFile
    {
        public BoardSize BoardSize { get; set; } = new BoardSize() { Width = 40, Height = 40 };
        public int HighScore { get; set; } = 0;
        public DateTime HighScoreDate { get; set; } = DateTime.MinValue;
        public enum Difficulty {  Easy, Medium, Hard, Impossible };
        public Difficulty DifficultySetting { get; set; } = Difficulty.Medium;
        public float ScalingFactor { get; set; } = 1.0f;
        public string GameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyGame");
        public const string GameConfigFile = "MyGame.ini";
        public string GameConfigFilePath => Path.Combine(GameFolder, GameConfigFile);
        public ConfigFile()
        {
            Load();               
        }
        public void Load()
        {
            try
            {
                string contents = File.ReadAllText(GameConfigFilePath);
                string[] elements = contents.Split('|');
                BoardSize = elements[0].GetValue<BoardSize>(new BoardSize() { Width = 40, Height = 40 });
                HighScore = elements[1].GetValue<int>(0);
                HighScoreDate = elements[2].GetValue<DateTime>(DateTime.MinValue);
                DifficultySetting = elements[3].GetValue<Difficulty>(Difficulty.Medium);
                ScalingFactor = elements[4].GetValue<float>(1.0f);
            }
            catch { }
        }
        public void Save()
        {
            Directory.CreateDirectory(GameFolder);
            File.WriteAllText(GameConfigFilePath, ToString());
        }
        public override string ToString()
        {
            return StringExtensions.SetValue(BoardSize)
                + "|" + StringExtensions.SetValue(HighScore)
                + "|" + StringExtensions.SetValue(HighScoreDate)
                + "|" + StringExtensions.SetValue(DifficultySetting)
                + "|" + StringExtensions.SetValue(ScalingFactor);
        }
    }
}
