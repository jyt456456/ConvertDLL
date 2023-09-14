using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using graphglobal;
using Npgsql;
using System.Windows.Forms;
using static graphglobal.global;
using System.Runtime.InteropServices;

namespace postdb
{
    public class DB
    {



        private string m_constring;
        private NpgsqlConnection m_conn;
        private NpgsqlCommand m_Command;

        public DB()
        {
            IniClass.IniC cini = new IniClass.IniC("setting.ini");
            string section = "DB";
            string ip =  cini.GetIni(section, "IP");
            string id = cini.GetIni(section, "ID");
            string pw = cini.GetIni(section, "PW");
            m_constring = "Server=" + ip + ";username=" + id + ";password=" + pw + ";database=UniScan";
            // 
        }

        public List<int> SeachLot(string _Lot)
        {
            string query = "select DISTINCT frame_index from \"IM_Frame\" where lot_name ILIKE " + "'%" + _Lot + "%'";

            //List<im_frame> liframe = new List<im_frame>();
            List<int> result = new List<int>();

            using (var conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        using (var reader = cmd.ExecuteReader())
                        {

                            if (reader == null)
                                return null;

                            while (reader.Read())
                            {
                                int index = Convert.ToInt32(reader["frame_index"]);
                                result.Add(index);
                            }

                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }

        }


        
        public List<global.im_defect> GetDefect(string _Lot, int _frame)
        {

            string query = "select * from public.\"IM_Defect\" where lot_name = " + _Lot + "and frame_index = " + _frame.ToString() + "ORDER BY defect_index asc";

            //List<im_frame> liframe = new List<im_frame>();
            List<global.im_defect> result = new List<global.im_defect>();

            using (var conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        using (var reader = cmd.ExecuteReader())
                        {

                            if (reader == null)
                                return null;

                            while (reader.Read())
                            {
                                //listBox1.Items.Add(reader.GetString(0));
                                //or listBox1.Items.Add(reader["table_name"].ToString());
                                //frame.Frame_index = Convert.ToInt32(reader["frame_index"]);
                                global.im_defect def = new global.im_defect();

                                def.Defect_type = reader["defect_type"].ToString();
                                def.Defect_index = Convert.ToInt32(reader["defect_index"]);
                                def.Pos_x = Convert.ToDouble(reader["pos_x"]);
                                def.Pos_y = Convert.ToDouble(reader["pos_y"]);
                                def.Skip = Convert.ToBoolean(reader["skip"]);

                                result.Add(def);

                            }
                            reader.Close();

                            conn.Close();
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }




        }


        private bool NonQuery(string _str)
        {

            using (var conn = new NpgsqlConnection(m_constring))
            {


                try
                {
                    conn.Open();

                    // DB INSERT 구문
                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = _str;
                        command.ExecuteNonQuery();

                    }
                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
        }

        public bool addinfo(global.im_defect _deff)
        {


            string dt = DateTime.Now.ToString("yyyy-MM-dd");
            bool suc = false;

            //CM_LOT 시간,모델이름,lot이름
            global.im_defect pre = new global.im_defect();
            pre = _deff;
            string que = "insert into \"IM_Defect\" values(" + operstr(dt) + "," + operstr(pre.Model_name) + "," + operstr(pre.Lot_name) + ")";

            suc = NonQuery(que);

            Thread.Sleep(5);
            //lot이름, frame인덱스,module인덱스, 
            que = "insert into public.CM_Frame values(" + operstr(pre.Lot_name) + "," + pre.Frame_index.ToString() + "," + pre.Model.ToString() + ")";

            NonQuery(que);

            Thread.Sleep(5);

            //lot이름, frame인덱스, module인덱스, defect인덱스, defect타입, skip, posx,posy
            que = "insert into public.CM_Defect values(" + operstr(pre.Lot_name) + "," + pre.Frame_index.ToString() + "," + pre.Model.ToString() + ","
                + pre.Defect_index.ToString() + "," + operstr(pre.Defect_type) + "," + pre.Skip.ToString() + "," + pre.Pos_x.ToString() + "," + pre.Pos_y.ToString() + ")";

            NonQuery(que);
            return suc;


        }
        //where type='[0] and type=1 and type2
        

        private void checktable()
        {
            /*
            string str = "select * from pg_tables";
            sqlData(str);
            //string str = "select * from pg_database";
            List<string> listr = new List<string>();
            List<string> listschemaname = new List<string>();
            while (m_read.Read())
            {
                listschemaname.Add(m_read.GetString(0));
                listr.Add(m_read.GetString(1));
            }
            m_read.Close();
            m_conn.Close();
            //m_constring.Close();
            
            mvvm
            */
        }

        private string operstr(string _str)
        {
            _str = "'" + _str + "'";
            return _str;
        }


        public List<Defect_info> getGraph(int _frameindex, string _lot)
        {

            
            string str = "select pos_x,pos_y,defect_type,defect_index from \"IM_Defect\" where lot_name = " + operstr(_lot) + "and frame_index = " + _frameindex.ToString() + "ORDER BY defect_index asc";
            //string str = "select * from \"IM_Defect\" where lot_name = " + operstr(_lot) + "and frame_index = " + _frameindex.ToString() + "ORDER BY defect_index asc";
            List<Defect_info> listpt = new List<Defect_info>();



            using (var conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = str;
                        using (var reader = cmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {
                                Defect_info ppoint = new Defect_info();

                                ppoint.X = Convert.ToDouble(reader["pos_x"]);
                                ppoint.Y = Convert.ToDouble(reader["pos_y"]);
                                ppoint.Defect_type = reader["defect_type"].ToString();
                                ppoint.Defect_xindex = Convert.ToUInt32(reader["defect_index"]);
                                listpt.Add(ppoint);

                            }
                            reader.Close();

                            conn.Close();
                            return listpt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());

                }
            }




            return null;
        }


        public List<string> ChagneFrameCheck(string _Iot, int _ifrmae)
        {
            string strquery = "select DISTINCT defect_type from \"IM_Defect\" where lot_name = '" + _Iot + "' and frame_index = " + _ifrmae.ToString();
            List<string> listr = new List<string>();
            using (var conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = strquery;
                        using (var reader = cmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {
                                string Defect_type = reader["defect_type"].ToString();
                                listr.Add(Defect_type);
                            }
                            reader.Close();

                            conn.Close();
                            return listr;
                        }
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());

                }
            }
            return null;

        }


