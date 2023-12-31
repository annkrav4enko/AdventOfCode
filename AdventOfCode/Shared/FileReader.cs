﻿namespace Shared
{
    public class FileReader
    {
        public FileReader() { }

        public List<string> ReadFile (string path)
        {
            StreamReader sr = new StreamReader(path);

            var lines = new List<string>();

            var line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                lines.Add(line);
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();

            return lines;
        }
    }
}
