using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.Xml;

namespace SilverNibbles
{
    public class HighScorePersister
    {
        private const string recordFileName = "record.xml";

        public HighScore Load()
        {
            HighScore highScore = new HighScore();
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (file.FileExists(recordFileName))
                {
                    // TEST file.DeleteFile("record.xml");
                    IsolatedStorageFileStream stream = null;
                    try
                    {
                        using (stream = new IsolatedStorageFileStream(
                                     recordFileName,
                                     System.IO.FileMode.Open,
                                     System.IO.FileAccess.Read,
                                     file))
                        {
                            using (XmlReader reader = XmlReader.Create(stream))
                            {
                                if (reader.ReadToFollowing("HighScore"))
                                {
                                    highScore.Score = Int32.Parse(reader.GetAttribute("Score"));
                                    highScore.Date = DateTime.Parse(reader.GetAttribute("Date"));
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // first run on Vista seems to have a problem,
                        // that doesn't result in a FileNotFoundException
                        // - need to work out what this is
                        System.Diagnostics.Debug.WriteLine("LOAD RECORD ERROR");
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                    }
                }
            }
            return highScore;
        }

        public void Save(HighScore record)
        {
            using (var file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(
                                                       recordFileName,
                                                       System.IO.FileMode.Create,
                                                       file))
                {
                    using (var writer = XmlWriter.Create(stream))
                    {
                        writer.WriteStartElement("HighScores");
                        writer.WriteStartElement("HighScore");
                        writer.WriteAttributeString("Score", record.Score.ToString());
                        writer.WriteAttributeString("Date", record.Date.ToLongDateString());
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                }
            }
        }
    }
}
