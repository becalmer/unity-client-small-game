using UnityEngine;
using System.Collections;

namespace framework
{
    public class log_t
    {
        private const int MAX_FRAME = 10;
        private const int MAX_SIZE = 10000000;
        private const int IDX_SIZE = 10;

        private int m_frame;
        private int m_idx;

        private string m_name;
        private string m_directory;

        private byte[] m_data;
        private int m_size;

        public log_t()
        {
            m_frame = 0;
            m_idx = 0;

            m_name = "";
            m_directory = "";

            m_data = new byte[MAX_SIZE];
            m_size = 0;
        }

        ~log_t()
        {
            flush();
        }

        public void set_filename(string name)
        {
            m_name = name;
        }

        public void get_directory()
        {
            m_directory = Application.persistentDataPath;

            if(!System.IO.Directory.Exists(m_directory))
            {
                System.IO.Directory.CreateDirectory(m_directory);
            }
        }

        public string format(string x, int n, string p)
        {
            string s = "";

            int m = n - x.Length;

            for(int c = 0; c < m; ++ c)
            {
                s += p;
            }

            s += x;

            return s;
        }

        public string get_time_str()
        {
            string s = "";

            s += System.DateTime.UtcNow.Year.ToString() + "/" + format(System.DateTime.UtcNow.Month.ToString(), 2, "0") + "/" + format(System.DateTime.UtcNow.Day.ToString(), 2, "0") + " " + format(System.DateTime.UtcNow.Hour.ToString(), 2, "0") + ":" + format(System.DateTime.UtcNow.Minute.ToString(), 2, "0") + ":" + format(System.DateTime.UtcNow.Second.ToString(), 2, "0");

            return s;
        }

        public string get_filename()
        {
            string name = "";

            if(0 == m_directory.Length)
            {
                get_directory();
            }

            name = m_directory + "/" + m_name + "_" + format(System.DateTime.UtcNow.Year.ToString(), 2, "0") + "_" + format(System.DateTime.UtcNow.Month.ToString(), 2, "0") + "_" + format(System.DateTime.UtcNow.Day.ToString(), 2, "0");

            return name;
        }

        public void tick()
        {
            ++ m_frame;
            if((m_frame >= MAX_FRAME) || (m_size >= MAX_SIZE))
            {
                m_frame = 0;
                flush();
            }
        }

        public string get_idx()
        {
            string s = m_idx.ToString();
            string ss = "[";

            int y = IDX_SIZE - s.Length;

            for(int x = 0; x < y; ++ x)
            {
                ss += "0";
            }

            ss += s;
            ss += "]";

            return ss;
        }

        public void write(string s)
        {
            string str = get_time_str() + " " + get_idx() + s;
            for(int x = 0; x < str.Length; ++ x)
            {
                m_data[m_size++] = (byte)str[x];
            }
            ++ m_idx;
        }

        public void flush()
        {
            if (m_size <= 0)
            {
                return;
            }

            string err = "";

            string name = get_filename();

            System.IO.FileStream fs = null;
            try
            {
                if (!System.IO.File.Exists(name))
                {
                    fs = System.IO.File.Open(name, System.IO.FileMode.Create);
                }
                else
                {
                    fs = System.IO.File.Open(name, System.IO.FileMode.Append);
                }
            }
            catch (System.ArgumentException e)
            {
                err = e.ToString();
            }
            catch (System.IO.PathTooLongException)
            {

            }
            catch (System.IO.DirectoryNotFoundException)
            {

            }
            catch (System.IO.IOException)
            {

            }
            catch (System.UnauthorizedAccessException)
            {

            }
            catch (System.NotSupportedException)
            {

            }
            catch (System.Exception e)
            {
                err = e.ToString() + "|" + e.Message;
            }
            if (fs == null)
            {
                if (m_size > (MAX_SIZE >> 1))
                {
                    m_size = 0;
                }
                return;
            }

            fs.Write(m_data, 0, m_size);

            fs.Close();

            m_size = 0;
        }
    }
}