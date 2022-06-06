using System;
using System.Collections.Generic;
using System.IO;

namespace common.Core.Configurations.Base
{
    public abstract class Config
    {
        protected readonly string[] Properties;

        protected Config(string propertiesFile)
        {
            Properties = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), propertiesFile));
            Name = Properties[0];
            Width = Int32.Parse(Properties[1]);
            Height = Int32.Parse(Properties[2]);
            Fps = Int32.Parse(Properties[3]);
        }
        
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public int Fps { get; }
        
        public Dictionary<string, string> Texts { get; protected set; }
        
        protected virtual void BuildTexts(){}
    }
}