        public List<Defect_info> CheckDefectType(string _Lot, int frame, string defect_index)
        {
            //string str = "select pos_x,pos_y from \"IM_Defect\"";
            string str = "select pos_x,pos_y,defect_type,defect_index from \"IM_Defect\" where lot_name = " + operstr(_Lot) + "and frame_index = " + frame.ToString() + "and defect_type =" + operstr(defect_index) + "ORDER BY defect_index asc";
            List<Defect_info> listpt = new List<Defect_info>();

            using (var conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = str;
                        using (var reader = cmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {
                                Defect_info ppoint = new Defect_info();

                                ppoint.X = Convert.ToDouble(reader["pos_x"]);
                                ppoint.Y = Convert.ToDouble(reader["pos_y"]);
                                ppoint.Defect_type = reader["defect_type"].ToString();
                                ppoint.Defect_xindex = Convert.ToUInt32(reader["defect_index"]);
                                listpt.Add(ppoint);

                            }
                            reader.Close();

                            conn.Close();
                            return listpt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());

                }
            }




            return null;
        }

        //2안 이어서그리기
        public  List<graphglobal.global.Defect_info> GetRealtime(int _curcnt, string _lot, int _frame)
        {
            string str = "select pos_x,pos_y,defect_type from \"IM_Defect\" where lot_name = " + operstr(_lot) + "and frame_index = " + _frame.ToString() + "ORDER BY defect_index asc";
            List<Defect_info> listpt = new List<Defect_info>();

            int icnt = 0;
            using (var conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = str;
                        using (var reader = cmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {

                                if(icnt >=_curcnt)
                                {
                                    Defect_info ppoint = new Defect_info();

                                    ppoint.X = Convert.ToDouble(reader["pos_x"]);
                                    ppoint.Y = Convert.ToDouble(reader["pos_y"]);
                                    ppoint.Defect_type = reader["defect_type"].ToString();
                                    listpt.Add(ppoint);
                                }
                                ++icnt;
                            }
                            reader.Close();

                            conn.Close();
                            return listpt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());

                }
            }

            return null;
        }



        public int GetCnt(string _lot, int _frame)
        {

            string str = "select count(defect_index) from \"IM_Defect\" where lot_name = " + operstr(_lot) + "and frame_index = " + _frame.ToString();
            

            int icnt = 0;
            using (var conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = str;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                icnt = Convert.ToInt32(reader["count"]);
                                //count(defect_index)
                            }
                            reader.Close();

                            conn.Close();
                            return icnt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());
                    return 0;
                }
            }

            

            
        }

        public DefectAllinfo GetDefectAllinfo(uint defect_index)
        {
            string strquery = "select * from \"IM_Defect\" where defect_index = " + defect_index.ToString();


            DefectAllinfo info = new DefectAllinfo();
            using (var conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = strquery;
                        using (var reader = cmd.ExecuteReader())
                        {


                            if (reader.Read())
                            {

                                info.Pos_x = Convert.ToDouble(reader["pos_x"]);
                                info.Pos_y = Convert.ToDouble(reader["pos_y"]);
                                info.Defect_type = reader["defect_type"].ToString();
                                //listpt.Add(ppoint);

                                reader.Close();

                                conn.Close();
                                //return listpt;
                            }
                            else
                            {
                              //  info = null;
                            }
                                



                        }
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());

                }
            }


                return info;
        }

        /*

        private void sqlData(string _str)
        {
            string str = "select * from pg_tables";
            //string str = "select * from pg_database";
            List<string> listr = new List<string>();
            List<string> listschemaname = new List<string>();

            using (m_conn = new NpgsqlConnection(m_constring))
            {
                try
                {
                    m_conn.Open();
                    m_Command = new NpgsqlCommand();

                    m_Command.Connection = m_conn;
                    m_Command.CommandText = str;
                    m_read = m_Command.ExecuteReader();

                }
                catch (Exception ex)
                {
                    m_conn.Close();
                    MessageBox.Show(ex.ToString());

                }
            }


        }
        */
    }
}
